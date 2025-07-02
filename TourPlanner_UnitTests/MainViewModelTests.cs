using PresentationLayer.Stores;
using TourPlanner;
using BusinessLayer.Services;
using PresentationLayer.ViewModel;
using BusinessLayer.Interfaces;
using BusinessLayer.Logger;
using NSubstitute;

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
            ITourService _tourService = Substitute.For<ITourService>();
            ITourLogService _tourLogService = Substitute.For<ITourLogService>();
            SelectedTourStore _selectedTourStore = new SelectedTourStore(_tourService);
            SearchStore _searchStore = new SearchStore();
            SelectedTabStore _selectedTabStore = new SelectedTabStore();
            ITourStatisticsService _tourStatisticsService = Substitute.For<ITourStatisticsService>();
            ITourExportService _tourOutputService = Substitute.For<ITourExportService>();
            ITourImportService _tourImportService = Substitute.For<ITourImportService>();
            IMapService _mapService = Substitute.For<IMapService>();
            ICreateTourReportService _createReportService = Substitute.For<ICreateTourReportService>();
            ITourFilterService _tourFilterService = Substitute.For<ITourFilterService>();
            var viewModel = new MainViewModel(_selectedTourStore, _tourService, _tourLogService, _tourStatisticsService, _tourOutputService, _tourImportService, _mapService, _createReportService, _searchStore, _tourFilterService, _selectedTabStore);

            // Assert
            Assert.IsNotNull(viewModel.MainContentViewModel,
                "MainContentViewModel should be initialized in the constructor.");
            Assert.IsNotNull(viewModel.SidebarViewModel, "SidebarViewModel should be initialized in the constructor.");
            
        }
    }
}