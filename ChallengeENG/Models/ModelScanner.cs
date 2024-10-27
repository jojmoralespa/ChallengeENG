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
        /// Get an diccionary that retrieve the parameternames with all its vallues, and each value with a List of Element Id that has this paramete with this value
        /// </summary>

        public Dictionary<string, Dictionary<string, List<ElementId>>> GetAllModelElements()
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
                .SelectMany(element => element.GetOrderedParameters()
                    .Where(param => param.Definition != null && param.HasValue)
                    .Select(param => new
                    {
                        ParameterName = param.Definition.Name,
                        ParameterValue = param.AsValueString() ?? "",
                        ElementId = element.Id
                    }))
                .GroupBy(x => x.ParameterName)
                .ToDictionary(
                    group => group.Key,
                    group => group
                        .GroupBy(x => x.ParameterValue)
                        .ToDictionary(
                            valueGroup => valueGroup.Key,
                            valueGroup => valueGroup.Select(x => x.ElementId).ToList()
                        )
                );

            transaction.Commit();

            return parametersWithElementValues;
        }
    }
}