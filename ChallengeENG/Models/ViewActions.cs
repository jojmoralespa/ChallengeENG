using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ChallengeENG.Models
{
    /// <summary>
    /// Logic related to the current view
    /// </summary>
    public class ViewActions
    {
        private readonly UIDocument _uiDocument;

        private static readonly List<ViewType> _allowedViewTypes = new()
    {
        ViewType.ThreeD,
        ViewType.FloorPlan,
        ViewType.CeilingPlan
    };

        public ViewActions(UIDocument uIDocument)
        {
            _uiDocument = uIDocument;
        }

        public bool IsValidView()
        {
            var activeView = _uiDocument.ActiveView;

            if (activeView != null && !_allowedViewTypes.Contains(activeView.ViewType))
            {
                TaskDialog.Show("warning", $"the current view \"{activeView.ViewType}\" is not allowed for element scanning.");
                return false;
            }
            return true;
        }
    }
}