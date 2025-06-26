using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Domain.Model;
using TourPlanner.BusinessLayer.Services;
using PresentationLayer.Commands;

namespace PresentationLayer.ViewModel
{
    public class CreateTourViewModel: ViewModelBase
    {
        public ICommand SaveTourCommand { get; }
        public ICommand CancelTourCreationCommand { get; }

        public event EventHandler CloseWindow;

        private readonly ITourService _toursService;

        public TourInputFormViewModel TourInputFormViewModel { get; }
        

        /** Constructor with Commands **/
        public CreateTourViewModel(ITourService tourService)
        {
            _toursService = tourService;

            TourInputFormViewModel = new TourInputFormViewModel();
            SaveTourCommand = new RelayCommand(SaveTour, CanExecuteSaveTour);
            CancelTourCreationCommand = new RelayCommand(CancelTourCreation);

            TourInputFormViewModel.CloseWindow += (s, e) => CloseWindow?.Invoke(this, EventArgs.Empty);
            _toursService.TourAdded += TourService_TourAdded;
        }

        protected override void Dispose()
        {
            _toursService.TourAdded -= TourService_TourAdded;
        }

        public void TourService_TourAdded(Tour tour)
        {

            CloseWindow?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecuteSaveTour()
        {
            return Validator.TryValidateObject(TourInputFormViewModel, new ValidationContext(TourInputFormViewModel), null, true);
        }
        public void SaveTour()
        {
            if (!CanExecuteSaveTour())
            {
                Console.WriteLine("Validation failed. Please fill in all fields correctly.");
                return;
            }

            if (!double.TryParse(TourInputFormViewModel.Distance.Replace('.', ','), out double parsedDistance))
            {
                Console.WriteLine("Invalid distance format.");
                return;
            }

            Tour tour = new()
            {
                Name = TourInputFormViewModel.TourName,
                Description = TourInputFormViewModel.TourDescription,
                From = TourInputFormViewModel.From,
                To = TourInputFormViewModel.To,
                Distance = parsedDistance,
                EstimatedTime = TourInputFormViewModel.EstimatedTime,
                TransportType = TourInputFormViewModel.SelectedTransportType,
                Image = null
            };

            _toursService.AddTour(tour);
        }

        private void CancelTourCreation()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }
        
    }
}
