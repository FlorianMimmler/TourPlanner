using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;

namespace TourPlanner.BusinessLayer.ViewModel
{
    public class TourlogsViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<TourLog> _tourLogs;
        private ObservableCollection<Tour> _tours;
        private readonly TourLogService _tourlogsService;
        public ICommand OpenCreateTourCommand { get; }

        public event EventHandler OpenCreateTourLogRequested;



        public ObservableCollection<TourLog> TourLogs
        {
            get { return _tourLogs; }
            set
            {
                _tourLogs = value;
                OnPropertyChanged(nameof(TourLogs));
            }
        }

        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

        public TourlogsViewModel()
        {
            // Sample data for Tour Logs
            _tourlogsService = new TourLogService();
            TourLogs = _tourlogsService.GetTourlogs();
            

        }

        public void AddTourLog(Tour tour) //gets called in AddTourLog within TourListViewModel
        {
            _tourlogsService.AddTourLogs(tour);
            OnPropertyChanged(nameof(TourLogs));
        }

        private void OpenCreateTourLog()
        {
            OpenCreateTourLogRequested?.Invoke(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }






    }
}
