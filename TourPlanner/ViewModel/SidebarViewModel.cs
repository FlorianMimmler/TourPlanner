using PresentationLayer.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;

namespace PresentationLayer.ViewModel
{
    public class SidebarViewModel
    {
        public ToursListViewModel ToursListViewModel { get; }
        public SidebarHeaderViewModel SidebarHeaderViewModel { get; }


        public SidebarViewModel(SelectedTourStore _selectedTourStore, ITourService tourService, CreateTourReportService createTourReportService)
        {
            ToursListViewModel = new ToursListViewModel(_selectedTourStore, tourService, createTourReportService);
            SidebarHeaderViewModel = new SidebarHeaderViewModel(tourService);
        }
    }
}
