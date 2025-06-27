using TourPlanner.Domain.Model;

namespace DAL.QueryInterfaces
{
    public interface IUpdateTourLogQuery
    {
        Task<bool> ExecuteAsync(TourLog tourLog);
    }
}