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
    public class ToursListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Tour> _tours;

        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

        public ToursListViewModel()
        {

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
