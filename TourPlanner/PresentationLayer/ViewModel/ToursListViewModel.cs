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
    public class ToursListViewModel : ViewModelBase
    {
        private readonly TourService _tourService;
        private readonly SelectedTourStore _selectedTourStore;

        public event EventHandler<Tour> TourSelected;


        private ObservableCollection<TourListItemViewModel> _tours;
        public IEnumerable<TourListItemViewModel> Tours => _tours;

        private TourListItemViewModel _selectedTour;
        public TourListItemViewModel SelectedTour
        {
            get => _selectedTour;
            set
            {
                if (_selectedTour != value)
                {
                    _selectedTour = value;
                    _selectedTourStore.SelectedTour = _selectedTour.Tour;
                }
            }
        }

        public ToursListViewModel(SelectedTourStore selectedTourStore, TourService tourService)
        {
            _tourService = tourService;
            _selectedTourStore = selectedTourStore;
            _tours = new ObservableCollection<TourListItemViewModel>();

            foreach (var tour in _tourService.GetTours())
            {
                var tourListItemViewModel = new TourListItemViewModel(tour, tourService);
                _tours.Add(tourListItemViewModel);
            }

            _tourService.TourAdded += TourService_TourAdded;
            _tourService.TourUpdated += TourService_TourUpdated;
        }

        private void TourService_TourUpdated(Tour tour)
        {
            _tours.First(t => t.Tour.Id == tour.Id)?.UpdateTour(tour);
        }

        protected override void Dispose()
        {
            _tourService.TourAdded -= TourService_TourAdded;
        }

        private void TourService_TourAdded(Tour tour)
        {
            _tours.Add(new TourListItemViewModel(tour, _tourService));
        }
    }
}
