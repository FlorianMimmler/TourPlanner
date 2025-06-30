using PresentationLayer.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Domain.Model;
using PresentationLayer.View;
using PresentationLayer.Commands;
using BusinessLayer.Interfaces;

namespace PresentationLayer.ViewModel
{
    public class TourlogsViewModel : ViewModelBase
    {

        private ObservableCollection<TourLogItemViewModel> _tourLogs;

        private ITourService _tourService;
        private ITourLogService _tourlogsService;
        private SelectedTourStore _selectedTourStore;
        public ICommand OpenCreateTourLogCommand { get; }


        public event EventHandler OpenCreateTourLogRequested;

        public TourlogsViewModel(ITourService tourService, ITourLogService tourlogsService, SelectedTourStore selectedTourStore)
        {
            // Sample data for Tour Logs
            OpenCreateTourLogCommand = new RelayCommand(OpenCreateTourLogView);
            _tourService = tourService;
            _tourlogsService = tourlogsService;
            _selectedTourStore = selectedTourStore;
            TourLogs = new ObservableCollection<TourLogItemViewModel>();

            _selectedTourStore.SelectedTourChanged += async () => { await SelectedTourStore_SelectedTourChanged(); };

            _tourlogsService.TourLogAdded += TourlogsService_TourLogAdded;
            _tourlogsService.TourLogUpdated += TourlogsService_TourLogUpdated;
            _tourlogsService.TourLogDeleted += TourlogsService_TourLogDeleted;

        }

        private void TourlogsService_TourLogDeleted(Guid id)
        {
            TourLogItemViewModel tourLogItemViewModel = TourLogs.First(item => item.Id == id);
            if (tourLogItemViewModel != null)
            {
                TourLogs.Remove(tourLogItemViewModel);
            }
        }

        private void TourlogsService_TourLogAdded(TourLog log)
        {
            if(_selectedTourStore.SelectedTour?.Id == log.TourId)
            {
                TourLogItemViewModel tourLogItemViewModel = new(log, _tourlogsService);
                TourLogs.Add(tourLogItemViewModel);
                OnPropertyChanged(nameof(TourLogs));
            }
            
        }

        private void TourlogsService_TourLogUpdated(TourLog tourLog)
        {
            TourLogItemViewModel tourLogItemViewModel = TourLogs.First(item=>item.Id==tourLog.Id);
            if (tourLogItemViewModel != null)
            {
                tourLogItemViewModel.Update(tourLog);
            }

        }

        private async Task SelectedTourStore_SelectedTourChanged()
        {
            if(_selectedTourStore.SelectedTour == null)
            {
                TourLogs = new ObservableCollection<TourLogItemViewModel>();
                return;
            }
            IEnumerable<TourLog> tourLogs = await _tourlogsService.GetTourlogsByTour(_selectedTourStore.SelectedTour.Id);
            TourLogs = new ObservableCollection<TourLogItemViewModel>();
            foreach(TourLog item in tourLogs)
            {
                TourLogs.Add(new TourLogItemViewModel(item, _tourlogsService));
            }
        }  

        public ObservableCollection<TourLogItemViewModel> TourLogs
        {
            get { return _tourLogs; }
            set
            {
                _tourLogs = value;
                OnPropertyChanged(nameof(TourLogs));
            }
        }

        

        private void OpenCreateTourLogView()
        {
            CreateTourLogViewModel createTourLogViewModel = new CreateTourLogViewModel(_tourService, _tourlogsService, _selectedTourStore.SelectedTour);
            CreateTourLogView createTourLogView = new CreateTourLogView()
            {
                DataContext = createTourLogViewModel
            };

            createTourLogViewModel.CloseWindow += (s, a) => createTourLogView.Close();

            createTourLogView.ShowDialog();
        }

    }
}
