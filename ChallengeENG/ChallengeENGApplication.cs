using Autodesk.Revit.UI;
using ChallengeENG.Commands;
using System.Reflection;

namespace ChallengeENG;

public class ChallengeENGApplication : IExternalApplication
{
    public Result OnStartup(UIControlledApplication application)
    {
        var tabName = "Parameters2";
        application.CreateRibbonTab(tabName);

        var createdPanel = application.CreateRibbonPanel(tabName, "Parameter\nScanner");

        var NewPushButton = new PushButtonData("Parameter\nScanner", "Parameter\nScanner", Assembly.GetExecutingAssembly().Location,
            typeof(ParameterScannerCommand).FullName);

        createdPanel.AddItem(NewPushButton);

        return Result.Succeeded;
    }

    public Result OnShutdown(UIControlledApplication application)
    {
        return Result.Succeeded;
    }
}