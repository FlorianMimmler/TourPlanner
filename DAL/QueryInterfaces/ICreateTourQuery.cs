using TourPlanner.Domain.Model;

namespace TourPlanner.DAL.QueryInterfaces
{
    public interface ICreateTourQuery
    {
        Task ExecuteAsync(Tour tour);
    }
}