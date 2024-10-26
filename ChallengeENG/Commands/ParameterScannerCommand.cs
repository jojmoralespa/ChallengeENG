using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ChallengeENG.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ChallengeENG.Commands;

[Transaction(TransactionMode.Manual)]
[Regeneration(RegenerationOption.Manual)]
public class ParameterScannerCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        var serviceProvider = DependencyInjection.DependencyInjection.GetServiceProvider(commandData.Application.ActiveUIDocument);

        serviceProvider.GetRequiredService<MainView>().ShowDialog();
        return Result.Succeeded;
    }
}