using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Commands;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;

namespace TourPlanner.PresentationLayer.ViewModel
{
    class ModifyTourViewModel
    {
        public ICommand UpdateTourCommand { get; }
        public ICommand CancelTourModificationCommand { get; }

        public event EventHandler CloseWindow;

        private readonly TourService _toursService;
        
        private readonly Tour _tour;

        public TourInputFormViewModel TourInputFormViewModel { get; }


        /** Constructor with Commands **/
        public ModifyTourViewModel(Tour tour, TourService tourService)
        {
            _toursService = tourService;
            this._tour = tour;

            TourInputFormViewModel = new TourInputFormViewModel()
            {
                TourName = _tour.Name,
                TourDescription = _tour.Description,
                From = _tour.From,
                To = _tour.To,
                Distance = _tour.Distance.ToString(),
                EstimatedTime = _tour.EstimatedTime,
                SelectedTransportType = _tour.TransportType
            };
            UpdateTourCommand = new RelayCommand(UpdateTour, CanExecuteUpdateTour);
            CancelTourModificationCommand = new RelayCommand(CancelTourModification);

            TourInputFormViewModel.CloseWindow += (s, e) => CloseWindow?.Invoke(this, EventArgs.Empty);
            
            _toursService.TourUpdated += TourService_TourUpdated;
        }

        private void TourService_TourUpdated(Tour tour)
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecuteUpdateTour()
        {
            return Validator.TryValidateObject(TourInputFormViewModel, new ValidationContext(TourInputFormViewModel), null);
        }
        public void UpdateTour()
        {
            if (!CanExecuteUpdateTour())
            {
                Console.WriteLine("Validation failed. Please fill in all fields correctly.");
                return;
            }

            if (!double.TryParse(TourInputFormViewModel.Distance.Replace('.', ','), out double parsedDistance))
            {
                Console.WriteLine("Invalid distance format.");
                return;
            }

            _tour.Name = TourInputFormViewModel.TourName;
            _tour.Description = TourInputFormViewModel.TourDescription;
            _tour.From = TourInputFormViewModel.From;
            _tour.To = TourInputFormViewModel.To;
            _tour.Distance = parsedDistance;
            _tour.EstimatedTime = TourInputFormViewModel.EstimatedTime;
            _tour.TransportType = TourInputFormViewModel.SelectedTransportType;

            _toursService.UpdateTour(_tour);
        }

        private void CancelTourModification()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }
    }
}
