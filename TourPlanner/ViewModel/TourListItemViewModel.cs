using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DAL;
using DAL.Queries;
using DAL.QueryInterfaces;
using Domain.Model;
using Microsoft.Win32;
using PresentationLayer.Commands;
using PresentationLayer.Commands;
using PresentationLayer.Stores;
using PresentationLayer.View;
using PresentationLayer.View;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly SelectedTourStore _selectedTourStore;
        private readonly SelectedTabStore _selectedTabStore;

        public Tour Tour { get; private set; }

        public string Name => Tour.Name;

        public string Id => Tour.Id.ToString();

        public ICommand ModifyCommand { get; }

        public ICommand CreateReport { get; }

        public TourListItemViewModel(Tour tour, ITourService tourService, ICreateTourReportService createTourReportService, SelectedTourStore selectedTourStore, SelectedTabStore selectedTabStore)
        {
            ModifyCommand = new RelayCommand(OpenModifyTourView);
            CreateReport = new RelayCommand(ProcessCreateReportRequest);
            //CreateReport = new RelayCommand(async () => await CreateTourReport(tour));
            Tour = tour;
            _tourService = tourService;

            _createTourReportService = createTourReportService;
            _selectedTourStore = selectedTourStore;
            _selectedTabStore = selectedTabStore;
        }

        private async void ProcessCreateReportRequest()
        {
            _selectedTourStore.SelectedTour = Tour;
            _selectedTabStore.SelectedTab = TabType.Route;
            
            await CreateTourReport();
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



        public async Task CreateTourReport()
        {
            var dialog = new OpenFolderDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Title = "Select Folder"
            };
            

            if (dialog.ShowDialog() == true)
            {
                await _createTourReportService.CreateTourReport(Tour, dialog.FolderName);
            }
        }




    }
}
