using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Commands;

namespace TourPlanner.BusinessLayer.ViewModel
{
    public class SidebarHeaderViewModel
    {
        public ICommand OpenCreateTourCommand { get; }

        public event EventHandler OpenCreateTourRequested;

        public SidebarHeaderViewModel()
        {
            OpenCreateTourCommand = new RelayCommand(OpenCreateTour);
        }

        private void OpenCreateTour()
        {
            Console.WriteLine("Open modal");
            OpenCreateTourRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
