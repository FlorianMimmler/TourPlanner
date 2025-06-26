using System.Runtime.CompilerServices;
using TourPlanner.Domain.Model;

namespace TourPlanner.BusinessLayer.Services
{
    public interface ITourService
    {
        event Action<Tour> TourAdded;
        event Action<int> TourDeleted;
        event Action<Tour> TourUpdated;

        Task AddTour(Tour tour);
        void DeleteTour(int id);
        Task<IEnumerable<Tour>> GetTours();
        void UpdateTour(Tour tour);
    }
}