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
    class TourLogService
    {

        private SingletonDatabase _database = SingletonDatabase.Instance;

        public TourLogService()
        {

        }

        public ObservableCollection<TourLog> GetTourlogs()
        {
            return _database.GetTourLogs();
        }

        public bool AddTourLogs(Tour tour)
        {
            /*tour.Id = _tours.Count + 1;
            _tours.Add(tour);
            */
            return _database.AddTourLogs(tour);
        }

        public void DeleteTour(int tourlogid)
        {
            /*var tourToRemove = _tours.FirstOrDefault(t => t.Id == id);
            if (tourToRemove != null)
            {
                _tours.Remove(tourToRemove);
            }
            */
            _database.DeleteTour(tourlogid);
        }

        public void DeleteTourLog(int id)
        {
            /*var tourToRemove = _tours.FirstOrDefault(t => t.Id == id);
            if (tourToRemove != null)
            {
                _tours.Remove(tourToRemove);
            }
            */
            _database.DeleteTourLog(id);
        }


        public void UpdateTourLog(TourLog tourlog)
        {
            /*var tourToRemove = _tours.FirstOrDefault(t => t.Id == id);
            if (tourToRemove != null)
            {
                _tours.Remove(tourToRemove);
            }
            */
            _database.UpdateTourLogs(tourlog);
        }
    }
}
