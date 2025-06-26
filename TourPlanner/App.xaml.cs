using System.Configuration;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using PresentationLayer.ViewModel;
using PresentationLayer.View;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.DAL.QueryInterfaces;
using TourPlanner.DAL.Queries;
using TourPlanner.DAL;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Stores;

namespace TourPlanner;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    private readonly SelectedTourStore _selectedTourStore;
    private readonly ITourService _tourService;
    private TourLogService _tourLogService;
    private TourStatisticsService _tourStatisticsService;
    private TourExportService _tourOutputService;
    private TourImportService _tourImportService;

    private readonly TourPlannerDbContextFactory _tourPlannerDbContextFactory;
    private readonly IGetAllToursQuery _getAllToursQuery;
    private readonly ICreateTourQuery _createTourQuery;
    private readonly IGetAllTourLogsQuery _getAllTourLogsQuery;
    private readonly ICreateTourLogQuery _createTourLogQuery;

    private readonly string _connectionString = $"Host=localhost;Port=5432;Database=TourPlanner;Username=admin;Password=password;";

    public App()
    {
        _tourPlannerDbContextFactory = new TourPlannerDbContextFactory(
            new DbContextOptionsBuilder<TourPlannerDbContext>().UseNpgsql(_connectionString).Options);
        _getAllToursQuery = new GetAllToursQuery(_tourPlannerDbContextFactory);
        _createTourQuery = new CreateTourQuery(_tourPlannerDbContextFactory);
        _getAllTourLogsQuery = new GetAllTourLogsQuery(_tourPlannerDbContextFactory);
        _createTourLogQuery = new CreateTourLogQuery(_tourPlannerDbContextFactory);
        _tourService = new TourService(_getAllToursQuery, _createTourQuery);
        _selectedTourStore = new SelectedTourStore(_tourService);
        _tourLogService = new TourLogService(_getAllTourLogsQuery, _createTourLogQuery);
        _tourStatisticsService = new TourStatisticsService(_tourLogService);
        _tourOutputService = new TourExportService(_tourService, _tourLogService);
        _tourImportService = new TourImportService(_tourService, _tourLogService);
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        using(TourPlannerDbContext context = _tourPlannerDbContextFactory.Create())
        {
            context.Database.EnsureCreated();
        }

        MainWindow = new MainView()
        {
            DataContext = new MainViewModel(_selectedTourStore, _tourService, _tourLogService, _tourStatisticsService, _tourOutputService, _tourImportService)
        };
        MainWindow.Show();

        base.OnStartup(e);
    }
}

