using TourPlanner.Domain.Model;

namespace BusinessLayer.Interfaces
{
    public interface ICreateTourReportService
    {
        event Action<Tour> ReportCreated;

        Task CreateTourReport(Tour tour, string path);
        Task CreateTourSummary(string path);
        //Task<double> CalculateChildFriendliness(Tour tour);
    }
}
