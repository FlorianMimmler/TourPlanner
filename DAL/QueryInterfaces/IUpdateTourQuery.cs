using TourPlanner.Domain.Model;

namespace DAL.QueryInterfaces
{
    public interface IUpdateTourQuery
    {
        Task<bool> ExecuteAsync(Tour tour);
    }
}