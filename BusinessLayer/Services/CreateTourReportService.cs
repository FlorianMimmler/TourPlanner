using BusinessLayer.Interfaces;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
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

        private async Task<bool> WaitForTourImage(Guid tourId)
        {
            var filepath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TourMapImages", tourId.ToString() + "_image.png");

            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10));
            var waitTask = Task.Run(async () =>
            {
                while (!File.Exists(filepath))
                {
                    await Task.Delay(10);
                }
            });

            var completedTask = await Task.WhenAny(waitTask, timeoutTask);

            return completedTask == waitTask;
        }

        public async Task CreateTourReport(Tour tour,string path)
        {

            IEnumerable<TourLog> _tourLogs;
            using PdfWriter writer = new(path+"/"+tour.Name + "Report.pdf");
            using PdfDocument pdf = new(writer);
            using iText.Layout.Document report = new iText.Layout.Document(pdf);

            Paragraph header = new Paragraph("REPORT: " + tour.Name).SetFontSize(24).SetFontColor(ColorConstants.RED);
            report.Add(header);

            byte[]? image = null;

            if(await WaitForTourImage(tour.Id))
            {
                image = File.ReadAllBytes(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TourMapImages", tour.Id.ToString() + "_image.png"));
            }

            if(image != null)
            {
                ImageData imageData = ImageDataFactory.Create(image);
                Image mapImage = new Image(imageData);
                mapImage.SetAutoScale(true);
                report.Add(new Paragraph("Tour Map:").SetFontSize(16).SetMarginTop(20));
                report.Add(mapImage);
            } else
            {
                report.Add(new Paragraph("Tour Map:").SetFontSize(16).SetMarginTop(14));
                report.Add(new Paragraph("No Image found").SetFontSize(12));
            }
            
            var description = new Paragraph(""

                + "\n\nTo: " + tour.To
                + "\nFrom: " + tour.From
                + "\nEstimated Time: " + tour.EstimatedTime.ToString()
                + "\nDistance: " + tour.Distance.ToString()
                + "\nMode of Transport: " + tour.TransportType.ToString()
                + "\nTour Description: " + tour.Description +"\n\n\n"
                
                ).SetFontSize(14);
            
            report.Add(description);

            Paragraph tourLogTableHeader = new Paragraph("Tourlogs:").SetFontSize(16);

            report.Add(tourLogTableHeader);

            Table tourLogTable = new Table(UnitValue.CreatePercentArray(8)).UseAllAvailableWidth();


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

        public async Task CreateTourSummary(string path)
        {

            IEnumerable<TourLog> _tourLogs;




            int logCount = 0;

            using PdfWriter writer = new(path + "/" + "TourSummary.pdf");
            using PdfDocument pdf = new(writer);
            PageSize landscape = PageSize.A4.Rotate();                              // put pdf in lanscape mode
            using iText.Layout.Document summary = new iText.Layout.Document(pdf,landscape);
  
            Paragraph header = new Paragraph("Tours Summary: ").SetFontSize(24).SetFontColor(ColorConstants.RED);
            summary.Add(header);

            Paragraph description = new Paragraph("Various Analysis Paramters can be found in the following table").SetFontSize(14);
            summary.Add(description);

            Paragraph tourLogTableHeader = new Paragraph("Tourlogs:");
            //Table tourLogTable = new Table(UnitValue.CreatePercentArray(8)).UseAllAvailableWidth();
            Table tourLogTable = new Table(UnitValue.CreatePercentArray(new float[] { 10, 20, 10, 10, 10, 10, 10,10,10 })).UseAllAvailableWidth();
            tourLogTable.SetFixedLayout();

            //Column titles
            tourLogTable.AddHeaderCell("Tour Name");
            tourLogTable.AddHeaderCell("ID");
            tourLogTable.AddHeaderCell("AVG. Difficulty");
            tourLogTable.AddHeaderCell("AVG. Distance in km");
            tourLogTable.AddHeaderCell("AVG. Duration in minutes");
            tourLogTable.AddHeaderCell("AVG. Rating");
            tourLogTable.AddHeaderCell("Childfriendlyness");
            tourLogTable.AddHeaderCell("Poplarity");
            tourLogTable.AddHeaderCell("Log Count");

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
            tourLogTable.AddCell(Math.Round(averageDifficulty,2).ToString() ?? "");
            tourLogTable.AddCell(Math.Round(averageDistance,2).ToString() ?? "");
            tourLogTable.AddCell(Math.Round(averageDuration, 2).ToString() ?? "");
            tourLogTable.AddCell(Math.Round(averageRating, 2).ToString() ?? "");
            tourLogTable.AddCell(Math.Round(childfriendlyness, 2).ToString() ?? "");
            tourLogTable.AddCell(Math.Round(popularity, 2).ToString() ?? "");
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

