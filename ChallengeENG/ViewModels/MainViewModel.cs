using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ChallengeENG.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ChallengeENG.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<string> _parameterNames = new();

        [ObservableProperty]
        private string? selectedParameter;

        [ObservableProperty]
        private ObservableCollection<string> _parameterValues = new();

        [ObservableProperty]
        private string? selectedParameterValue;

        private Dictionary<string, Dictionary<string, List<ElementId>>> _parametersWithElementValues = new();

        public ICommand CountWallsCommand { get; }
        public ICommand LoadParametersCommand { get; }
        public ICommand IsolateViewCommand { get; }
        public ICommand SelectCommand { get; }

        public MainViewModel(ModelScanner modelScanner, ElementActions elementActions, ViewActions viewActions)
        {
            CountWallsCommand = new RelayCommand(() =>
            {
                var wallCounts = modelScanner.CountWalls();

                TaskDialog.Show("Walls", $"se contaron {wallCounts} muros");
            });

            LoadParametersCommand = new RelayCommand(() =>
            {
                if (!viewActions.IsValidView()) return;

                var elements = modelScanner.getAllModelElements();

                if (elements.Count == 0)
                {
                    TaskDialog.Show("Error", "there aren't selected elements.");
                    return;
                }

                var parameterNames = elements
                .SelectMany(element => element.Parameters.Cast<Parameter>())
                .Select(param => param.Definition.Name)
                .Distinct()
                .ToList();

                //var parametersWithElementValues = elements
                //.SelectMany(element => element.Parameters.Cast<Parameter>()
                //.Where(param => param.Definition != null && param.HasValue)) // Filtramos los parámetros válidos
                //.GroupBy(param => param.Definition.Name) // Agrupamos por el nombre del parámetro
                //    .ToDictionary(
                //        group => group.Key, // Nombre del parámetro como clave
                //        group => group
                //            .Where(param => param.StorageType == StorageType.String) // Aseguramos que el valor sea del tipo correcto
                //            .GroupBy(param => param.AsValueString() ?? "") // Agrupamos por valor del parámetro
                //            .ToDictionary(
                //                paramGroup => paramGroup.Key ?? "Undefined", // Valor del parámetro como clave
                //                paramGroup => paramGroup
                //                    .Select(param => param.Element.Id) // Lista de ElementId
                //                    .ToList()
                //            )
                //    );

                var parametersWithElementValues = elements
                .SelectMany(element => element.GetOrderedParameters()
                    .Where(param => param.Definition != null && param.HasValue) // Filtra parámetros válidos
                    .Select(param => new
                    {
                        ParameterName = param.Definition.Name,
                        ParameterValue = param.AsValueString() ?? "",
                        ElementId = element.Id
                    }))
                .GroupBy(x => x.ParameterName) // Agrupa por nombre del parámetro
                .ToDictionary(
                    group => group.Key, // Nombre del parámetro como clave
                    group => group
                        .GroupBy(x => x.ParameterValue) // Agrupa por valor del parámetro
                        .ToDictionary(
                            valueGroup => valueGroup.Key, // Valor del parámetro como clave
                            valueGroup => valueGroup.Select(x => x.ElementId).ToList() // Lista de ElementId
                        )
                );

                _parametersWithElementValues = parametersWithElementValues;

                ParameterNames.Clear();
                foreach (var name in parameterNames)
                {
                    ParameterNames.Add(name);
                }
            });

            // Comando para aislar la vista
            IsolateViewCommand = new RelayCommand(() =>
            {
                if (SelectedParameter != null && SelectedParameterValue != null && _parametersWithElementValues.ContainsKey(SelectedParameter))
                {
                    var elementIds = _parametersWithElementValues[SelectedParameter][SelectedParameterValue];
                    elementActions.IsolateElements(elementIds);
                    TaskDialog.Show("info", $"{elementIds.Count} Elements has been isolated");
                }
            })
            {
            };

            // Comando para seleccionar elementos
            SelectCommand = new RelayCommand(() =>
            {
                if (SelectedParameter != null && SelectedParameterValue != null && _parametersWithElementValues.ContainsKey(SelectedParameter))
                {
                    var elementIds = _parametersWithElementValues[SelectedParameter][SelectedParameterValue];
                    elementActions.SelectElements(elementIds);
                    TaskDialog.Show("info", $"{elementIds.Count} Elements has been selected");
                }
            });

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(SelectedParameter))
                {
                    UpdateParameterValues();
                }
            };
        }

        private void UpdateParameterValues()
        {
            ParameterValues.Clear();
            if (SelectedParameter != null && _parametersWithElementValues.ContainsKey(SelectedParameter))
            {
                foreach (var value in _parametersWithElementValues[SelectedParameter].Keys)
                {
                    ParameterValues.Add(value);
                }
            }
        }
    }
}