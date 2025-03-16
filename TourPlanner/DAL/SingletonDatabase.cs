using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer.Model;

namespace TourPlanner.DAL
{
    public class SingletonDatabase
    {

        private static SingletonDatabase? _instance;
        private static readonly object _lock = new();

        private ObservableCollection<Tour> _tours;

        public static SingletonDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock) // Ensures only one thread creates an instance
                    {
                        _instance ??= new SingletonDatabase();
                    }
                }
                return _instance;
            }
        }

        private SingletonDatabase() 
        {
            _tours = new ObservableCollection<Tour>()
            {
                new() {Id = 1, Name = "Wienerwald", Description = "A nice tour", Distance = 5.5, EstimatedTime = "01:10", From = "here", To = "there", TransportType = TransportType.Hiking, Image = ""},
                new() {Id = 1, Name = "Dorfrunde", Description = "A really nice tour", Distance = 2, EstimatedTime = "00:30", From = "here", To = "there", TransportType = TransportType.Bike, Image = ""}
            };
        }

        public ObservableCollection<Tour> GetTours()
        {
            return _tours;
        }

        public bool AddTour(Tour tour)
        {
            tour.Id = _tours.Count + 1;
            _tours.Add(tour);
            return true;
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
