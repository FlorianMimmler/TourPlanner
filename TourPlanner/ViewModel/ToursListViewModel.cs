using PresentationLayer.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Domain.Model;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;

namespace PresentationLayer.ViewModel
{
    public class ToursListViewModel : ViewModelBase
    {
        private readonly ITourService _tourService;
        private readonly SelectedTourStore _selectedTourStore;

        public event EventHandler<Tour> TourSelected;

        private ICreateTourReportService _createTourReportService;

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
                    _selectedTourStore.SelectedTour = _selectedTour?.Tour ?? null;
                }
            }
        }

        public ToursListViewModel(SelectedTourStore selectedTourStore, ITourService tourService, ICreateTourReportService createTourReportService)
        {
            _tourService = tourService;
            _selectedTourStore = selectedTourStore;
            _tours = new ObservableCollection<TourListItemViewModel>();

            _createTourReportService = createTourReportService;

            Load();

            _tourService.TourAdded += TourService_TourAdded;
            _tourService.TourUpdated += TourService_TourUpdated;
            _tourService.TourDeleted += _tourService_TourDeleted;
           // _createTourReportService.ReportCreated += TourService_ReportCreated;
        }

        private async Task Load()
        {
            var tours = await _tourService.GetTours();

            foreach (var tour in tours)
            {
                var tourListItemViewModel = new TourListItemViewModel(tour, _tourService,_createTourReportService);
                _tours.Add(tourListItemViewModel);
            }
        }

        private void _tourService_TourDeleted(Guid id)
        {
            _tours.Remove(_tours.First(t => t.Tour.Id == id));
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
            _tours.Add(new TourListItemViewModel(tour, _tourService,_createTourReportService));
        }

        /*private void TourService_ReportCreated(Tour tour)
        {
            _tours.First(t => t.Tour.Id == tour.Id)?.ExecuteCreateReportService(tour);
        }*/

       
    }
}
