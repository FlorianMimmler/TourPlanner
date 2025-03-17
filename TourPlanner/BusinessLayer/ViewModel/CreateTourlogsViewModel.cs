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

namespace TourPlanner.BusinessLayer.ViewModel
{
    public class CreateTourlogsViewModel
    {
        public ICommand SaveTourCommand { get; }
        public event EventHandler OpenCreateLogRequested;


        private SingletonDatabase _database = SingletonDatabase.Instance;

        public event EventHandler CloseWindow;
        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly TourService _toursService = new TourService();
        private readonly TourLogService _tourlogService = new(); // to change logs after adding tour


       public Tour SelectedTour { get; set; } 

        public ObservableCollection<Tour> Tours
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

        private void OpenCreateTour()
        {
            OpenCreateLogRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
