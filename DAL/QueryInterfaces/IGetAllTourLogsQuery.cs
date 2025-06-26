using TourPlanner.Domain.Model;

namespace DAL.QueryInterfaces
{
    public interface IGetAllTourLogsQuery
    {
        Task<IEnumerable<TourLog>> ExecuteAsync();
    }
}