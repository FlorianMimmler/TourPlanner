using System.Configuration;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using TourPlanner.PresentationLayer.Stores;
using TourPlanner.PresentationLayer.ViewModel;
using TourPlanner.PresentationLayer.View;

namespace TourPlanner;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    private readonly SelectedTourStore _selectedTourStore;

    public App()
    {
        _selectedTourStore = new SelectedTourStore();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow = new MainView()
        {
            DataContext = new MainViewModel(_selectedTourStore)
        };
        MainWindow.Show();

        base.OnStartup(e);
    }
}

