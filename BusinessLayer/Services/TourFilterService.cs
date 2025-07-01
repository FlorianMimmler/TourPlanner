using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain.Model;

namespace BusinessLayer.Services
{
    public class TourFilterService : ITourFilterService
    {
        private readonly ITourService _tourService;
        private readonly ITourLogService _tourLogService;
        private readonly ITourStatisticsService _tourStatisticsService;

        public TourFilterService(ITourService tourService, ITourLogService tourLogService, ITourStatisticsService tourStatisticsService)
        {
            _tourService = tourService;
            _tourLogService = tourLogService;
            _tourStatisticsService = tourStatisticsService;
        }

        public async Task<IEnumerable<Tour>> FilterToursAsync(string searchTerm)
        {
            var tours = await _tourService.GetTours();
            var lowerSearchTerm = searchTerm.ToLower();

            var result = new List<Tour>();

            foreach (var tour in tours)
            {
                var logs = await _tourLogService.GetTourlogsByTour(tour.Id);
                var popularity = await _tourStatisticsService.CalculatePopularity(tour);
                var childFriendliness = await _tourStatisticsService.CalculateChildFriendliness(tour);

                var matches =
                    tour.Name.ToLower().Contains(lowerSearchTerm) ||
                    tour.Description.ToLower().Contains(lowerSearchTerm) ||
                    logs.Any(log => log.Comment.ToLower().Contains(lowerSearchTerm)) ||
                    popularity.ToString().Contains(lowerSearchTerm) ||
                    childFriendliness.ToString().Contains(lowerSearchTerm);

                if (matches)
                    result.Add(tour);
            }

            return result;
        }
    }
}
