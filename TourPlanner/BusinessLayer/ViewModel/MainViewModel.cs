using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer.Model;

namespace TourPlanner.BusinessLayer.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TourLog> _tourLogs;
        private ObservableCollection<Tour> _tours;
        public ObservableCollection<TourLog> TourLogs
        {
            get { return _tourLogs; }
            set
            {
                _tourLogs = value;
                OnPropertyChanged(nameof(TourLogs));
            }
        }

        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

        public MainViewModel()
        {
            // Sample data for Tour Logs
            TourLogs = new ObservableCollection<TourLog>
            {
                new() { Date = "2024-02-01", Duration = "3h 15m", Distance = "12.5 km" },
                new() { Date = "2024-02-05", Duration = "2h 40m", Distance = "9.2 km" },
                new() { Date = "2024-02-10", Duration = "4h 05m", Distance = "15.8 km" },
                new() { Date = "2024-02-15", Duration = "1h 55m", Distance = "7.3 km" }
            };

            Tours = new ObservableCollection<Tour>
            {
                new() {Id = 1, Name ="Wienerwald"},
                new() {Id = 2, Name="Dorfrunde" },
                new() {Id = 3, Name="Dopplerhütte"}

            };

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

