using PresentationLayer.Stores;
using TourPlanner.Domain.Model;
using TourPlanner.BusinessLayer.Services;

namespace PresentationLayer.ViewModel
{
    public class TabsViewModel
    {
        public GeneralViewModel GeneralViewModel { get; }

        public TabsViewModel(SelectedTourStore selectedTourStore, TourStatisticsService tourStatisticsService)
        {
            GeneralViewModel = new GeneralViewModel(selectedTourStore, tourStatisticsService);
        }
    }
}