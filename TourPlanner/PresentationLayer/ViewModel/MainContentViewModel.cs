using TourPlanner.BusinessLayer.Services;
using TourPlanner.PresentationLayer.Stores;
using TourPlanner.PresentationLayer.View.Subviews.Maincontent;

namespace TourPlanner.PresentationLayer.ViewModel
{
    public class MainContentViewModel
    {
        public TabsViewModel TabsViewModel { get;}

        public MainContentViewModel(SelectedTourStore selectedTourStore)
        {
            TabsViewModel = new TabsViewModel(selectedTourStore);
        }
    }
}