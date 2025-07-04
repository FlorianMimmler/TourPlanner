using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.Win32;
using PresentationLayer.Commands;
using PresentationLayer.Stores;
using PresentationLayer.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Domain.Model;

namespace PresentationLayer.ViewModel
{
    public class SidebarHeaderViewModel
    {
        private ITourService _tourService;

        public ICommand OpenCreateTourCommand { get; }

        public ICommand CreateSummary { get; }
        
        private ICreateTourReportService _createTourReportService;



        public SidebarHeaderViewModel(ITourService tourService, ICreateTourReportService createTourReportService)
        {
            OpenCreateTourCommand = new RelayCommand(OpenCreateTour);
            _tourService = tourService;
            var allTours = LoadTours();

            CreateSummary = new RelayCommand(async () => await CreateTourSummary());
            _createTourReportService = createTourReportService;


        }


        private async Task<IEnumerable<Tour>> LoadTours()
        {
            var tours = await _tourService.GetTours();
            return tours;
        }

        private void OpenCreateTour()
        {
            CreateTourViewModel createTourViewModel = new CreateTourViewModel(_tourService);
            CreateTourView createTourView = new CreateTourView()
            {
                DataContext = createTourViewModel
            };
            createTourViewModel.CloseWindow += (s, e) => createTourView.Close();
            createTourView.ShowDialog();
        }



        public async Task CreateTourSummary()
        {
            var dialog = new OpenFolderDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Title = "Select Folder"
            };


            if (dialog.ShowDialog() == true)
            {
                _createTourReportService.CreateTourSummary(dialog.FolderName);
            }
        }
    }
}
