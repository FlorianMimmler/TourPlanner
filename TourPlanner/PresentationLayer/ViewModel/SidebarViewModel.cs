using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.PresentationLayer.Stores;

namespace TourPlanner.PresentationLayer.ViewModel
{
    public class SidebarViewModel
    {
        public ToursListViewModel ToursListViewModel { get; }
        public SidebarHeaderViewModel SidebarHeaderViewModel { get; }

        public SidebarViewModel(SelectedTourStore _selectedTourStore, TourService tourService)
        {
            ToursListViewModel = new ToursListViewModel(_selectedTourStore, tourService);
            SidebarHeaderViewModel = new SidebarHeaderViewModel(tourService);
        }
    }
}
