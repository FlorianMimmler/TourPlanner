using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class CreateTourReportService
    {

        private ITourService _tourService;
        private ITourLogService _tourLogService;
        private IEnumerable<TourLog> _tourLogs;

        public event Action<Tour> ReportCreated;


        public string path = "";

        public CreateTourReportService(ITourService tourService, ITourLogService tourLogService)
        {
            _tourService = tourService;
            _tourLogService = tourLogService;
        }

        public async Task CreateTourReport(Tour tour)
        {

            using PdfWriter writer = new(tour.Name + "Report.pdf");
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
    }
}
