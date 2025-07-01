using PresentationLayer.Stores;
using BusinessLayer.Services;
using PresentationLayer.View.Subviews.Maincontent;
using BusinessLayer.Interfaces;

namespace PresentationLayer.ViewModel
{
    public class MainContentViewModel
    {
        public TabsViewModel TabsViewModel { get;}
        public TourlogsViewModel TourlogsViewModel { get; }

        public MainContentViewModel(SelectedTourStore selectedTourStore, ITourService tourService, ITourLogService tourlogsService, ITourStatisticsService tourStatisticsService, IMapService mapService, SelectedTabStore selectedTabStore)
        {
            TabsViewModel = new TabsViewModel(selectedTourStore, tourStatisticsService, mapService, selectedTabStore);
            TourlogsViewModel = new TourlogsViewModel(tourService, tourlogsService, selectedTourStore);
        }
    }
}