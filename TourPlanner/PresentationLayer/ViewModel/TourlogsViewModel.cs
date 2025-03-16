using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Model;

namespace TourPlanner.PresentationLayer.ViewModel
{
    public class TourlogsViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<TourLog> _tourLogs;

        public ObservableCollection<TourLog> TourLogs
        {
            get { return _tourLogs; }
            set
            {
                _tourLogs = value;
                OnPropertyChanged(nameof(TourLogs));
            }
        }

        public TourlogsViewModel()
        {
            // Sample data for Tour Logs
            TourLogs = new ObservableCollection<TourLog>
            {
                new() { Date = "2024-02-01", Duration = "3h 15m", Distance = "12.5 km" },
                new() { Date = "2024-02-05", Duration = "2h 40m", Distance = "9.2 km" },
                new() { Date = "2024-02-10", Duration = "4h 05m", Distance = "15.8 km" },
                new() { Date = "2024-02-15", Duration = "1h 55m", Distance = "7.3 km" }
            };

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
