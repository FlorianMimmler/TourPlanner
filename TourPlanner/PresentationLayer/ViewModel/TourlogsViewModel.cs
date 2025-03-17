using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Commands;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.BusinessLayer.ViewModel;
using TourPlanner.PresentationLayer.Stores;
using TourPlanner.View;

namespace TourPlanner.PresentationLayer.ViewModel
{
    public class TourlogsViewModel : ViewModelBase
    {

        private ObservableCollection<TourLog> _tourLogs;

        private TourService _tourService;
        private TourLogService _tourlogsService;
        private SelectedTourStore _selectedTourStore;
        public ICommand OpenCreateTourLogCommand { get; }

        public event EventHandler OpenCreateTourLogRequested;

        public TourlogsViewModel(TourService tourService, TourLogService tourlogsService, SelectedTourStore selectedTourStore)
        {
            // Sample data for Tour Logs
            OpenCreateTourLogCommand = new RelayCommand(OpenCreateTourLogView);
            _tourService = tourService;
            _tourlogsService = tourlogsService;
            _selectedTourStore = selectedTourStore;
            TourLogs = new ObservableCollection<TourLog>();

            _selectedTourStore.SelectedTourChanged += SelectedTourStore_SelectedTourChanged;
        }

        private void SelectedTourStore_SelectedTourChanged()
        {
            if(_selectedTourStore.SelectedTour == null)
            {
                TourLogs = new ObservableCollection<TourLog>();
                return;
            }
            TourLogs= new ObservableCollection<TourLog>(_tourlogsService.GetTourlogsByTour(_selectedTourStore.SelectedTour.Id));
        }  

        public ObservableCollection<TourLog> TourLogs
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
            CreateTourLogViewModel createTourLogViewModel = new CreateTourLogViewModel(_tourService,_tourlogsService);
            CreateTourLogView createTourLogView = new CreateTourLogView()
            {
                DataContext = createTourLogViewModel
            };

            createTourLogViewModel.CloseWindow += (s, a) => createTourLogView.Close();

            createTourLogView.ShowDialog();
        }

    }
}
