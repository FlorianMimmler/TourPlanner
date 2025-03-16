using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Commands;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;

namespace TourPlanner.PresentationLayer.ViewModel
{
    public class CreateTourViewModel: ViewModelBase
    {
        public ICommand SaveTourCommand { get; }
        public ICommand CancelTourCreationCommand { get; }

        public event EventHandler CloseWindow;
        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly TourService _toursService = new TourService();

        public TourInputFormViewModel TourInputFormViewModel { get; }
        

        /** Constructor with Commands **/
        public CreateTourViewModel()
        {
            TourInputFormViewModel = new TourInputFormViewModel();
            SaveTourCommand = new RelayCommand(SaveTour, CanExecuteSaveTour);
            CancelTourCreationCommand = new RelayCommand(CancelTourCreation);

            TourInputFormViewModel.CloseWindow += (s, e) => CloseWindow?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecuteSaveTour()
        {
            return Validator.TryValidateObject(TourInputFormViewModel, new ValidationContext(TourInputFormViewModel), null);
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
                TransportType = TourInputFormViewModel.SelectedTransportType
            };

            var success = _toursService.AddTour(tour);
            if (success)
            {
                CloseWindow?.Invoke(this, EventArgs.Empty);
            }
        }



        private void CancelTourCreation()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }

        
    }
}
