using System.Runtime.CompilerServices;
using TourPlanner.Domain.Model;

namespace BusinessLayer.Services
{
    public interface ITourService
    {
        event Action<Tour> TourAdded;
        event Action<Guid> TourDeleted;
        event Action<Tour> TourUpdated;

        Task AddTour(Tour tour);
        Task DeleteTour(Tour tour);
        Task<IEnumerable<Tour>> GetTours();
        Task UpdateTour(Tour tour);
    }
}