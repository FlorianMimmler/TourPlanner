using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain.Model;
using TourPlanner.DAL.DTOs;
using TourPlanner.DAL.QueryInterfaces;

namespace TourPlanner.DAL.Queries
{
    public class CreateTourLogQuery : ICreateTourLogQuery
    {

        private readonly TourPlannerDbContextFactory _contextFactory;

        public CreateTourLogQuery(TourPlannerDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task ExecuteAsync(TourLog tourLog)
        {
            using var context = _contextFactory.Create();

            var tourLogDTO = new TourLogDto
            {
                Id = tourLog.Id,
                TourId = tourLog.TourId,
                Date = tourLog.Date,
                Duration = tourLog.Duration,
                Distance = tourLog.Distance,
                Comment = tourLog.Comment,
                Difficulty = tourLog.Difficulty,
                Rating = tourLog.Rating
            };

        }

    }
}
