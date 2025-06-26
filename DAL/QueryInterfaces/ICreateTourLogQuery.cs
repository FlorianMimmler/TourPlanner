using TourPlanner.Domain.Model;

namespace DAL.QueryInterfaces
{
    public interface ICreateTourLogQuery
    {
        Task ExecuteAsync(TourLog tourLog);
    }
}