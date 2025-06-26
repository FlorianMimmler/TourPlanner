using PresentationLayer.Stores;
using TourPlanner.BusinessLayer.Services;
using PresentationLayer.View.Subviews.Maincontent;

namespace PresentationLayer.ViewModel
{
    public class MainContentViewModel
    {
        public TabsViewModel TabsViewModel { get;}
        public TourlogsViewModel TourlogsViewModel { get; }

        public MainContentViewModel(SelectedTourStore selectedTourStore, ITourService tourService, TourLogService tourlogsService, TourStatisticsService tourStatisticsService)
        {
            TabsViewModel = new TabsViewModel(selectedTourStore, tourStatisticsService);
            TourlogsViewModel = new TourlogsViewModel(tourService, tourlogsService, selectedTourStore);
        }
    }
}