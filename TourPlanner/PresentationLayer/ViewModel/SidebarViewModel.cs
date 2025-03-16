using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.PresentationLayer.Stores;

namespace TourPlanner.PresentationLayer.ViewModel
{
    public class SidebarViewModel
    {
        public ToursListViewModel ToursListViewModel { get; }

        public SidebarViewModel(SelectedTourStore _selectedTourStore)
        {
            ToursListViewModel = new ToursListViewModel(_selectedTourStore);
        }
    }
}
