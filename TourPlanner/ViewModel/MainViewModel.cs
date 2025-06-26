using PresentationLayer.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain.Model;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.PresentationLayer.View.Subviews.Sidebar;

namespace PresentationLayer.ViewModel
{
    public class MainViewModel
    {
        public SidebarViewModel SidebarViewModel { get; }
        public MainContentViewModel MainContentViewModel { get; }
        public MenuViewModel MenuViewModel { get; }
        public MainViewModel(SelectedTourStore _selectedTourStore, ITourService tourService, TourLogService tourlogsService, TourStatisticsService tourStatisticsService, TourOutputService tourOutputService, TourImportService tourImportService)
        {
            SidebarViewModel = new SidebarViewModel(_selectedTourStore, tourService);
            MainContentViewModel = new MainContentViewModel(_selectedTourStore, tourService, tourlogsService, tourStatisticsService);
            MenuViewModel = new MenuViewModel(tourOutputService, tourImportService);
        }
    }
}

