using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Domain.Model;
using TourPlanner.BusinessLayer.Services;
using DAL;
using PresentationLayer.Commands;

namespace PresentationLayer.ViewModel
{
    public class CreateTourLogViewModel: ValidatorViewModel
    {
        public event EventHandler OpenCreateLogRequested;
        public ICommand SaveTourLogCommand { get; }
        public ICommand CancelCommand { get; }

        public event EventHandler CloseWindow;
        public event PropertyChangedEventHandler PropertyChanged;

        private ITourService _toursService;
        private ITourLogService _tourlogService; // to change logs after adding tour

        public CreateTourLogViewModel(ITourService toursService, ITourLogService tourlogService, Tour? selectedTour)
        {
            _toursService = toursService;
            _tourlogService = tourlogService;
            SelectedTour = selectedTour;
            SaveTourLogCommand = new RelayCommand(SaveTourLog, CanExecuteSaveTourLog);
            CancelCommand = new RelayCommand(Cancel);

            _tourlogService.TourLogAdded += TourLogService_TourLogAdded;

            Date = DateTime.Now;

            LoadToursAsync();
        }

        private void TourLogService_TourLogAdded(TourLog log)
        {
            CloseView();
        }

        public bool CanExecuteSaveTourLog()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null, true);
        }

        private void SaveTourLog()
        {
            if (!CanExecuteSaveTourLog())
            {
                Console.WriteLine("Validation failed. Please fill in all fields correctly.");
                return;
            }

            if (!double.TryParse(Distance.Replace('.', ','), out double parsedDistance))
            {
                Console.WriteLine("Invalid distance format.");
                return;
            }

            if(SelectedTour == null)
            {
                Console.WriteLine("Tour is required!");
                MessageBox.Show("Please select a tour!");
                return;
            }

            TourLog tourLog = new TourLog()
            {
                Date = Date,
                Duration = Duration,
                Distance = parsedDistance,
                TourId = SelectedTour.Id,
                Comment = Comment,
                Difficulty = Difficulty, 
                Rating = Rating
            };

            _tourlogService.AddTourLog(tourLog);
        }

        private void CloseView()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }
        private void Cancel()
        {
            CloseView();
        }

        private Tour _selectedTour;
        [Required(ErrorMessage = "Tour is required!")]
        public Tour SelectedTour {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                if(value != null)
                {
                    Validate(nameof(SelectedTour), value);
                }
                
            }
        }

        public async Task LoadToursAsync()
        {
            var loadedTours = await _toursService.GetTours();
            _tours = loadedTours.ToList();
            OnPropertyChanged(nameof(Tours));
        }

        private List<Tour> _tours = new();

        public IEnumerable<Tour> Tours => _tours;

        private DateTime _date;
        [Required(ErrorMessage = "Date is required!")]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                Validate(nameof(Date), value);
            }
        }

        private string _duration;
        [Required(ErrorMessage = "Duration is required!")]
        [RegularExpression(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format (HH:mm)")]
        public string Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                Validate(nameof(Duration), value);
            }
        }

        private string _distance;
        [Required(ErrorMessage = "Distance is required!")]
        [RegularExpression(@"^\d+([,]\d+)?$", ErrorMessage = "Invalid distance format (e.g., 5,4 or 123,54)")]
        public string Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                Validate(nameof(Distance), value);
            }
        }

        private string _comment;
        //[Required(ErrorMessage = "Comment is required!")]
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                Validate(nameof(Comment), value);
            }
        }



       

        private double _difficulty;
        [Required(ErrorMessage = "Difficulty is required!")]
        //[RegularExpression(@"^\d+([,]\d+)?$", ErrorMessage = "Invalid distance format (e.g., 5,4 or 123,54)")]
        public double Difficulty
        {
            get { return _difficulty; }
            set
            {
                if (_difficulty == value)
                {
                    return;
                }

                _difficulty = value;
                Validate(nameof(Difficulty), value);
                OnPropertyChanged("Difficulty");

            }
        }

        private double _rating;
        [Required(ErrorMessage = "Rating is required!")]
        //[RegularExpression(@"^\d+([,]\d+)?$", ErrorMessage = "Invalid distance format (e.g., 5,4 or 123,54)")]
        public double Rating
        {
            get { return _rating; }
            set
            {
                if (_rating == value)
                {
                    return;
                }
                _rating = value;
                Validate(nameof(Rating), value);
                OnPropertyChanged("Rating");
            }
        }
    }
}
