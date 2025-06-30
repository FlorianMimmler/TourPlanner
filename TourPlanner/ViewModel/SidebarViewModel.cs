using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.Win32;
using PresentationLayer.Commands;
using PresentationLayer.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Domain.Model;

namespace PresentationLayer.ViewModel
{
    public class SidebarViewModel
    {
        public ToursListViewModel ToursListViewModel { get; }
        public SidebarHeaderViewModel SidebarHeaderViewModel { get; }
        

        private CreateTourReportService _createTourReportService;


        public SidebarViewModel(SelectedTourStore _selectedTourStore, ITourService tourService, ICreateTourReportService createTourReportService)
        {
            ToursListViewModel = new ToursListViewModel(_selectedTourStore, tourService, createTourReportService);
            SidebarHeaderViewModel = new SidebarHeaderViewModel(tourService,createTourReportService);
            _createTourReportService = createTourReportService;
            
           

        }





    
    }
}
