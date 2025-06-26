using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.Domain.Model;

namespace PresentationLayer.Stores
{
    internal class TourStore
    {

        private readonly TourService _tourService;
        public ObservableCollection<Tour> Tours { get; } = new();

        public TourStore(TourService tourService)
        {
            _tourService = tourService;
        }

        public async Task LoadToursAsync()
        {
            var tours = await _tourService.GetTours();
            Tours.Clear();
            foreach (var tour in tours)
            {
                Tours.Add(tour);
            }
        }

        public async Task AddTour(Tour tour)
        {
            await _tourService.AddTour(tour);
            Tours.Add(tour);
            //TourAdded?.Invoke(tour);
        }

        public void UpdateTour(Tour tour)
        {
            _tourService.UpdateTour(tour);

            var index_to_update = Tours.IndexOf(tour);

            if(index_to_update != -1)
            {
                Tours[index_to_update] = tour;
            }

            //if (_database.UpdateTour(tour)) TourUpdated?.Invoke(tour);

        }

        public void DeleteTour(Guid id)
        {
            _tourService.DeleteTour(id);

            //TourDeleted?.Invoke(id);
        }

    }
}
