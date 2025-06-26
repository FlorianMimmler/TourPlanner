using TourPlanner.DAL;
using TourPlanner.DAL.QueryInterfaces;
using TourPlanner.Domain.Model;

namespace TourPlanner.BusinessLayer.Services
{
    public class TourLogService
    {

        private SingletonDatabase _database = SingletonDatabase.Instance;

        private readonly IGetAllTourLogsQuery _getAllTourLogsQuery;
        private readonly ICreateTourLogQuery _createTourLogQuery;

        public event Action<TourLog> TourLogAdded;
        public event Action<TourLog> TourLogUpdated;
        public event Action<int> TourLogDeleted;

        public TourLogService(IGetAllTourLogsQuery getAllTourLogsQuery, ICreateTourLogQuery createTourLogQuery)
        {
            _getAllTourLogsQuery = getAllTourLogsQuery;
            _createTourLogQuery = createTourLogQuery;
        }

        public async Task<IEnumerable<TourLog>> GetTourlogs()
        {
            return await _getAllTourLogsQuery.ExecuteAsync();
        }

        public IEnumerable<TourLog> GetTourlogsByTour(int tourId)
        {
            return _database.GetTourLogsByTour(tourId);
        }

        public async Task AddTourLog(TourLog log)
        {
            await _createTourLogQuery.ExecuteAsync(log);
            TourLogAdded?.Invoke(log);
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
