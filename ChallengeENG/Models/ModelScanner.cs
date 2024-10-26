using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ChallengeENG.Models
{
    public class ModelScanner
    {
        private readonly UIDocument _uiDocument;

        public ModelScanner(UIDocument uIDocument)
        {
            _uiDocument = uIDocument;
        }

        /// <summary>
        /// Counts the walls in revit
        /// </summary>
        public int CountWalls()
        {
            using var transaction = new Transaction(_uiDocument.Document, "Count walls");

            transaction.Start();
            var document = _uiDocument.Document;
            var walls = new FilteredElementCollector(document, document.ActiveView.Id)
                .WhereElementIsNotElementType()
                .OfClass(typeof(Wall))
                .ToList();
            transaction.Commit();

            return walls.Count;
        }

        public ICollection<Element> getAllModelElements()
        {
            using var transaction = new Transaction(_uiDocument.Document, "Get elements");

            transaction.Start();
            var document = _uiDocument.Document;
            var elements = new FilteredElementCollector(document, document.ActiveView.Id)
                .WhereElementIsNotElementType()
                .ToElements()
                .Where(element => element.Category?.IsVisibleInUI ?? false)
            .ToList();

            var parametersWithElementValues = elements
                .SelectMany(element => element.Parameters.Cast<Parameter>()
                .Where(param => param.Definition != null && param.HasValue)) // Filtramos los parámetros válidos
                .GroupBy(param => param.Definition.Name) // Agrupamos por el nombre del parámetro
                    .ToDictionary(
                        group => group.Key, // Nombre del parámetro como clave
                        group => group
                            .GroupBy(param => param.AsValueString() ?? "") // Agrupamos por valor del parámetro
                            .ToDictionary(
                                paramGroup => paramGroup.Key ?? "Undefined", // Valor del parámetro como clave
                                paramGroup => paramGroup
                                    .Select(param => param.Element.Id) // Lista de ElementId
                                    .ToList()
                            )
                    );

            transaction.Commit();

            return elements;
        }
    }
}