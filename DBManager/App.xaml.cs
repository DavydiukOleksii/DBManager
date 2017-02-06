using System.Windows;
using DBManager.ViewModel;

namespace DBManager.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // base.OnStartup(e);
            MainWindow window = new MainWindow();
            MainWindowViewModel viewModel = new MainWindowViewModel();

            //binding context to window
            window.DataContext = viewModel;
            window.Show();
        }
    }
}
