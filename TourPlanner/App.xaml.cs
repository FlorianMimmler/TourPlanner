﻿using System.Configuration;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using TourPlanner.PresentationLayer.Stores;
using TourPlanner.PresentationLayer.ViewModel;
using TourPlanner.PresentationLayer.View;
using TourPlanner.BusinessLayer.Services;

namespace TourPlanner;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    private readonly SelectedTourStore _selectedTourStore;
    private readonly ITourService _tourService;
    private TourLogService _tourLogService;

    public App()
    {
        _tourService = new TourService();
        _selectedTourStore = new SelectedTourStore(_tourService);
        _tourLogService = new TourLogService();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow = new MainView()
        {
            DataContext = new MainViewModel(_selectedTourStore, _tourService, _tourLogService)
        };
        MainWindow.Show();

        base.OnStartup(e);
    }
}

