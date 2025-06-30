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
using PresentationLayer.View.Subviews.Maincontent;
using BusinessLayer.Services;
using PresentationLayer.Stores;
using BusinessLayer.Interfaces;

namespace PresentationLayer.ViewModel
{
    public class GeneralViewModel : ViewModelBase
    {

        private SelectedTourStore _selectedTourStore;
        private ITourStatisticsService _tourStatisticsService;
        public Tour SelectedTour => _selectedTourStore.SelectedTour;
        public string Name => SelectedTour?.Name ?? "unknown";
        public string Description => SelectedTour?.Description ?? "unknown";
        public string From => SelectedTour?.From ?? "unknown";
        public string To => SelectedTour?.To ?? "unknown";
        public TransportType TransportType => SelectedTour?.TransportType ?? TransportType.Unknown;
        public string Distance => SelectedTour?.Distance.ToString() ?? "0";
        public string EstimatedTime => SelectedTour?.EstimatedTime ?? "unknown";



        public GeneralViewModel(SelectedTourStore selectedTourStore, ITourStatisticsService tourStatisticsService)
        {
            _selectedTourStore = selectedTourStore;
            _tourStatisticsService = tourStatisticsService;
            _selectedTourStore.SelectedTourChanged += _selectedTourStore_SelectedTourChanged;
            _selectedTourStore.SelectedTourChanged += async() => await LoadPopularityAsync();
            _selectedTourStore.SelectedTourChanged += async () => await LoadChildFriendliness();
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

        private string _popularity = "Loading...";
        public string Popularity
        {
            get => _popularity;
            private set
            {
                _popularity = value;
                OnPropertyChanged(nameof(Popularity));
            }
        }

        private string _childFriendliness = "Loading...";

        public string ChildFriendliness
        {
            get => _childFriendliness;
            private set
            {
                _childFriendliness = value;
                OnPropertyChanged(nameof(ChildFriendliness));
            }
        }

        private async Task LoadPopularityAsync()
        {
            if (SelectedTour != null)
            {
                var result = await _tourStatisticsService.CalculatePopularity(SelectedTour);
                Popularity = (result.ToString() ?? "0") + "/10";
            }
            else
            {
                Popularity = "0";
            }
        }

        private async Task LoadChildFriendliness()
        {
            if (SelectedTour != null)
            {
                var result = await _tourStatisticsService.CalculateChildFriendliness(SelectedTour);
                ChildFriendliness = (result.ToString() ?? "0") + "/10";

            }
            else
            {
                ChildFriendliness = "0";
            }
        }
    }
}
