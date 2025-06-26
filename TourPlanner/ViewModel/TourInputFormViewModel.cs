using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Commands;
using TourPlanner.Domain.Model;
using TourPlanner.BusinessLayer.Services;

namespace PresentationLayer.ViewModel
{
    public class TourInputFormViewModel: ValidatorViewModel
    {

        public event EventHandler CloseWindow;

        public TourInputFormViewModel()
        {
            SelectedTransportType = TransportTypes.FirstOrDefault(t => t == TransportType.Hiking);
        }

        /** Binding Variables For View **/

        private string _tourName;
        [Required(ErrorMessage = "Tour name is required!")]
        public string TourName
        {
            get { return _tourName; }
            set
            {
                _tourName = value;
                Validate(nameof(TourName), value);
            }
        }

        private string _tourDescription;
        [Required(ErrorMessage = "Tour description is required!")]
        public string TourDescription
        {
            get { return _tourDescription; }
            set
            {
                _tourDescription = value;
                Validate(nameof(TourDescription), value);
            }
        }

        private string _from;
        [Required(ErrorMessage = "From location is required!")]
        public string From
        {
            get { return _from; }
            set
            {
                _from = value;
                Validate(nameof(From), value);
            }
        }

        private string _to;
        [Required(ErrorMessage = "To location is required!")]
        public string To
        {
            get { return _to; }
            set
            {
                _to = value;
                Validate(nameof(To), value);
            }
        }

        private string _distance;
        [Required(ErrorMessage = "Distance is required!")]
        [RegularExpression(@"^\d+([,]\d+)?$", ErrorMessage = "Invalid distance format (e.g., 5,4 or 123,54)")]
        public string Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                Validate(nameof(Distance), value);
            }
        }

        private string _estimatedTime;
        [Required(ErrorMessage = "Estimated time is required!")]
        [RegularExpression(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format (HH:mm)")]
        public string EstimatedTime
        {
            get { return _estimatedTime; }
            set
            {
                _estimatedTime = value;
                Validate(nameof(EstimatedTime), value);
            }
        }

        public TransportType SelectedTransportType { get; set; }
        public ObservableCollection<TransportType> TransportTypes
        {
            get
            {
                return [.. (TransportType[])Enum.GetValues(typeof(TransportType))];
            }
        }
    }
}
