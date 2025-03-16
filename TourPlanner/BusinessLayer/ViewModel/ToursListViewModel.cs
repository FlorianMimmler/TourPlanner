using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;

namespace TourPlanner.BusinessLayer.ViewModel
{
    public class ToursListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Tour> _tours;
        private readonly TourService _tourService;
        private readonly TourLogService _tourlogService; // to change logs after adding tour

        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

        public ToursListViewModel()
        {

            _tourService = new TourService();
            Tours = _tourService.GetTours();

        }

        public void AddTour(Tour tour)
        {
            _tourService.AddTour(tour);
            _tourlogService.AddTourLogs(tour); // add to list of logs 
            OnPropertyChanged(nameof(Tours));

        }

        public void UpdateTour(Tour tour)
        {
            _tourService.UpdateTour(tour);
            OnPropertyChanged(nameof(Tours));
        }

        public void DeleteTour(int id)
        {
            _tourService.DeleteTour(id);
            OnPropertyChanged(nameof(Tours));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
