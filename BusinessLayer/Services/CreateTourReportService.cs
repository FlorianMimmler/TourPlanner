using BusinessLayer.Interfaces;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NetTopologySuite.Index.HPRtree;
using Npgsql.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain.Model;


namespace BusinessLayer.Services
{
    public class CreateTourReportService : ICreateTourReportService
    {

        private ITourService _tourService;
        private ITourLogService _tourLogService;
        private TourStatisticsService _tourStatistic;
        
        private IEnumerable<Tour> _tour;
        private IEnumerable<Tour> _tours;


        public event Action<Tour> ReportCreated;

        
        public string path = "";

        public CreateTourReportService(ITourService tourService, ITourLogService tourLogService,TourStatisticsService tourStatistics)
        {
            _tourService = tourService;
            _tourLogService = tourLogService;
            _tourStatistic = tourStatistics;

        }

        public async Task CreateTourReport(Tour tour,string path)
        {

            IEnumerable<TourLog> _tourLogs;
            using PdfWriter writer = new(path+"/"+tour.Name + "Report.pdf");
            using PdfDocument pdf = new(writer);
            using iText.Layout.Document report = new iText.Layout.Document(pdf);

            Paragraph header = new Paragraph("REPORT: " + tour.Name).SetFontSize(24).SetFontColor(ColorConstants.RED);
            report.Add(header);

            Paragraph description = new Paragraph("All tour Logs of tour " + tour.Name + ":").SetFontSize(14);
            report.Add(description);

            Paragraph tourLogTableHeader = new Paragraph("Tourlogs:");
            iText.Layout.Element.Table tourLogTable = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(8)).UseAllAvailableWidth();


            //Column titles
            tourLogTable.AddHeaderCell("TourLogID");
            tourLogTable.AddHeaderCell("Date");
            tourLogTable.AddHeaderCell("Comment");
            tourLogTable.AddHeaderCell("Difficulty");
            tourLogTable.AddHeaderCell("Distance");
            tourLogTable.AddHeaderCell("Duration");
            tourLogTable.AddHeaderCell("Rating");
            tourLogTable.AddHeaderCell("TourID");

            tourLogTable.SetFontSize(14).SetBackgroundColor(ColorConstants.WHITE);





            _tourLogs = await _tourLogService.GetTourlogsByTour(tour.Id); // feth all tourlogs of tour

            foreach (var item in _tourLogs)
            {

                tourLogTable.AddCell(item.Id.ToString() ?? "");
                tourLogTable.AddCell(item.Date.ToString("yyyy-MM-dd HH:mm") ?? "");
                tourLogTable.AddCell(item.Comment ?? "");
                tourLogTable.AddCell(item.Difficulty.ToString() ?? "");
                tourLogTable.AddCell(item.Distance.ToString() ?? "");
                tourLogTable.AddCell(item.Duration.ToString() ?? "");
                tourLogTable.AddCell(item.Rating.ToString() ?? "");
                tourLogTable.AddCell(item.TourId.ToString() ?? "");
            }

            report.Add(tourLogTable);

            report.Close();

        }

        public async Task CreateSummary(string path)
        {

            IEnumerable<TourLog> _tourLogs;




            int logCount = 0;

            using PdfWriter writer = new(path + "/" + "TourSummary.pdf");
            using PdfDocument pdf = new(writer);
            using iText.Layout.Document summary = new iText.Layout.Document(pdf);

            Paragraph header = new Paragraph("Tours Summary: ").SetFontSize(24).SetFontColor(ColorConstants.RED);
            summary.Add(header);

            Paragraph description = new Paragraph("Various Analysis Paramters can be found in the following table").SetFontSize(14);
            summary.Add(description);

            Paragraph tourLogTableHeader = new Paragraph("Tourlogs:");
            iText.Layout.Element.Table tourLogTable = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(8)).UseAllAvailableWidth();


            //Column titles
            tourLogTable.AddHeaderCell("Name of tour");
            tourLogTable.AddHeaderCell("ID");
            tourLogTable.AddHeaderCell("AVG. Difficulty");
            tourLogTable.AddHeaderCell("AVG. Distance in km");
            tourLogTable.AddHeaderCell("AVG. Duration in minutes");
            tourLogTable.AddHeaderCell("AVG. Rating");
            tourLogTable.AddHeaderCell("Childfriendlyness");
            tourLogTable.AddHeaderCell("Poplarity");
            tourLogTable.AddHeaderCell("Amount of Logs");

            tourLogTable.SetFontSize(14).SetBackgroundColor(ColorConstants.WHITE);




            foreach (var tour in await _tourService.GetTours())
            {

                double childfriendlyness = await _tourStatistic.CalculateChildFriendliness(tour);
                double popularity = await _tourStatistic.CalculatePopularity(tour);
                _tourLogs = await _tourLogService.GetTourlogsByTour(tour.Id); // feth all tourlogs of tour
                double averageDuration = 0;
                double averageDistance = 0;
                double averageRating = 0;
                double averageDifficulty = 0;

                logCount = _tourLogs.Count<TourLog>();

                // calculate average from tourlogs
                foreach (var item in _tourLogs) 
            {
                averageDistance += item.Distance;
                averageDuration += ParseTimeToMinutes(item.Duration);
                averageRating += item.Rating;
                averageDifficulty += item.Difficulty;
            }

            averageDifficulty = averageDifficulty / logCount;
            averageDistance = averageDistance / logCount;
            averageDuration = averageDuration / logCount;
            averageDifficulty = averageDifficulty / logCount;



                // add values to according cells 
            tourLogTable.AddCell(tour.Name?? "");
            tourLogTable.AddCell(tour.Id.ToString() ?? "");
            tourLogTable.AddCell(averageDifficulty.ToString() ?? "");
            tourLogTable.AddCell(averageDistance.ToString() ?? "");
            tourLogTable.AddCell(averageDuration.ToString() ?? "");
            tourLogTable.AddCell(averageRating.ToString() ?? "");
            tourLogTable.AddCell(childfriendlyness.ToString() ?? "");
            tourLogTable.AddCell(popularity.ToString() ?? "");
            tourLogTable.AddCell(logCount.ToString() ?? "");
            }

            summary.Add(tourLogTable);

            summary.Close();



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

