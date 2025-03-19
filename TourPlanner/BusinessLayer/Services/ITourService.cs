using TourPlanner.BusinessLayer.Model;

namespace TourPlanner.BusinessLayer.Services
{
    public interface ITourService
    {
        event Action<Tour> TourAdded;
        event Action<int> TourDeleted;
        event Action<Tour> TourUpdated;

        void AddTour(Tour tour);
        void DeleteTour(int id);
        IEnumerable<Tour> GetTours();
        void UpdateTour(Tour tour);
    }
}