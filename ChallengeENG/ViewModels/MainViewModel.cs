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
        private ObservableCollection<string> parameterNames = new();

        [ObservableProperty]
        private string? selectedParameter;

        [ObservableProperty]
        private ObservableCollection<string> parameterValues = new();

        [ObservableProperty]
        private string? selectedParameterValue;

        private Dictionary<string, Dictionary<string, List<ElementId>>> _parametersWithElementValues = new();
        public ICommand LoadParametersCommand { get; }
        public ICommand IsolateViewCommand { get; }
        public ICommand SelectCommand { get; }

        public MainViewModel(ModelScanner modelScanner, ElementActions elementActions, ViewActions viewActions)
        {
            LoadParametersCommand = new RelayCommand(() =>
            {
                if (!viewActions.IsValidView()) return;

                var parametersWithElementValues = modelScanner.GetAllModelElements();

                if (parametersWithElementValues.Count == 0)
                {
                    TaskDialog.Show("Error", "there aren't selected elements.");
                    return;
                }

                _parametersWithElementValues = parametersWithElementValues;

                ParameterNames.Clear();
                foreach (var name in parametersWithElementValues.Keys.ToList())
                {
                    ParameterNames.Add(name);
                }
            });

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