using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Domain.Model;
using TourPlanner.BusinessLayer.Services;
using DAL;
using PresentationLayer.Commands;

namespace PresentationLayer.ViewModel
{
    class ModifyTourLogViewModel : ValidatorViewModel
    {

        public event EventHandler OpenModifyTourLogRequested;
        public ICommand UpdateTourLogCommand { get; }
        public ICommand DeleteTourLogCommand { get; }
        public ICommand CancelTourLogModificationCommand { get; }

        public event EventHandler CloseWindow;

        private readonly ITourLogService _tourLogService;

        private readonly TourLog _tourLog;



        public ModifyTourLogViewModel(TourLog tourLog, ITourLogService tourLogService)
        {
            _tourLogService = tourLogService;
            _tourLog = tourLog;

            Date = _tourLog.Date;
            Distance = _tourLog.Distance.ToString();
            Duration = _tourLog.Duration;
            Difficulty = _tourLog.Difficulty;
            Rating = _tourLog.Rating;
            Comment = _tourLog.Comment;


            UpdateTourLogCommand = new RelayCommand(UpdateTourLog ,CanExecuteUpdateTourLog);
            DeleteTourLogCommand = new RelayCommand(DeleteTourLog);

            CancelTourLogModificationCommand = new RelayCommand(CancelTourLogModification);

            //TourInputFormViewModel.CloseWindow += (s, e) => CloseWindow?.Invoke(this, EventArgs.Empty);

            _tourLogService.TourLogUpdated += TourLogService_TourLogUpdated;
            _tourLogService.TourLogDeleted += TourLogService_TourLogDeleted;
        }


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
                if(_difficulty == value)
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
                if(_rating == value)
                {
                    return;
                }
                _rating = value;
                Validate(nameof(Rating), value);
                OnPropertyChanged("Rating");
            }
        }


        private void TourLogService_TourLogDeleted(Guid id)
        {
            CloseTourLogModificationWindow();
        }

        private void TourLogService_TourLogUpdated(TourLog tourLog)
        {
            CloseTourLogModificationWindow();
        }

 

        private void DeleteTourLog()
        {
            _tourLogService.DeleteTourLog(_tourLog.Id);
        }

        private void TourLogService_TourUpdated(TourLog tourLog)
        {
            CloseTourLogModificationWindow();
        }

        private void CloseTourLogModificationWindow()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecuteUpdateTourLog()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null, true);
        }
        public void UpdateTourLog()
        {
            if (!CanExecuteUpdateTourLog())
            {
                Console.WriteLine("Validation failed. Please fill in all fields correctly.");
                return;
            }

            if (!double.TryParse(Distance.Replace('.', ','), out double parsedDistance))
            {
                Console.WriteLine("Invalid distance format.");
                return;
            }

            _tourLog.Date = Date;
            _tourLog.Distance = parsedDistance;
            _tourLog.Duration = Duration;
            _tourLog.Comment = Comment;
            _tourLog.Difficulty = Difficulty;
            _tourLog.Rating = Rating;

            _tourLogService.UpdateTourLog(_tourLog);

        }

        private void CancelTourLogModification()
        {
            CloseTourLogModificationWindow();
        }

    }
}
