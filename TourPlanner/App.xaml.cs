using System.Windows;
using PresentationLayer.ViewModel;
using PresentationLayer.View;
using DAL.QueryInterfaces;
using DAL.Queries;
using DAL;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Stores;
using System.Configuration;
using System.Runtime.CompilerServices;
using BusinessLayer.Services;
using log4net.Core;
using log4net;
using BusinessLayer.Logger;
using System.IO;
using BusinessLayer.Interfaces;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace TourPlanner;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    private readonly SelectedTourStore _selectedTourStore;
    private readonly SearchStore _searchStore;
    private readonly ITourService _tourService;
    private ITourLogService _tourLogService;
    private TourStatisticsService _tourStatisticsService;
    private ITourExportService _tourOutputService;
    private ITourImportService _tourImportService;
    private IMapService _mapService;
    private ITourFilterService _tourFilterService;
    private ICreateTourReportService _createReportService;



    private readonly TourPlannerDbContextFactory _tourPlannerDbContextFactory;
    private readonly IGetAllToursQuery _getAllToursQuery;
    private readonly IGetTourLogsByTourQuery _getTourLogsByTourQuery;
    private readonly ICreateTourQuery _createTourQuery;
    private readonly IGetAllTourLogsQuery _getAllTourLogsQuery;
    private readonly ICreateTourLogQuery _createTourLogQuery;

    private readonly IUpdateTourQuery _updateTourQuery;
    private readonly IUpdateTourLogQuery _updateTourLogQuery;
    private readonly IDeleteTourQuery _deleteTourQuery;
    private readonly IDeleteTourLogQuery _deleteTourLogQuery;
    

    private readonly string? _connectionString = ConfigurationManager.ConnectionStrings["connection_string"]?.ConnectionString;
    private readonly string? _orsApiKey = ConfigurationManager.AppSettings["ORSApiKey"];

    public App()
    {
        _tourPlannerDbContextFactory = new TourPlannerDbContextFactory(
            new DbContextOptionsBuilder<TourPlannerDbContext>().UseNpgsql(_connectionString).Options);

        _getAllToursQuery = new GetAllToursQuery(_tourPlannerDbContextFactory);
        _getTourLogsByTourQuery = new GetTourLogsByTourQuery(_tourPlannerDbContextFactory);
        _createTourQuery = new CreateTourQuery(_tourPlannerDbContextFactory);
        _getAllTourLogsQuery = new GetAllTourLogsQuery(_tourPlannerDbContextFactory);
        _createTourLogQuery = new CreateTourLogQuery(_tourPlannerDbContextFactory);
        _updateTourQuery = new UpdateTourQuery(_tourPlannerDbContextFactory);
        _deleteTourQuery = new DeleteTourQuery(_tourPlannerDbContextFactory);
        _updateTourLogQuery = new UpdateTourLogQuery(_tourPlannerDbContextFactory);
        _deleteTourLogQuery = new DeleteTourLogQuery(_tourPlannerDbContextFactory);

        _tourService = new TourService(_getAllToursQuery, _createTourQuery, _updateTourQuery, _deleteTourQuery, new LoggerWrapper(typeof(TourService)));
        _selectedTourStore = new SelectedTourStore(_tourService);
        _searchStore = new SearchStore();
        _tourLogService = new TourLogService(_getAllTourLogsQuery, _createTourLogQuery, _getTourLogsByTourQuery, _updateTourLogQuery, _deleteTourLogQuery, new LoggerWrapper(typeof(TourLogService)));
        _tourStatisticsService = new TourStatisticsService(_tourLogService);
        _tourOutputService = new TourExportService(_tourService, _tourLogService, new LoggerWrapper(typeof(TourExportService)));
        _tourImportService = new TourImportService(_tourService, _tourLogService, new LoggerWrapper(typeof(TourImportService)));
        _mapService = new MapService(_orsApiKey, new LoggerWrapper(typeof(MapService)));
        _tourFilterService = new TourFilterService(_tourService, _tourLogService, _tourStatisticsService);
        _createReportService = new CreateTourReportService(_tourService,_tourLogService, _tourStatisticsService);
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));

        using (TourPlannerDbContext context = _tourPlannerDbContextFactory.Create())
        {
            context.Database.EnsureCreated();
        }

        MainWindow = new MainView()
        {
            DataContext = new MainViewModel(_selectedTourStore, _tourService, _tourLogService, _tourStatisticsService, _tourOutputService, _tourImportService, _mapService,_createReportService, _searchStore, _tourFilterService)
        };
        MainWindow.Show();

        base.OnStartup(e);
    }
}

