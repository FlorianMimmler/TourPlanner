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
        private Tour _selectedTour;
        private TourService _tourService;

        public Tour SelectedTour 
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

        public SelectedTourStore(TourService tourService)
        {
            this._tourService = tourService;
            _tourService.TourUpdated += TourService_TourUpdated;

        }

        private void TourService_TourUpdated(Tour tour)
        {
            if(tour.Id == SelectedTour.Id)
            {
                SelectedTour = tour;
            }
        }
    }
}
