using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.PresentationLayer.Stores;

namespace TourPlanner.PresentationLayer.ViewModel
{
    public class TabsViewModel
    {
        public GeneralViewModel GeneralViewModel { get; }

        public TabsViewModel(SelectedTourStore selectedTourStore)
        {
            this.GeneralViewModel = new GeneralViewModel(selectedTourStore);
        }
    }
}