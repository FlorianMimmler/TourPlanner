using PresentationLayer.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain.Model;
using BusinessLayer.Services;
using PresentationLayer.View.Subviews.Sidebar;

namespace PresentationLayer.ViewModel
{
    public class MainViewModel
    {
        public SidebarViewModel SidebarViewModel { get; }
        public MainContentViewModel MainContentViewModel { get; }
        public MenuViewModel MenuViewModel { get; }
        public MainViewModel(SelectedTourStore _selectedTourStore, ITourService tourService, ITourLogService tourlogsService, TourStatisticsService tourStatisticsService, TourExportService tourOutputService, TourImportService tourImportService, IMapService mapService,CreateTourReportService createTourReportService)
        {
            SidebarViewModel = new SidebarViewModel(_selectedTourStore, tourService,createTourReportService);
            MainContentViewModel = new MainContentViewModel(_selectedTourStore, tourService, tourlogsService, tourStatisticsService, mapService);
            MenuViewModel = new MenuViewModel(tourOutputService, tourImportService);
        }
    }
}

