using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.ObjectModel;

namespace ChallengeENG.Models
{
    public class ElementActions
    {
        private readonly UIDocument _uiDocument;

        public ElementActions(UIDocument uIDocument)
        {
            _uiDocument = uIDocument;
        }

        public void SelectElements(ICollection<ElementId> elements)
        {
            var uiDocument = _uiDocument;

            uiDocument.Selection.SetElementIds(elements);
        }

        public void IsolateElements(ICollection<ElementId> elements)
        {
            using var transaction = new Transaction(_uiDocument.Document, "Get elements");

            transaction.Start();

            var activeView = _uiDocument.ActiveView;

            activeView.IsolateElementsTemporary(elements);

            transaction.Commit();
        }
    }
}