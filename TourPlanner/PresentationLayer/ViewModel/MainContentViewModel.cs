using TourPlanner.BusinessLayer.Services;
using TourPlanner.PresentationLayer.Stores;
using TourPlanner.PresentationLayer.View.Subviews.Maincontent;

namespace TourPlanner.PresentationLayer.ViewModel
{
    public class MainContentViewModel
    {
        public TabsViewModel TabsViewModel { get;}
        public TourlogsViewModel TourlogsViewModel { get; }

        public MainContentViewModel(SelectedTourStore selectedTourStore, ITourService tourService, TourLogService tourlogsService)
        {
            TabsViewModel = new TabsViewModel(selectedTourStore);
            TourlogsViewModel = new TourlogsViewModel(tourService, tourlogsService, selectedTourStore);
        }
    }
}