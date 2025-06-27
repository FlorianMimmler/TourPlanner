using TourPlanner.Domain.Model;
using DAL;
using DAL.QueryInterfaces;

namespace TourPlanner.BusinessLayer.Services
{
    public class TourService : ITourService
    {

        private SingletonDatabase _database = SingletonDatabase.Instance;

        private readonly IGetAllToursQuery _getAllToursQuery;
        private readonly ICreateTourQuery _createTourQuery;
        private readonly IUpdateTourQuery _updateTourQuery;
        private readonly IDeleteTourQuery _deleteTourQuery;

        public event Action<Tour> TourAdded;
        public event Action<Tour> TourUpdated;
        public event Action<Guid> TourDeleted;

        public TourService(IGetAllToursQuery getAllToursQuery, ICreateTourQuery createTourQuery, IUpdateTourQuery updateTourQuery, IDeleteTourQuery deleteTourQuery)
        {
            _getAllToursQuery = getAllToursQuery;
            _createTourQuery = createTourQuery;
            _updateTourQuery = updateTourQuery;
            _deleteTourQuery = deleteTourQuery;
        }

        public async Task<IEnumerable<Tour>> GetTours()
        {
            return await _getAllToursQuery.ExecuteAsync();
        }

        public async Task AddTour(Tour tour)
        {
            try
            {
                tour.Id = Guid.NewGuid();
                await _createTourQuery.ExecuteAsync(tour);
                TourAdded?.Invoke(tour);
            } catch (Exception ex)
            {
                Console.WriteLine($"[AddTour] Error: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateTour(Tour tour)
        {
            var success = await _updateTourQuery.ExecuteAsync(tour);
            if (success) TourUpdated?.Invoke(tour);
        }

        public async Task DeleteTour(Tour tour)
        {
            var success = await _deleteTourQuery.ExecuteAsync(tour);
            if (success) TourDeleted?.Invoke(tour.Id);
        }
    }
}
