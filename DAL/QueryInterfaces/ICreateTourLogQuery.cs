using TourPlanner.Domain.Model;

namespace TourPlanner.DAL.QueryInterfaces
{
    public interface ICreateTourLogQuery
    {
        Task ExecuteAsync(TourLog tourLog);
    }
}