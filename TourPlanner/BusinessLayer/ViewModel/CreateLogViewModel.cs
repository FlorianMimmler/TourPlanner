using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Commands;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;

namespace TourPlanner.BusinessLayer.ViewModel
{
    public class CreateLogViewModel
    {
        public ICommand SaveTourCommand { get; }

        public event EventHandler CloseWindow;
        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly TourService _toursService = new TourService();
        private readonly TourLogService _tourlogService = new(); // to change logs after adding tour
    }
}
