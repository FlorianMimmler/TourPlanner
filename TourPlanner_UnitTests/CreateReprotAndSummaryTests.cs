using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using NSubstitute;
using PresentationLayer.Stores;
using PresentationLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain.Model;

namespace TourPlanner_UnitTests
{
    [TestFixture]
    public class CreateReprotAndSummaryTests
    {
        private ITourLogService _mockTourLogService;
        private ITourService _mockTourService;
        private CreateTourReportService _createTourReportService;
        private SelectedTourStore _selectedTourStore;
        private ToursListViewModel _viewModel;
        private ITourStatisticsService _mockTourStatisticsService;

        private TourStatisticsService _tourStatisticsService;
        private SidebarHeaderViewModel _sideHeaderViewModel;




        private List<Tour> tours1;

        private List<TourLog> tourLogsOfTourOne;
        private List<TourLog> tourLogsOfTourTwo;


        [SetUp]
        public void Setup()
        {
            _mockTourService = Substitute.For<ITourService>();
            _mockTourLogService = Substitute.For<ITourLogService>();
            _mockTourStatisticsService = Substitute.For < ITourStatisticsService>();
            _tourStatisticsService = new TourStatisticsService(_mockTourLogService);
            //_selectedTourStore = new SelectedTourStore(_mockTourService);

            tours1 = new List<Tour>
            {
                new Tour { Id = Guid.NewGuid(), Name = "Tour A", Description="test description", Distance = 10, EstimatedTime= "10", From = "Wien", To="Graz", TransportType = TransportType.Bike},
                new Tour { Id = Guid.NewGuid(), Name = "Tour B", Description="test description", Distance = 10, EstimatedTime= "10", From = "Wien", To="Graz", TransportType = TransportType.Bike}
            };


            tourLogsOfTourOne = new List<TourLog>
            {
                new TourLog {Id = Guid.NewGuid(), Date = DateTime.Now, Comment = "TestComment", Difficulty = 10.5, Distance = 11.5, Duration = "100", Rating =12.5, TourId = tours1[0].Id },
                new TourLog {Id = Guid.NewGuid(), Date = DateTime.Now, Comment = "TestComment2", Difficulty = 13.5, Distance = 14.5, Duration = "120", Rating =15.5, TourId = tours1[0].Id }

            };

            tourLogsOfTourTwo = new List<TourLog>
            {
                new TourLog {Id = Guid.NewGuid(), Date = DateTime.Now, Comment = "TestComment", Difficulty = 10.5, Distance = 11.5, Duration = "100", Rating =12.5, TourId = tours1[1].Id },
                new TourLog {Id = Guid.NewGuid(), Date = DateTime.Now, Comment = "TestComment2", Difficulty = 13.5, Distance = 14.5, Duration = "120", Rating =15.5, TourId = tours1[1].Id }

            };



            _mockTourService.GetTours().Returns(tours1);
            _mockTourLogService.GetTourlogsByTour(tours1[0].Id).Returns(tourLogsOfTourOne);
            _mockTourLogService.GetTourlogsByTour(tours1[1].Id).Returns(tourLogsOfTourOne);
            


            /*
                double childfriendlyness = await _tourStatistic.CalculateChildFriendliness(tour);
                double popularity = await _tourStatistic.CalculatePopularity(tour);
                _tourLogs = await _tourLogService.GetTourlogsByTour(tour.Id);
            
             */

            _createTourReportService = new CreateTourReportService(_mockTourService, _mockTourLogService, _tourStatisticsService);
        }

        [Test]
        public async Task CreateTourReport_ShouldGenerateTourReportPdf()
        {
            // Arrange
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
            Directory.CreateDirectory(path);


            Tour test = tours1[0];

            // Act
            await _createTourReportService.CreateTourReport(test, path);
            // Assert
            Assert.True(File.Exists(path + "/" + "Tour AReport.pdf"));
            // Cleanup
            File.Delete(path + "/" + "Tour AReport.pdf");
            Directory.Delete(path);
        }





        [Test]

        public async Task CreateTourSummary_ShouldGenerateTourSummaryPdf()
        {
            // Arrange
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
            Directory.CreateDirectory(path);
            //act
            await _createTourReportService.CreateTourSummary(path);
            // Assert
            Assert.True(File.Exists(path + "/" + "TourSummary.pdf"));
            // Cleanup
            File.Delete(path + "/" + "TourSummary.pdf");
            Directory.Delete(path);


        }

    }
}
