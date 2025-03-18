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

        private ObservableCollection<TourLogItemViewModel> _tourLogs;

        private ITourService _tourService;
        private TourLogService _tourlogsService;
        private SelectedTourStore _selectedTourStore;
        public ICommand OpenCreateTourLogCommand { get; }


        public event EventHandler OpenCreateTourLogRequested;

        public TourlogsViewModel(ITourService tourService, TourLogService tourlogsService, SelectedTourStore selectedTourStore)
        {
            // Sample data for Tour Logs
            OpenCreateTourLogCommand = new RelayCommand(OpenCreateTourLogView);
            _tourService = tourService;
            _tourlogsService = tourlogsService;
            _selectedTourStore = selectedTourStore;
            TourLogs = new ObservableCollection<TourLogItemViewModel>();

            _selectedTourStore.SelectedTourChanged += SelectedTourStore_SelectedTourChanged;

            _tourlogsService.TourLogUpdated += TourlogsService_TourLogUpdated;

        }

        private void TourlogsService_TourLogUpdated(TourLog tourLog)
        {
            TourLogItemViewModel tourLogItemViewModel = TourLogs.First(item=>item.Id==tourLog.Id);
            if (tourLogItemViewModel != null)
            {
                tourLogItemViewModel.Update(tourLog);
            }

        }

        private void OpenModifyTourLogView()
        {
            //ModifyTourLogViewModel modifyTourLogViewModel = new();

        }

        private void SelectedTourStore_SelectedTourChanged()
        {
            if(_selectedTourStore.SelectedTour == null)
            {
                TourLogs = new ObservableCollection<TourLogItemViewModel>();
                return;
            }
            IEnumerable<TourLog> tourLogs = _tourlogsService.GetTourlogsByTour(_selectedTourStore.SelectedTour.Id);
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
