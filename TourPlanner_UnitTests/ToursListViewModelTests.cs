using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;
using NSubstitute;
using PresentationLayer.ViewModel;
using PresentationLayer.Stores;

namespace TourPlanner_UnitTests
{
    [TestFixture]
    public class ToursListViewModelTests
    {

        private ITourService _mockTourService;
        private SelectedTourStore _selectedTourStore;
        private ToursListViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _mockTourService = Substitute.For<ITourService>();
            _selectedTourStore = new SelectedTourStore(_mockTourService);

            var tours = new List<Tour>
        {
            new Tour { Id = 1, Name = "Tour A" },
            new Tour { Id = 2, Name = "Tour B" }
        };

            _mockTourService.GetTours().Returns(tours);

            _viewModel = new ToursListViewModel(_selectedTourStore, _mockTourService);
        }

        [Test]
        public void Constructor_ShouldInitializeTours()
        {
            // Assert
            Assert.That(_viewModel.Tours, Is.Not.Null, "Tours collection should be initialized.");
            Assert.That(_viewModel.Tours.Count(), Is.EqualTo(2), "Tours collection should contain all initial tours.");
        }

        [Test]
        public void Selecting_Tour_ShouldUpdate_SelectedTourStore()
        {
            // Arrange
            var tourItem = _viewModel.Tours.First();

            // Act
            _viewModel.SelectedTour = tourItem;

            // Assert
            Assert.That(_selectedTourStore.SelectedTour, Is.EqualTo(tourItem.Tour),
                "SelectedTourStore should update when SelectedTour is set.");
        }

        [Test]
        public void TourService_TourAdded_ShouldAddTourToCollection()
        {
            // Arrange
            var newTour = new Tour { Id = 3, Name = "Tour C" };

            // Act
            _mockTourService.TourAdded += Raise.Event<Action<Tour>>(newTour);

            // Assert
            Assert.That(_viewModel.Tours.Any(t => t.Tour.Id == newTour.Id), Is.True,
                "Newly added tour should exist in the ViewModel's collection.");
        }

        [Test]
        public void TourService_TourUpdated_ShouldUpdateTourInCollection()
        {
            // Arrange
            var updatedTour = new Tour { Id = 1, Name = "Updated Tour A" };

            // Act
            _mockTourService.TourUpdated += Raise.Event<Action<Tour>>(updatedTour);

            // Assert
            var updatedItem = _viewModel.Tours.First(t => t.Tour.Id == 1);
            Assert.That(updatedItem.Tour.Name, Is.EqualTo("Updated Tour A"), "Tour name should be updated.");
        }

        [Test]
        public void TourService_TourDeleted_ShouldRemoveTourFromCollection()
        {
            // Arrange
            int idToDelete = 1;

            // Act
            _mockTourService.TourDeleted += Raise.Event<Action<int>>(idToDelete);

            // Assert
            Assert.That(_viewModel.Tours.Any(t => t.Tour.Id == idToDelete), Is.False,
                "Deleted tour should be removed from the collection.");
        }
    }
}
