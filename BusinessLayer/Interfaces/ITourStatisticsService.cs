using TourPlanner.Domain.Model;

namespace BusinessLayer.Interfaces
{
    public interface ITourStatisticsService
    {
        Task<double> CalculateChildFriendliness(Tour tour);
        Task<double> CalculatePopularity(Tour tour);
    }
}