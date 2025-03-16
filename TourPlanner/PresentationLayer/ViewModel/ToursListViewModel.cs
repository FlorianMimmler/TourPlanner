using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.PresentationLayer.Stores;

namespace TourPlanner.PresentationLayer.ViewModel
{
    public class ToursListViewModel : INotifyPropertyChanged
    {
        private readonly TourService _tourService;
        private readonly SelectedTourStore _selectedTourStore;

        public event EventHandler<Tour> TourSelected;


        private ObservableCollection<Tour> _tours;
        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                if (_selectedTour != value)
                {
                    _selectedTour = value;
                    _selectedTourStore.SelectedTour = _selectedTour;
                }
            }
        }

        public ToursListViewModel(SelectedTourStore selectedTourStore)
        {

            _tourService = new TourService();
            Tours = _tourService.GetTours();
            _selectedTourStore = selectedTourStore;
        }

        public void AddTour(Tour tour)
        {
            _tourService.AddTour(tour);
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
