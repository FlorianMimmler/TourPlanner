using TourPlanner.Domain.Model;

namespace TourPlanner.BusinessLayer.Services
{
    public interface ITourLogService
    {
        event Action<TourLog> TourLogAdded;
        event Action<Guid> TourLogDeleted;
        event Action<TourLog> TourLogUpdated;

        Task AddTourLog(TourLog log);
        void DeleteTour(Guid tourlogid);
        void DeleteTourLog(Guid id);
        Task<IEnumerable<TourLog>> GetTourlogs();
        Task<IEnumerable<TourLog>> GetTourlogsByTour(Guid tourId);
        void UpdateTourLog(TourLog tourlog);
    }
}