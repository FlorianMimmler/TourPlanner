using TourPlanner;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.PresentationLayer.Stores;
using TourPlanner.PresentationLayer.ViewModel;

namespace TourPlanner_UnitTests
{
    [TestFixture]
    public class MainViewModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_ShouldInitializeViewModels()
        {
            // Act
            TourService TourService = new();
            TourLogService TourlogsService = new();
            SelectedTourStore SelectedTourStore = new(TourService);
            var viewModel = new MainViewModel(SelectedTourStore, TourService, TourlogsService);

            // Assert
            Assert.IsNotNull(viewModel.MainContentViewModel,
                "MainContentViewModel should be initialized in the constructor.");
            Assert.IsNotNull(viewModel.SidebarViewModel, "SidebarViewModel should be initialized in the constructor.");
        }
    }
}