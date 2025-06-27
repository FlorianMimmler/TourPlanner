using TourPlanner.Domain.Model;

namespace DAL.QueryInterfaces
{
    public interface IDeleteTourQuery
    {
        Task<bool> ExecuteAsync(Tour tour);
    }
}