using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.DAL;

namespace TourPlanner.BusinessLayer.Services
{
    public class TourService : ITourService
    {

        private SingletonDatabase _database = SingletonDatabase.Instance;

        public event Action<Tour> TourAdded;
        public event Action<Tour> TourUpdated;
        public event Action<int> TourDeleted;

        public TourService()
        {

        }

        public IEnumerable<Tour> GetTours()
        {
            return _database.GetTours();
        }

        public void AddTour(Tour tour)
        {
            /*tour.Id = _tours.Count + 1;
            _tours.Add(tour);
            */
            TourAdded?.Invoke(_database.AddTour(tour));
        }

        public void UpdateTour(Tour tour)
        {
            /*var existingTour = _tours.FirstOrDefault(t => t.Id == tour.Id);
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
            }*/
            if (_database.UpdateTour(tour)) TourUpdated?.Invoke(tour);

        }

        public void DeleteTour(int id)
        {
            /*var tourToRemove = _tours.FirstOrDefault(t => t.Id == id);
            if (tourToRemove != null)
            {
                _tours.Remove(tourToRemove);
            }
            */
            _database.DeleteTour(id);
            TourDeleted?.Invoke(id);
        }
    }
}
