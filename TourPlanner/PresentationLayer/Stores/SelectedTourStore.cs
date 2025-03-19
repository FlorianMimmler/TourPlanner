using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;

namespace TourPlanner.PresentationLayer.Stores
{
    public class SelectedTourStore
    {
        private ITourService _tourService;

        private Tour? _selectedTour;
        public Tour? SelectedTour 
        { 
            get
            {
                return _selectedTour;
            }
            set
            {
                _selectedTour = value;
                SelectedTourChanged?.Invoke();
            }
        }

        public event Action SelectedTourChanged;

        public SelectedTourStore(ITourService tourService)
        {
            this._tourService = tourService;
            _tourService.TourUpdated += TourService_TourUpdated;
            _tourService.TourDeleted += TourService_TourDeleted;
            _tourService.TourAdded += TourService_TourAdded;

        }

        private void TourService_TourAdded(Tour tour)
        {
            SelectedTour = tour;
        }

        private void TourService_TourDeleted(int id)
        {
            if(id == SelectedTour?.Id)
            {
                SelectedTour = null;
            }
        }

        private void TourService_TourUpdated(Tour tour)
        {
            if(tour.Id == SelectedTour?.Id)
            {
                SelectedTour = tour;
            }
        }
    }
}
