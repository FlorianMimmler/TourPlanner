using System.Runtime.CompilerServices;
using TourPlanner.Domain.Model;

namespace TourPlanner.BusinessLayer.Services
{
    public interface ITourService
    {
        event Action<Tour> TourAdded;
        event Action<Guid> TourDeleted;
        event Action<Tour> TourUpdated;

        Task AddTour(Tour tour);
        void DeleteTour(Guid id);
        Task<IEnumerable<Tour>> GetTours();
        void UpdateTour(Tour tour);
    }
}