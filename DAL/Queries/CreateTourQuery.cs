using Microsoft.EntityFrameworkCore;
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
    public class CreateTourQuery : ICreateTourQuery
    {
        private readonly TourPlannerDbContextFactory _contextFactory;

        public CreateTourQuery(TourPlannerDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task ExecuteAsync(Tour tour)
        {
            using var context = _contextFactory.Create();

            var tourDTO = new TourDto
            {
                Id = tour.Id,
                Name = tour.Name,
                Description = tour.Description,
                From = tour.From,
                To = tour.To,
                TransportType = TransportTypeHelper.TransportTypeToString(tour.TransportType),
                Distance = tour.Distance,
                EstimatedTime = tour.EstimatedTime,
                Image = tour.Image
            };

            context.Tours.Add(tourDTO);
            await context.SaveChangesAsync();

        }
    }
}
