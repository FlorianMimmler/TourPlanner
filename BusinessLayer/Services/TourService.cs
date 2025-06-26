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

        public event Action<Tour> TourAdded;
        public event Action<Tour> TourUpdated;
        public event Action<Guid> TourDeleted;

        public TourService(IGetAllToursQuery getAllToursQuery, ICreateTourQuery createTourQuery)
        {
            _getAllToursQuery = getAllToursQuery;
            _createTourQuery = createTourQuery;
        }

        public async Task<IEnumerable<Tour>> GetTours()
        {
            return await _getAllToursQuery.ExecuteAsync();
        }

        public async Task AddTour(Tour tour)
        {
            /*tour.Id = _tours.Count + 1;
            _tours.Add(tour);
            */
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

        public void UpdateTour(Tour tour)
        {
            /*var existingTour = _tours.FirstOrDefault(t => t.Id == tour.Id);
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
            }*/
            if (_database.UpdateTour(tour)) TourUpdated?.Invoke(tour);

        }

        public void DeleteTour(Guid id)
        {
            /*var tourToRemove = _tours.FirstOrDefault(t => t.Id == id);
            if (tourToRemove != null)
            {
                _tours.Remove(tourToRemove);
            }
            */
            //_database.DeleteTour(id);
            TourDeleted?.Invoke(id);
        }
    }
}
