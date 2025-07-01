using PresentationLayer.Stores;
using TourPlanner.Domain.Model;
using BusinessLayer.Services;
using BusinessLayer.Interfaces;
using Domain.Model;

namespace PresentationLayer.ViewModel
{
    public class TabsViewModel: ViewModelBase
    {
        public GeneralViewModel GeneralViewModel { get; }
        public MapViewModel MapViewModel { get; }

        private readonly SelectedTabStore _selectedTabStore;

        public TabsViewModel(SelectedTourStore selectedTourStore, ITourStatisticsService tourStatisticsService, IMapService mapService, SelectedTabStore selectedTabStore)
        {
            GeneralViewModel = new GeneralViewModel(selectedTourStore, tourStatisticsService);
            MapViewModel = new MapViewModel(mapService, selectedTourStore);
            _selectedTabStore = selectedTabStore;
            _selectedTabIndex = 0;

            _selectedTabStore.SelectedTabChanged += SelectedTabChanged;
        }

        private void SelectedTabChanged()
        {
            SelectedTabIndex = (int)_selectedTabStore.SelectedTab;
        }

        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                if (_selectedTabIndex != value)
                {
                    _selectedTabIndex = value;
                    _selectedTabStore.SelectedTab = (TabType)_selectedTabIndex;
                    OnPropertyChanged(nameof(SelectedTabIndex));
                }
            }
        }
    }
}