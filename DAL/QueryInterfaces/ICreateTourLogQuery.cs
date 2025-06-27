using TourPlanner.Domain.Model;

namespace DAL.QueryInterfaces
{
    public interface ICreateTourLogQuery
    {
        Task<bool> ExecuteAsync(TourLog tourLog);
    }
}