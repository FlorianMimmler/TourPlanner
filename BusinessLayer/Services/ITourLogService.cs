using TourPlanner.Domain.Model;

namespace BusinessLayer.Services
{
    public interface ITourLogService
    {
        event Action<TourLog> TourLogAdded;
        event Action<Guid> TourLogDeleted;
        event Action<TourLog> TourLogUpdated;

        Task<IEnumerable<TourLog>> GetTourlogs();
        Task<IEnumerable<TourLog>> GetTourlogsByTour(Guid tourId);
        Task AddTourLog(TourLog tourlog);
        Task UpdateTourLog(TourLog tourlog);
        Task DeleteTourLog(TourLog tourlog);
    }
}