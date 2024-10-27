using Autodesk.Revit.UI;
using ChallengeENG.Commands;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace ChallengeENG;

public class ChallengeENGApplication : IExternalApplication
{
    public Result OnStartup(UIControlledApplication application)
    {
        var tabName = "Parameters";
        application.CreateRibbonTab(tabName);

        var createdPanel = application.CreateRibbonPanel(tabName, "Parameter\nScanner");

        var uri = new Uri("pack://application:,,,/ChallengeENG;component/Resources/Icons/icons8-parameter-32.png");
        BitmapImage bitMapImage = new(uri);

        var NewPushButton = new PushButtonData("Parameter\nScanner", "Parameter\nScanner", Assembly.GetExecutingAssembly().Location,
            typeof(ParameterScannerCommand).FullName)
        {
            LargeImage = bitMapImage,
        };

        createdPanel.AddItem(NewPushButton);

        return Result.Succeeded;
    }

    public Result OnShutdown(UIControlledApplication application)
    {
        return Result.Succeeded;
    }
}