using ChallengeENG.ViewModels;

namespace ChallengeENG.Views
{
    /// <summary>
    /// Lógica de interacción para MainView.xaml
    /// </summary>
    public partial class MainView
    {
        public MainView(MainViewModel mainViewModel)
        {
            this.DataContext = mainViewModel;
            InitializeComponent();
        }
    }
}