using TourPlanner.Domain.Model;

namespace DAL.QueryInterfaces
{
    public interface ICreateTourQuery
    {
        Task ExecuteAsync(Tour tour);
    }
}