using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Commands;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;

namespace TourPlanner.BusinessLayer.ViewModel
{
    public class CreateTourViewModel
    {
        public ICommand SaveTourCommand { get; }

        public event EventHandler CloseWindow;

        private TourService _toursService = new TourService();

        private string tourName;
        public string TourName
        {
            get { return this.tourName; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this.tourName, value))
                {
                    this.tourName = value;
                    //this.RaisePropertyChanged(); // Method to raise the PropertyChanged event in your BaseViewModel class...
                }
            }
        }

        public CreateTourViewModel()
        {
            SaveTourCommand = new RelayCommand(SaveTour);
        }

        private void SaveTour()
        {
            Console.WriteLine("Save Tour");
            Tour tour = new Tour()
            {
                Name = this.TourName
            };

            var success = _toursService.AddTour(tour);
            if (success)
            {
                CloseWindow?.Invoke(this, EventArgs.Empty);
            }

        }

    }
}
