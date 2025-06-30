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
    public class TourLogServiceTests
    {
        private readonly IGetAllTourLogsQuery _getAllTourLogsQuery;
        private readonly ICreateTourLogQuery _createTourLogQuery;
        private readonly IGetTourLogsByTourQuery _getTourLogsByTourQuery;
        private readonly IUpdateTourLogQuery _updateTourLogQuery;
        private readonly IDeleteTourLogQuery _deleteTourLogQuery;
        private readonly ILoggerWrapper _logger;

        private readonly TourLogService _service;


        public TourLogServiceTests()
        {
            _getAllTourLogsQuery = Substitute.For<IGetAllTourLogsQuery>();
            _createTourLogQuery = Substitute.For<ICreateTourLogQuery>();
            _getTourLogsByTourQuery = Substitute.For<IGetTourLogsByTourQuery>();
            _updateTourLogQuery = Substitute.For<IUpdateTourLogQuery>();
            _deleteTourLogQuery = Substitute.For<IDeleteTourLogQuery>();
            _logger = Substitute.For<ILoggerWrapper>();

            _service = new TourLogService(_getAllTourLogsQuery, _createTourLogQuery, _getTourLogsByTourQuery, _updateTourLogQuery, _deleteTourLogQuery, _logger);
        }

        [Test]
        public async Task GetTourlogs_CallsQueryAndReturnsResult()
        {
            var expected = new List<TourLog> { new TourLog() };
            _getAllTourLogsQuery.ExecuteAsync().Returns(expected);

            var result = await _service.GetTourlogs();

            Assert.That(expected, Is.EqualTo(result), "TourLogService should return all tourLogs");
        }

        [Test]
        public async Task GetTourlogsByTour_CallsQueryAndReturnsResult()
        {
            var tourId = Guid.NewGuid();
            var expected = new List<TourLog> { new TourLog() };
            _getTourLogsByTourQuery.ExecuteAsync(tourId).Returns(expected);

            var result = await _service.GetTourlogsByTour(tourId);

            Assert.That(expected, Is.EqualTo(result), "TourLogService should return all tourLogs");
        }

        [Test]
        public async Task AddTourLog_Success_LogsAndRaisesEvent()
        {
            var log = new TourLog { Id = Guid.NewGuid() };
            _createTourLogQuery.ExecuteAsync(log).Returns(true);

            TourLog added = null;
            _service.TourLogAdded += l => added = l;

            await _service.AddTourLog(log);

            _logger.Received().Info($"New tourlog added: {log.Id}");
            Assert.That(log, Is.EqualTo(added), "TourLogService should return correct tour after adding");
        }

        [Test]
        public async Task AddTourLog_Exception_LogsError()
        {
            var log = new TourLog();
            _createTourLogQuery.ExecuteAsync(log).Returns<Task<bool>>(_ => throw new Exception("fail"));

            await _service.AddTourLog(log);

            _logger.Received().Error("fail");
        }

        [Test]
        public async Task UpdateTourLog_Success_LogsAndRaisesEvent()
        {
            var log = new TourLog { Id = Guid.NewGuid() };
            _updateTourLogQuery.ExecuteAsync(log).Returns(true);

            TourLog updated = null;
            _service.TourLogUpdated += l => updated = l;

            await _service.UpdateTourLog(log);

            _logger.Received().Info($"Tourlog updated: {log.Id}");
            Assert.That(log, Is.EqualTo(updated), "TourLogService should return correct tourlog after updating");
        }

        [Test]
        public async Task UpdateTourLog_Exception_LogsError()
        {
            var log = new TourLog();
            _updateTourLogQuery.ExecuteAsync(log).Returns<Task<bool>>(_ => throw new Exception("fail"));

            await _service.UpdateTourLog(log);

            _logger.Received().Error("fail");
        }

        [Test]
        public async Task DeleteTourLog_Success_LogsAndRaisesEvent()
        {
            var log = new TourLog { Id = Guid.NewGuid() };
            _deleteTourLogQuery.ExecuteAsync(log).Returns(true);

            Guid deletedId = Guid.Empty;
            _service.TourLogDeleted += id => deletedId = id;

            await _service.DeleteTourLog(log);

            _logger.Received().Info($"Tourlog deleted: {log.Id}");
            Assert.That(log.Id, Is.EqualTo(deletedId), "TourLogService should return correct id after deleting");
        }

        [Test]
        public async Task DeleteTourLog_Exception_LogsError()
        {
            var log = new TourLog();
            _deleteTourLogQuery.ExecuteAsync(log).Returns<Task<bool>>(_ => throw new Exception("fail"));

            await _service.DeleteTourLog(log);

            _logger.Received().Error("fail");
        }
    }
}
