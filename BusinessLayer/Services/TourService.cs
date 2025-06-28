using TourPlanner.Domain.Model;
using DAL;
using DAL.QueryInterfaces;

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
            if (await _createTourQuery.ExecuteAsync(tour)) TourAdded?.Invoke(tour);
        }

        public async Task UpdateTour(Tour tour)
        {
            if (await _updateTourQuery.ExecuteAsync(tour)) TourUpdated?.Invoke(tour);
        }

        public async Task DeleteTour(Tour tour)
        {
            if (await _deleteTourQuery.ExecuteAsync(tour)) TourDeleted?.Invoke(tour.Id);
        }


    }
}
