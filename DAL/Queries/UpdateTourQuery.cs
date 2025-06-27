using DAL.DTOs;
using DAL.QueryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain.Model;

namespace DAL.Queries
{
    public class UpdateTourQuery : DbQueryBase, IUpdateTourQuery
    {

        public UpdateTourQuery(TourPlannerDbContextFactory contextFactory) : base(contextFactory) { }

        public async Task<bool> ExecuteAsync(Tour tour)
        {

            using var context = ContextFactory.Create();

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

            context.Tours.Update(tourDTO);

            return await context.SaveChangesAsync() == 1;

        }

    }
}
