using BusinessLayer.Interfaces;
using Domain.Model;
using Microsoft.Web.WebView2.Core;
using PresentationLayer.Stores;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModel
{
    public class MapViewModel : ViewModelBase
    {
        private readonly IMapService _mapService;
        private readonly SelectedTourStore _selectedTourStore;

        public Func<string, Task>? CallJsFunction { get; set; }
        public Func<string, Task>? CallCreateImageFunction { get; set; }

        public bool IsLoading => CurrentState == MapViewState.Loading;
        public bool IsError => CurrentState == MapViewState.Error;
        public bool IsLoaded => CurrentState == MapViewState.Loaded;

        private MapViewState _currentState = MapViewState.Loading;

        public string? ImageFilename { get; set; }


        public MapViewModel(IMapService mapService, SelectedTourStore selectedTourStore)
        {
            _mapService = mapService;
            _selectedTourStore = selectedTourStore;
            CurrentState = MapViewState.Loading;

            _selectedTourStore.SelectedTourChanged += UpdateMap;
        }

        public async void UpdateMap()
        {
            if(_selectedTourStore.SelectedTour == null)
            {
                return;
            }

            CurrentState = MapViewState.Loading;

            if (_selectedTourStore.SelectedTour == null)
            {
                CurrentState = MapViewState.Error;
                return;
            }

            var routeGeoJson = await _mapService.GetRouteGeoJson(_selectedTourStore.SelectedTour.From, _selectedTourStore.SelectedTour.To);

            if(routeGeoJson.Contains("error"))
            {
                CurrentState = MapViewState.Error;
                return;
            }

            var js = $"displayRoute({routeGeoJson})";

            CallJsFunction?.Invoke(js);

            ImageFilename = _selectedTourStore.SelectedTour.Id.ToString();
            CurrentState = MapViewState.Loaded;
            //CallCreateImageFunction?.Invoke(_selectedTourStore.SelectedTour.Id.ToString());
        }

        public MapViewState CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(IsError));
                OnPropertyChanged(nameof(IsLoaded));
            }
        }

        
    }
}
