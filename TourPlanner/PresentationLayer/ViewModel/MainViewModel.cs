using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.PresentationLayer.Stores;
using TourPlanner.PresentationLayer.View.Subviews.Sidebar;

namespace TourPlanner.PresentationLayer.ViewModel
{
    public class MainViewModel
    {
        public SidebarViewModel SidebarViewModel { get; }
        public MainContentViewModel MainContentViewModel { get; }
        public MenuViewModel MenuViewModel { get; }
        public MainViewModel(SelectedTourStore _selectedTourStore, ITourService tourService, TourLogService tourlogsService)
        {
            SidebarViewModel = new SidebarViewModel(_selectedTourStore, tourService);
            MainContentViewModel = new MainContentViewModel(_selectedTourStore, tourService, tourlogsService);
            MenuViewModel = new MenuViewModel();
        }
    }
}

