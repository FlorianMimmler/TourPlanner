using DAL.QueryInterfaces;
using DAL;
using TourPlanner.Domain.Model;

namespace TourPlanner.BusinessLayer.Services
{
    public class TourLogService : ITourLogService
    {

        private SingletonDatabase _database = SingletonDatabase.Instance;

        private readonly IGetAllTourLogsQuery _getAllTourLogsQuery;
        private readonly ICreateTourLogQuery _createTourLogQuery;
        private readonly IGetTourLogsByTourQuery _getTourLogsByTourQuery;

        public event Action<TourLog> TourLogAdded;
        public event Action<TourLog> TourLogUpdated;
        public event Action<Guid> TourLogDeleted;

        public TourLogService(IGetAllTourLogsQuery getAllTourLogsQuery, ICreateTourLogQuery createTourLogQuery, IGetTourLogsByTourQuery getTourLogsByTourQuery)
        {
            _getAllTourLogsQuery = getAllTourLogsQuery;
            _createTourLogQuery = createTourLogQuery;
            _getTourLogsByTourQuery = getTourLogsByTourQuery;
        }

        public async Task<IEnumerable<TourLog>> GetTourlogs()
        {
            return await _getAllTourLogsQuery.ExecuteAsync();
        }

        public async Task<IEnumerable<TourLog>> GetTourlogsByTour(Guid tourId)
        {
            return await _getTourLogsByTourQuery.ExecuteAsync(tourId);
        }

        public async Task AddTourLog(TourLog log)
        {
            log.Id = Guid.NewGuid();
            await _createTourLogQuery.ExecuteAsync(log);
            TourLogAdded?.Invoke(log);
        }

        public void DeleteTour(Guid tourlogid)
        {
            /*var tourToRemove = _tours.FirstOrDefault(t => t.Id == id);
            if (tourToRemove != null)
            {
                _tours.Remove(tourToRemove);
            }
            */
            //_database.DeleteTour(tourlogid);
            TourLogDeleted?.Invoke(tourlogid);
        }

        public void DeleteTourLog(Guid id)
        {
            /*var tourToRemove = _tours.FirstOrDefault(t => t.Id == id);
            if (tourToRemove != null)
            {
                _tours.Remove(tourToRemove);
            }
            */
            //_database.DeleteTourLog(id);
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
