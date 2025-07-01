using TourPlanner.Domain.Model;

namespace BusinessLayer.Interfaces
{
    public interface ITourFilterService
    {
        Task<IEnumerable<Tour>> FilterToursAsync(string searchTerm);
    }
}