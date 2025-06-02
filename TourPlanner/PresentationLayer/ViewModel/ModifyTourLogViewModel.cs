using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Commands;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.DAL;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TourPlanner.PresentationLayer.ViewModel
{
    class ModifyTourLogViewModel : ValidatorViewModel
    {

        public event EventHandler OpenModifyTourLogRequested;
        public ICommand UpdateTourLogCommand { get; }
        public ICommand DeleteTourLogCommand { get; }
        public ICommand CancelTourLogModificationCommand { get; }

        public event EventHandler CloseWindow;

        private readonly TourLogService _tourLogService;

        private readonly TourLog _tourLog;



        public ModifyTourLogViewModel(TourLog tourLog, TourLogService tourLogService)
        {
            _tourLogService = tourLogService;
            this._tourLog = tourLog;

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
            get { return this._date; }
            set
            {
                this._date = value;
                Validate(nameof(Date), value);
            }
        }

        private string _duration;
        [Required(ErrorMessage = "Duration is required!")]
        [RegularExpression(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format (HH:mm)")]
        public string Duration
        {
            get { return this._duration; }
            set
            {
                this._duration = value;
                Validate(nameof(Duration), value);
            }
        }

        private string _distance;
        [Required(ErrorMessage = "Distance is required!")]
        [RegularExpression(@"^\d+([,]\d+)?$", ErrorMessage = "Invalid distance format (e.g., 5,4 or 123,54)")]
        public string Distance
        {
            get { return this._distance; }
            set
            {
                this._distance = value;
                Validate(nameof(Distance), value);
            }
        }

        private string _comment;
        //[Required(ErrorMessage = "Comment is required!")]
        public string Comment
        {
            get { return this._comment; }
            set
            {
                this._comment = value;
                Validate(nameof(Comment), value);
            }
        }



        private double _difficulty;
        [Required(ErrorMessage = "Difficulty is required!")]
        //[RegularExpression(@"^\d+([,]\d+)?$", ErrorMessage = "Invalid distance format (e.g., 5,4 or 123,54)")]
        public double Difficulty 
        {
            get { return this._difficulty ; }
            set
            {
                if(this._difficulty == value)
                {
                    return;
                }

                this._difficulty = value;
                Validate(nameof(Difficulty), value);
                OnPropertyChanged("Difficulty");

            }
        }

        private double _rating;
        [Required(ErrorMessage = "Rating is required!")]
        //[RegularExpression(@"^\d+([,]\d+)?$", ErrorMessage = "Invalid distance format (e.g., 5,4 or 123,54)")]
        public double Rating
        {
            get { return this._rating; }
            set
            {
                if(this._rating == value)
                {
                    return;
                }
                this._rating = value;
                Validate(nameof(Rating), value);
                OnPropertyChanged("Rating");
            }
        }


        private void TourLogService_TourLogDeleted(int id)
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
