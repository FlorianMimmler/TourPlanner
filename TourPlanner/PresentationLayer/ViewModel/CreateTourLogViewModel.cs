using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Commands;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.DAL;
using TourPlanner.PresentationLayer.ViewModel;

namespace TourPlanner.BusinessLayer.ViewModel
{
    public class CreateTourLogViewModel
    {
        public event EventHandler OpenCreateLogRequested;
        public ICommand SaveTourLogCommand { get; }
        public ICommand CancelCommand { get; }


        private SingletonDatabase _database = SingletonDatabase.Instance;

        public event EventHandler CloseWindow;
        public event PropertyChangedEventHandler PropertyChanged;

        private ITourService _toursService;
        private TourLogService _tourlogService; // to change logs after adding tour

        public CreateTourLogViewModel(ITourService toursService, TourLogService tourlogService)
        {
            _toursService = toursService;
            _tourlogService = tourlogService;

            SaveTourLogCommand = new RelayCommand(SaveTourLog);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void SaveTourLog()
        {
            if (!double.TryParse(Distance.Replace('.', ','), out double parsedDistance))
            {
                Console.WriteLine("Invalid distance format.");
                return;
            }

            TourLog tourLog = new TourLog()
            {
                Date = Date,
                Duration = Duration,
                Distance = parsedDistance,
                TourId = SelectedTour.Id
            };

            _tourlogService.AddTourLog(tourLog);
            CloseView();
        }

        private void CloseView()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }
        private void Cancel()
        {
            CloseView();
        }

        public Tour SelectedTour { get; set; } 

        public IEnumerable<Tour> Tours
        {
            get
            {
                return _database.GetTours();
            }
        }

        private string _date;
        [Required(ErrorMessage = "Date is required!")]
        public string Date
        {
            get { return this._date; }
            set
            {
                this._date = value;
                //Validate(nameof(Date), value);
            }
        }

        private string _duration;
        [Required(ErrorMessage = "Duration is required!")]
        public string Duration
        {
            get { return this._duration; }
            set
            {
                this._duration = value;
                //Validate(nameof(Duration), value);
            }
        }

        private string _distance;
        [Required(ErrorMessage = "Distance is required!")]
        public string Distance
        {
            get { return this._distance; }
            set
            {
                this._distance = value;
                //Validate(nameof(Distance), value);
            }
        }


    }
}
