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
    public class TourService
    {

        private SingletonDatabase _database = SingletonDatabase.Instance;

        public TourService()
        {
            
        }

        public ObservableCollection<Tour> GetTours()
        {
            return _database.GetTours();
        }

        public bool AddTour(Tour tour)
        {
            /*tour.Id = _tours.Count + 1;
            _tours.Add(tour);
            */
            return _database.AddTour(tour);
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
            _database.UpdateTour(tour);
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
        }
    }
}
