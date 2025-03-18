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
    class ModifyTourLogViewModel
    {

        public event EventHandler OpenModifyTourLogRequested;
        public ICommand UpdateTourLogCommand { get; }
        public ICommand DeleteTourLogCommand { get; }
        public ICommand CancelTourLogModificationCommand { get; }

        private SingletonDatabase _database = SingletonDatabase.Instance;



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


            UpdateTourLogCommand = new RelayCommand(UpdateTourLog /*,CanExecuteUpdateTourLog*/);
            DeleteTourLogCommand = new RelayCommand(DeleteTourLog);

            CancelTourLogModificationCommand = new RelayCommand(CancelTourLogModification);

            //TourInputFormViewModel.CloseWindow += (s, e) => CloseWindow?.Invoke(this, EventArgs.Empty);

            _tourLogService.TourLogUpdated += TourLogService_TourLogUpdated;
            _tourLogService.TourLogDeleted += TourLogService_TourLogDeleted;
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

        /*public bool CanExecuteUpdateTourLog()
        {
            return Validator.TryValidateObject(TourInputFormViewModel, new ValidationContext(TourInputFormViewModel), null);
        }*/
        public void UpdateTourLog()
        {
            /*if (!CanExecuteUpdateTour())
            {
                Console.WriteLine("Validation failed. Please fill in all fields correctly.");
                return;
            }

            if (!double.TryParse(TourInputFormViewModel.Distance.Replace('.', ','), out double parsedDistance))
            {
                Console.WriteLine("Invalid distance format.");
                return;
            }*/

            if (!double.TryParse(Distance.Replace('.', ','), out double parsedDistance))
            {
                Console.WriteLine("Invalid distance format.");
                return;
            }

            _tourLog.Date = Date;
            _tourLog.Distance = parsedDistance;
            _tourLog.Duration = Duration;

            _tourLogService.UpdateTourLog(_tourLog);



        }

        private void CancelTourLogModification()
        {
            CloseTourLogModificationWindow();
        }

    }
}
