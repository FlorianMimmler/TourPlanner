using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TourPlanner.Domain.Model;
using System.Diagnostics.Metrics;
using System.Windows;
using TourPlanner.PresentationLayer.View.Subviews.Maincontent;
using TourPlanner.BusinessLayer.Services;
using PresentationLayer.Stores;

namespace PresentationLayer.ViewModel
{
    public class GeneralViewModel : ViewModelBase
    {

        private SelectedTourStore _selectedTourStore;
        private TourStatisticsService _tourStatisticsService;
        public Tour SelectedTour => _selectedTourStore.SelectedTour;
        public string Name => SelectedTour?.Name ?? "unknown";
        public string Description => SelectedTour?.Description ?? "unknown";
        public string From => SelectedTour?.From ?? "unknown";
        public string To => SelectedTour?.To ?? "unknown";
        public TransportType TransportType => SelectedTour?.TransportType ?? TransportType.Unknown;
        public string Distance => SelectedTour?.Distance.ToString() ?? "0";
        public string EstimatedTime => SelectedTour?.EstimatedTime ?? "unknown";
        public string Popularity => _tourStatisticsService.CalculatePopularity(SelectedTour).ToString() ?? "0";
        public string ChildFriendliness => _tourStatisticsService.CalculateChildFriendliness(SelectedTour).ToString() ?? "0";



        public GeneralViewModel(SelectedTourStore selectedTourStore, TourStatisticsService tourStatisticsService)
        {
            _selectedTourStore = selectedTourStore;
            _tourStatisticsService = tourStatisticsService;
            _selectedTourStore.SelectedTourChanged += _selectedTourStore_SelectedTourChanged;

        }

        protected override void Dispose()
        {
            _selectedTourStore.SelectedTourChanged -= _selectedTourStore_SelectedTourChanged;
        }

        private void _selectedTourStore_SelectedTourChanged()
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(From));
            OnPropertyChanged(nameof(To));
            OnPropertyChanged(nameof(TransportType));
            OnPropertyChanged(nameof(Distance));
            OnPropertyChanged(nameof(EstimatedTime));
            OnPropertyChanged(nameof(Popularity));
            OnPropertyChanged(nameof(ChildFriendliness));
        }
    }
}
