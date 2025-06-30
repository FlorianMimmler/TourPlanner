using BusinessLayer.Interfaces;
using TourPlanner.Domain.Model;

namespace BusinessLayer.Services
{
    public class TourStatisticsService : ITourStatisticsService
    {

        private ITourLogService _tourLogService;

        public TourStatisticsService(ITourLogService tourLogService)
        {
            _tourLogService = tourLogService;
        }

        public async Task<double> CalculatePopularity(Tour tour)
        {
            if (tour == null)
            {
                //throw new ArgumentNullException(nameof(tour), "Tour cannot be null");
                return 0;
            }
            var tourlogs = await _tourLogService.GetTourlogsByTour(tour.Id);

            var tourlogsCount = tourlogs.Count();

            double popularity = tourlogsCount / 2;

            return Math.Min(Math.Round(popularity, 1), 10);
        }

        public async Task<double> CalculateChildFriendliness(Tour tour)
        {
            if (tour == null)
            {
                //throw new ArgumentNullException(nameof(tour), "Tour cannot be null");
                return 0;
            }
            var tourlogs = await _tourLogService.GetTourlogsByTour(tour.Id);

            if (tourlogs.Count() == 0)
            {
                return 0;
            }

            double avgDifficulty = tourlogs.Average(log => log.Difficulty);
            double avgTime = tourlogs
                .Select(log => ParseTimeToMinutes(log.Duration))
                .Average();
            double avgDistance = tourlogs.Average(log => log.Distance);

            double difficultyScore = (5 - avgDifficulty) / 4.0 * 4;

            double timeRatio = ParseTimeToMinutes(tour.EstimatedTime) > 0
                ? ParseTimeToMinutes(tour.EstimatedTime) / avgTime
                : 1.0;
            double timeScore = Math.Min(1.0, timeRatio) * 3;

            double distanceRatio = tour.Distance > 0
                ? tour.Distance / avgDistance
                : 1.0;
            double distanceScore = Math.Min(1.0, distanceRatio) * 3;

            double totalScore = (difficultyScore + timeScore + distanceScore);

            return (double)Math.Round(Math.Clamp(totalScore, 0, 10), 1);
        }

        private static int ParseTimeToMinutes(string time)
        {
            if (TimeSpan.TryParse(time, out TimeSpan ts))
            {
                return (int)ts.TotalMinutes;
            }
            return 0;
        }

    }
}
