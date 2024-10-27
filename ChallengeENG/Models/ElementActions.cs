using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ChallengeENG.Models
{
    /// <summary>
    /// All the logic related to Elements in view ( isolate and select elements)
    /// </summary>
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
            using var transaction = new Transaction(_uiDocument.Document, "isolate elements");

            transaction.Start();

            var activeView = _uiDocument.ActiveView;

            activeView.IsolateElementsTemporary(elements);

            transaction.Commit();
        }
    }
}