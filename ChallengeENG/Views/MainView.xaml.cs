using ChallengeENG.ViewModels;

namespace ChallengeENG.Views
{
    public partial class MainView
    {
        public MainView(MainViewModel mainViewModel)
        {
            this.DataContext = mainViewModel;
            InitializeComponent();
        }
    }
}