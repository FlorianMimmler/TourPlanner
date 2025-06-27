using TourPlanner.Domain.Model;

namespace DAL.QueryInterfaces
{
    public interface ICreateTourQuery
    {
        Task<bool> ExecuteAsync(Tour tour);
    }
}