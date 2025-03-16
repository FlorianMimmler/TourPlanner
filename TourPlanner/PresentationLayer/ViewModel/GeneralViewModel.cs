using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TourPlanner.BusinessLayer.Model;
using System.Diagnostics.Metrics;
using System.Windows;
using TourPlanner.PresentationLayer.View.Subviews.Maincontent;
using TourPlanner.PresentationLayer.Stores;

namespace TourPlanner.PresentationLayer.ViewModel
{
    public class GeneralViewModel : ViewModelBase
    {

        private SelectedTourStore _selectedTourStore;
        public Tour SelectedTour => _selectedTourStore.SelectedTour;
        public string Name => SelectedTour?.Name ?? "unknown";
        public string Description => SelectedTour?.Description ?? "unknown";
        public string From => SelectedTour?.From ?? "unknown";
        public string To => SelectedTour?.To ?? "unknown";
        public TransportType TransportType => SelectedTour?.TransportType ?? TransportType.Unknown;
        public string Distance => SelectedTour?.Distance.ToString() ?? "0";
        public string EstimatedTime => SelectedTour?.EstimatedTime ?? "unknown";


        public GeneralViewModel(SelectedTourStore selectedTourStore)
        {
            this._selectedTourStore = selectedTourStore;
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
        }
    }
}
