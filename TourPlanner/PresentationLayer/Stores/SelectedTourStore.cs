using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.BusinessLayer.Model;

namespace TourPlanner.PresentationLayer.Stores
{
    public class SelectedTourStore
    {
        private Tour _selectedTour;
        public Tour SelectedTour 
        { 
            get
            {
                return _selectedTour;
            }
            set
            {
                _selectedTour = value;
                SelectedTourChanged?.Invoke();
            }
        }

        public event Action SelectedTourChanged;

    }
}
