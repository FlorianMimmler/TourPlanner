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
using BusinessLayer.Interfaces;

namespace PresentationLayer.ViewModel
{
    public class MainViewModel
    {
        public SidebarViewModel SidebarViewModel { get; }
        public MainContentViewModel MainContentViewModel { get; }
        public MenuViewModel MenuViewModel { get; }
        public MainViewModel(SelectedTourStore _selectedTourStore, ITourService tourService, ITourLogService tourlogsService, ITourStatisticsService tourStatisticsService, ITourExportService tourOutputService, ITourImportService tourImportService, IMapService mapService, ICreateTourReportService createTourReportService, SearchStore searchStore, ITourFilterService tourFilterService)
        {
            SidebarViewModel = new SidebarViewModel(_selectedTourStore, tourService,createTourReportService, searchStore, tourFilterService);
            MainContentViewModel = new MainContentViewModel(_selectedTourStore, tourService, tourlogsService, tourStatisticsService, mapService);
            MenuViewModel = new MenuViewModel(tourOutputService, tourImportService);
        }
    }
}

