using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer.Model;

namespace TourPlanner.BusinessLayer.Services
{
    class TourService
    {

        private ObservableCollection<Tour> _tours;

        public TourService()
        {
            _tours = new ObservableCollection<Tour>()
            {
                new() {Id = 1, Name = "Wienerwald", Description = "A nice tour", Distance = 5, EstimatedTime = 1.5, From = "here", To = "there", TransportType = null, Image = ""},
                new() {Id = 1, Name = "Dorfrunde", Description = "A nice tour", Distance = 2, EstimatedTime = 0.5, From = "here", To = "there", TransportType = null, Image = ""}
            };
        }

        public ObservableCollection<Tour> GetTours()
        {
            return _tours;
        }

        public void AddTour(Tour tour)
        {
            tour.Id = _tours.Count + 1;
            _tours.Add(tour);
        }

        public void UpdateTour(Tour tour)
        {
            var existingTour = _tours.FirstOrDefault(t => t.Id == tour.Id);
            if (existingTour != null)
            {
                existingTour.Name = tour.Name;
                existingTour.Description = tour.Description;
                existingTour.From = tour.From;
                existingTour.To = tour.To;
                existingTour.TransportType = tour.TransportType;
                existingTour.Distance = tour.Distance;
                existingTour.EstimatedTime = tour.EstimatedTime;
                existingTour.Image = tour.Image;
            }
        }

        public void DeleteTour(int id)
        {
            var tourToRemove = _tours.FirstOrDefault(t => t.Id == id);
            if (tourToRemove != null)
            {
                _tours.Remove(tourToRemove);
            }
        }
    }
}
