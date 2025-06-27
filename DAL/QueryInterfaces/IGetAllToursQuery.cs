using TourPlanner.Domain.Model;

namespace DAL.QueryInterfaces
{
    public interface IGetAllToursQuery
    {
        Task<IEnumerable<Tour>> ExecuteAsync();
    }
}