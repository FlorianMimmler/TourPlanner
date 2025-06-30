using DAL.QueryInterfaces;
using DAL;
using TourPlanner.Domain.Model;
using BusinessLayer.Interfaces;

namespace BusinessLayer.Services
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

        private readonly ILoggerWrapper _logger;

        public TourLogService(IGetAllTourLogsQuery getAllTourLogsQuery, ICreateTourLogQuery createTourLogQuery, IGetTourLogsByTourQuery getTourLogsByTourQuery, IUpdateTourLogQuery updateTourLogQuery, IDeleteTourLogQuery deleteTourLogQuery, ILoggerWrapper logger)
        {
            _getAllTourLogsQuery = getAllTourLogsQuery;
            _createTourLogQuery = createTourLogQuery;
            _getTourLogsByTourQuery = getTourLogsByTourQuery;
            _updateTourLogQuery = updateTourLogQuery;
            _deleteTourLogQuery = deleteTourLogQuery;
            _logger = logger;
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
            try
            {
                if (await _createTourLogQuery.ExecuteAsync(tourlog))
                {
                    _logger.Info($"New tourlog added: {tourlog.Id}");
                    TourLogAdded?.Invoke(tourlog);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        public async Task UpdateTourLog(TourLog tourlog)
        {
            try
            {
                if (await _updateTourLogQuery.ExecuteAsync(tourlog))
                {
                    _logger.Info($"Tourlog updated: {tourlog.Id}");
                    TourLogUpdated?.Invoke(tourlog);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        public async Task DeleteTourLog(TourLog tourlog)
        {
            try
            {
                if (await _deleteTourLogQuery.ExecuteAsync(tourlog))
                {
                    _logger.Info($"Tourlog deleted: {tourlog.Id}");
                    TourLogDeleted?.Invoke(tourlog.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
    }
}
