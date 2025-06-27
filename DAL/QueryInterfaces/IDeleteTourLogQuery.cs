using TourPlanner.Domain.Model;

namespace DAL.QueryInterfaces
{
    public interface IDeleteTourLogQuery
    {
        Task<bool> ExecuteAsync(TourLog tourLog);
    }
}