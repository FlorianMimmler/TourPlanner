using PresentationLayer.Stores;
using TourPlanner.Domain.Model;
using BusinessLayer.Services;

namespace PresentationLayer.ViewModel
{
    public class TabsViewModel
    {
        public GeneralViewModel GeneralViewModel { get; }
        public MapViewModel MapViewModel { get; }

        public TabsViewModel(SelectedTourStore selectedTourStore, TourStatisticsService tourStatisticsService, IMapService mapService)
        {
            GeneralViewModel = new GeneralViewModel(selectedTourStore, tourStatisticsService);
            MapViewModel = new MapViewModel(mapService, selectedTourStore);
        }
    }
}