using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.BusinessLayer.Model;

namespace TourPlanner.DAL
{
    public class SingletonDatabase
    {

        private static SingletonDatabase? _instance;
        private static readonly object _lock = new();

       
        private List<TourLog> _tourlogs;
        private List<Tour> _tours;

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
            _tours = new List<Tour>()
            {
                new() {Id = 1, Name = "Wienerwald", Description = "A nice tour", Distance = 5.5, EstimatedTime = "01:10", From = "here", To = "there", TransportType = TransportType.Hiking, Image = ""},
                new() {Id = 2, Name = "Dorfrunde", Description = "A really nice tour", Distance = 2, EstimatedTime = "00:30", From = "here", To = "there", TransportType = TransportType.Bike, Image = ""}
            };

            _tourlogs = new List<TourLog>
            {
                new() {Distance = 5, Duration = "01:10", Date = DateTime.Now, Id=1, TourId=1, Rating = 4, Difficulty = 3, Comment = "Nice tour"},
                new() {Distance = 2, Duration = "00:30", Date = DateTime.Now, Id=2, TourId=2, Rating = 3, Difficulty = 2, Comment = "Nice tour"}

            };
        }

        public IEnumerable<Tour> GetTours()
        {
            return _tours;
        }

        public Tour AddTour(Tour tour)
        {
            if (_tours.Count == 0)
            {
                tour.Id = 1;
            }
            else
            {
                tour.Id = Math.Max(_tours.Count, _tours.Last<Tour>().Id) + 1;
            }
            _tours.Add(tour);
            return tour;
        }

        public bool UpdateTour(Tour tour)
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
            return true;
        }

        public void DeleteTour(int id)
        {
            var tourToRemove = _tours.FirstOrDefault(t => t.Id == id);
            if (tourToRemove != null)
            {
                _tours.Remove(tourToRemove);
            }

        }

        public IEnumerable<TourLog> GetTourLogs()
        {
            return _tourlogs;
        }

        public IEnumerable<TourLog> GetTourLogsByTour(int tourId)
        {
            return _tourlogs.Where(tourlog => tourlog.TourId == tourId);
        }


        public TourLog AddTourLog(TourLog log) // to create a new log from existing tour
        {
            log.Id = _tourlogs.Count() + 1;

            _tourlogs.Add(log);
            return log;

        }

        public void DeleteTourLog(int Id) // For deleting specific tourlog 
        {
            var tourLogToRemove = _tourlogs.FirstOrDefault(tourlog => tourlog.Id == Id);
            if (tourLogToRemove != null)
            {
                _tourlogs.Remove(tourLogToRemove);
            }
        }


        public void UpdateTourLogs(TourLog tourlog)
        {
            var existingTourlog = _tourlogs.FirstOrDefault(t => t.Id == tourlog.Id);
            if (existingTourlog != null)
            {
                existingTourlog.Duration = tourlog.Duration;
                existingTourlog.Date = tourlog.Date;
                existingTourlog.Distance = tourlog.Distance;
                existingTourlog.Id = tourlog.Id;
            }
        }

    }
}
