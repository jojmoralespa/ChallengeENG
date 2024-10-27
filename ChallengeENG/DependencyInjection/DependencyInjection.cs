using Autodesk.Revit.UI;
using ChallengeENG.Models;
using ChallengeENG.ViewModels;
using ChallengeENG.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ChallengeENG.DependencyInjection
{
    public class DependencyInjection
    {
        public static IServiceProvider GetServiceProvider(UIDocument uiDocument)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(uiDocument);

            //Models
            serviceCollection.AddSingleton<ModelScanner>();
            serviceCollection.AddSingleton<ElementActions>();
            serviceCollection.AddSingleton<ViewActions>();
            
            //ViewModels
            serviceCollection.AddSingleton<MainViewModel>();

            //Views
            serviceCollection.AddSingleton<MainView>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}