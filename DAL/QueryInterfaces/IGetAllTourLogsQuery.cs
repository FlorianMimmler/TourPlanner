using TourPlanner.Domain.Model;

namespace TourPlanner.DAL.QueryInterfaces
{
    public interface IGetAllTourLogsQuery
    {
        Task<IEnumerable<TourLog>> ExecuteAsync();
    }
}