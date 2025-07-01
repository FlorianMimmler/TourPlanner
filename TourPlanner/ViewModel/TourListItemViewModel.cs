using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DAL;
using DAL.Queries;
using DAL.QueryInterfaces;
using Microsoft.Win32;
using PresentationLayer.Commands;
using PresentationLayer.Commands;
using PresentationLayer.View;
using PresentationLayer.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner;
using TourPlanner.Domain.Model;

namespace PresentationLayer.ViewModel
{
    public class TourListItemViewModel : ViewModelBase
    {


        private ITourService _tourService;
        private ICreateTourReportService _createTourReportService;
        public Tour Tour { get; private set; }

        public string Name => Tour.Name;

        public string Id => Tour.Id.ToString();

        public ICommand ModifyCommand { get; }



        public ICommand CreateReport { get; }

        public TourListItemViewModel(Tour tour, ITourService tourService, ICreateTourReportService createTourReportService)
        {
            ModifyCommand = new RelayCommand(OpenModifyTourView);
            CreateReport = new RelayCommand(async () => await CreateTourReport(tour));
            Tour = tour;
            _tourService = tourService;

            _createTourReportService = createTourReportService;
            
        }

        private void OpenModifyTourView()
        {
            ModifyTourViewModel modifyTourViewModel = new ModifyTourViewModel(Tour, _tourService);
            ModifyTourView modifyTourView = new ModifyTourView()
            {
                DataContext = modifyTourViewModel
            };
            modifyTourViewModel.CloseWindow += (s, e) => modifyTourView.Close();
            modifyTourView.ShowDialog();
        }

        public void UpdateTour(Tour tour)
        {
            Tour = tour;
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Id));
        }

        /*public void ExecuteCreateReportService()
        {



            _=_createTourReportService.CreateTourReport(Tour);


        }*/



        public async Task CreateTourReport(Tour tour)
        {
            var dialog = new OpenFolderDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Title = "Select Folder"
            };
            

            if (dialog.ShowDialog() == true)
            {
                _createTourReportService.CreateTourReport(tour,dialog.FolderName);
            }
        }




    }
}
