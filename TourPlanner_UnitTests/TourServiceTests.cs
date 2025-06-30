using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DAL.QueryInterfaces;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain.Model;

namespace TourPlanner_UnitTests
{
    [TestFixture]
    public class TourServiceTests
    {
        private readonly IGetAllToursQuery _getAllToursQuery;
        private readonly ICreateTourQuery _createTourQuery;
        private readonly IUpdateTourQuery _updateTourQuery;
        private readonly IDeleteTourQuery _deleteTourQuery;
        private readonly ILoggerWrapper _logger;

        private readonly TourService _tourService;

        public TourServiceTests()
        {
            _getAllToursQuery = Substitute.For<IGetAllToursQuery>();
            _createTourQuery = Substitute.For<ICreateTourQuery>();
            _updateTourQuery = Substitute.For<IUpdateTourQuery>();
            _deleteTourQuery = Substitute.For<IDeleteTourQuery>();
            _logger = Substitute.For<ILoggerWrapper>();

            _tourService = new TourService(_getAllToursQuery, _createTourQuery, _updateTourQuery, _deleteTourQuery, _logger);
        }

        [Test]
        public async Task GetTours_CallsGetAllToursQuery()
        {
            // Arrange
            var expectedTours = new List<Tour> { new Tour { Id = Guid.NewGuid() } };
            _getAllToursQuery.ExecuteAsync().Returns(expectedTours);

            // Act
            var result = await _tourService.GetTours();

            // Assert
            Assert.That(expectedTours, Is.EqualTo(result), "TourService should return all tours");
        }

        [Test]
        public async Task AddTour_Successful_AddsTourAndLogs()
        {
            // Arrange
            var tour = new Tour { Id = Guid.NewGuid() };
            _createTourQuery.ExecuteAsync(tour).Returns(true);

            Tour addedTour = null;
            _tourService.TourAdded += t => addedTour = t;

            // Act
            await _tourService.AddTour(tour);

            // Assert
            _logger.Received().Info($"New tour added: {tour.Id}");
            Assert.That(tour, Is.EqualTo(addedTour), "TourService addTour event should return the same tour");
        }

        [Test]
        public async Task AddTour_Failure_DoesNotTriggerEvent()
        {
            // Arrange
            var tour = new Tour { Id = Guid.NewGuid() };
            _createTourQuery.ExecuteAsync(tour).Returns(false);

            var eventCalled = false;
            _tourService.TourAdded += _ => eventCalled = true;

            // Act
            await _tourService.AddTour(tour);

            // Assert
            Assert.False(eventCalled);
        }

        [Test]
        public async Task AddTour_Exception_LogsError()
        {
            // Arrange
            var tour = new Tour { Id = Guid.NewGuid() };
            _createTourQuery.ExecuteAsync(tour).Returns<Task<bool>>(_ => throw new Exception("Create failed"));

            // Act
            await _tourService.AddTour(tour);

            // Assert
            _logger.Received().Error("Create failed");
        }

        [Test]
        public async Task UpdateTour_Successful_TriggersEventAndLogs()
        {
            // Arrange
            var tour = new Tour { Id = Guid.NewGuid() };
            _updateTourQuery.ExecuteAsync(tour).Returns(true);

            Tour updatedTour = null;
            _tourService.TourUpdated += t => updatedTour = t;

            // Act
            await _tourService.UpdateTour(tour);

            // Assert
            _logger.Received().Info($"Tour updated: {tour.Id}");
            Assert.That(tour, Is.EqualTo(updatedTour), "TourService should update tour and return the same tour");
        }

        [Test]
        public async Task UpdateTour_Exception_LogsError()
        {
            var tour = new Tour { Id = Guid.NewGuid() };
            _updateTourQuery.ExecuteAsync(tour).Returns<Task<bool>>(_ => throw new Exception("Update failed"));

            await _tourService.UpdateTour(tour);

            _logger.Received().Error("Update failed");
        }

        [Test]
        public async Task DeleteTour_Successful_TriggersEventAndLogs()
        {
            var tour = new Tour { Id = Guid.NewGuid() };
            _deleteTourQuery.ExecuteAsync(tour).Returns(true);

            Guid deletedId = Guid.Empty;
            _tourService.TourDeleted += id => deletedId = id;

            await _tourService.DeleteTour(tour);

            _logger.Received().Info($"Tour deleted: {tour.Id}");
            Assert.That(tour.Id, Is.EqualTo(deletedId), "TourService should return the same tourId after deleting");
        }

        [Test]
        public async Task DeleteTour_Exception_LogsError()
        {
            var tour = new Tour { Id = Guid.NewGuid() };
            _deleteTourQuery.ExecuteAsync(tour).Returns<Task<bool>>(_ => throw new Exception("Delete failed"));

            await _tourService.DeleteTour(tour);

            _logger.Received().Error("Delete failed");
        }
    }
}
