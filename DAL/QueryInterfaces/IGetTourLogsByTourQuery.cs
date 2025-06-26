using TourPlanner.Domain.Model;

namespace DAL.QueryInterfaces
{
    public interface IGetTourLogsByTourQuery
    {
        Task<IEnumerable<TourLog>> ExecuteAsync(Guid tourId);
    }
}