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
    public class TourLogService
    {

        private SingletonDatabase _database = SingletonDatabase.Instance;

        public event Action<TourLog> TourLogAdded;
        public event Action<TourLog> TourLogUpdated;
        public event Action<int> TourLogDeleted;

        public TourLogService()
        {

        }

        public IEnumerable<TourLog> GetTourlogs()
        {
            return _database.GetTourLogs();
        }

        public IEnumerable<TourLog> GetTourlogsByTour(int tourId)
        {
            return _database.GetTourLogsByTour(tourId);
        }

        public void AddTourLog(TourLog log)
        {
            TourLogAdded?.Invoke(_database.AddTourLog(log));
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
            TourLogDeleted?.Invoke(tourlogid);
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
            TourLogDeleted?.Invoke(id);
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
            TourLogUpdated?.Invoke(tourlog);  
        }
    }
}
