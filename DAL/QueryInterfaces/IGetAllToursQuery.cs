using TourPlanner.Domain.Model;

namespace TourPlanner.DAL.QueryInterfaces
{
    public interface IGetAllToursQuery
    {
        Task<IEnumerable<Tour>> ExecuteAsync();
    }
}