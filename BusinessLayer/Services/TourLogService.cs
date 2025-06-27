using DAL.QueryInterfaces;
using DAL;
using TourPlanner.Domain.Model;

namespace TourPlanner.BusinessLayer.Services
{
    public class TourLogService : ITourLogService
    {
        private readonly IGetAllTourLogsQuery _getAllTourLogsQuery;
        private readonly ICreateTourLogQuery _createTourLogQuery;
        private readonly IGetTourLogsByTourQuery _getTourLogsByTourQuery;
        private readonly IUpdateTourLogQuery _updateTourLogQuery;
        private readonly IDeleteTourLogQuery _deleteTourLogQuery;

        public event Action<TourLog> TourLogAdded;
        public event Action<TourLog> TourLogUpdated;
        public event Action<Guid> TourLogDeleted;

        public TourLogService(IGetAllTourLogsQuery getAllTourLogsQuery, ICreateTourLogQuery createTourLogQuery, IGetTourLogsByTourQuery getTourLogsByTourQuery, IUpdateTourLogQuery updateTourLogQuery, IDeleteTourLogQuery deleteTourLogQuery)
        {
            _getAllTourLogsQuery = getAllTourLogsQuery;
            _createTourLogQuery = createTourLogQuery;
            _getTourLogsByTourQuery = getTourLogsByTourQuery;
            _updateTourLogQuery = updateTourLogQuery;
            _deleteTourLogQuery = deleteTourLogQuery;
        }

        public async Task<IEnumerable<TourLog>> GetTourlogs()
        {
            return await _getAllTourLogsQuery.ExecuteAsync();
        }

        public async Task<IEnumerable<TourLog>> GetTourlogsByTour(Guid tourId)
        {
            return await _getTourLogsByTourQuery.ExecuteAsync(tourId);
        }

        public async Task AddTourLog(TourLog tourlog)
        {
            if( await _createTourLogQuery.ExecuteAsync(tourlog) ) TourLogAdded?.Invoke(tourlog);
        }

        public async Task DeleteTourLog(TourLog tourlog)
        {
            if( await _deleteTourLogQuery.ExecuteAsync(tourlog) ) TourLogDeleted?.Invoke(tourlog.Id);
        }


        public async Task UpdateTourLog(TourLog tourlog)
        {
            if( await _updateTourLogQuery.ExecuteAsync(tourlog) ) TourLogUpdated?.Invoke(tourlog);
        }
    }
}
