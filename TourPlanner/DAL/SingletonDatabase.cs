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

       
        private ObservableCollection<TourLog> _tourlogs;
        private IEnumerable<Tour> _tours;

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

            _tourlogs = new ObservableCollection<TourLog>
            {
                new() {Distance = 5, Duration = "01:10", Date ="12.03.2025", Id=1},
                new() {Distance = 2, Duration = "00:30", Date ="13.03.2025", Id=2}

            };
        }

        public IEnumerable<Tour> GetTours()
        {
            return _tours;
        }

        public Tour AddTour(Tour tour)
        {
            tour.Id = _tours.Count() + 1;
            _tours.Append(tour);
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
            _tours = _tours.Where(t => t.Id != id);
        }

        public ObservableCollection<TourLog> GetTourLogs()
        {
            return _tourlogs;
        }


        public bool AddTourLogs(Tour tour) // to create a new log from existing tour
        {
            //tourlog.Id = _tourlogs.Count + 1;
            //_tourlog.Add(tourlog);

            TourLog? newlog = new TourLog();

            newlog.Id = tour.Id;
            newlog.Duration = tour.EstimatedTime;
            newlog.Date = "newdate";
            newlog.Distance = tour.Distance;
            

            _tourlogs.Add(newlog);
            return true;

        }

        public void DeleteTourLog(int Id) // For deleting specific tourlog 
        {
            var tourToRemove = _tourlogs.FirstOrDefault(t => t.Id == Id);
            if (tourToRemove != null)
            {
                _tourlogs.Remove(tourToRemove);
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
