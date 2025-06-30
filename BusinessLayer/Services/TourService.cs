using TourPlanner.Domain.Model;
using DAL;
using DAL.QueryInterfaces;
using BusinessLayer.Interfaces;

namespace BusinessLayer.Services
{
    public class TourService : ITourService
    {
        private readonly IGetAllToursQuery _getAllToursQuery;
        private readonly ICreateTourQuery _createTourQuery;
        private readonly IUpdateTourQuery _updateTourQuery;
        private readonly IDeleteTourQuery _deleteTourQuery;


        public event Action<Tour> TourAdded;
        public event Action<Tour> TourUpdated;
        public event Action<Guid> TourDeleted;
        public event Action<Guid> TourSelected;

        private readonly ILoggerWrapper _logger;

        public TourService(IGetAllToursQuery getAllToursQuery, ICreateTourQuery createTourQuery, IUpdateTourQuery updateTourQuery, IDeleteTourQuery deleteTourQuery, ILoggerWrapper logger)
        {
            _getAllToursQuery = getAllToursQuery;
            _createTourQuery = createTourQuery;
            _updateTourQuery = updateTourQuery;
            _deleteTourQuery = deleteTourQuery;

            _logger = logger;
        }

        public async Task<IEnumerable<Tour>> GetTours()
        {
            return await _getAllToursQuery.ExecuteAsync();
        }

        public async Task AddTour(Tour tour)
        {
            try
            {
                if (await _createTourQuery.ExecuteAsync(tour))
                {
                    _logger.Info($"New tour added: {tour.Id}");
                    TourAdded?.Invoke(tour);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        public async Task UpdateTour(Tour tour)
        {
            try
            {
                if (await _updateTourQuery.ExecuteAsync(tour))
                {
                    _logger.Info($"Tour updated: {tour.Id}");
                    TourUpdated?.Invoke(tour);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            
        }

        public async Task DeleteTour(Tour tour)
        {
            try
            {
                if (await _deleteTourQuery.ExecuteAsync(tour))
                {
                    _logger.Info($"Tour deleted: {tour.Id}");
                    TourDeleted?.Invoke(tour.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }


    }
}
