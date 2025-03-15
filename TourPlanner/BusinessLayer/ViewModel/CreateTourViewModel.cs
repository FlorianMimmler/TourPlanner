﻿using System.Collections;
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
    public class CreateTourViewModel : INotifyDataErrorInfo
    {
        public ICommand SaveTourCommand { get; }

        public event EventHandler CloseWindow;
        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly TourService _toursService = new TourService();

        /** Binding Variables For View **/

        private string _tourName;
        [Required(ErrorMessage = "Tour name is required!")]
        public string TourName
        {
            get { return this._tourName; }
            set
            {
                this._tourName = value;
                Validate(nameof(TourName), value);
            }
        }

        private string _tourDescription;
        [Required(ErrorMessage = "Tour description is required!")]
        public string TourDescription
        {
            get { return this._tourDescription; }
            set
            {
                this._tourDescription = value;
                Validate(nameof(TourDescription), value);
            }
        }

        private string _from;
        [Required(ErrorMessage = "From location is required!")]
        public string From
        {
            get { return this._from; }
            set
            {
                this._from = value;
                Validate(nameof(From), value);
            }
        }

        private string _to;
        [Required(ErrorMessage = "To location is required!")]
        public string To
        {
            get { return this._to; }
            set
            {
                this._to = value;
                Validate(nameof(To), value);
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
                Validate(nameof(Distance), value);
            }
        }

        private string _estimatedTime;
        [Required(ErrorMessage = "Estimated time is required!")]
        public string EstimatedTime
        {
            get { return this._estimatedTime; }
            set
            {
                this._estimatedTime = value;
                Validate(nameof(EstimatedTime), value);
            }
        }

        public TransportTypes SelectedTransportType { get; set; }
        public ObservableCollection<TransportTypes> TransportTypes
        {
            get
            {
                return [.. (TransportTypes[])Enum.GetValues(typeof(TransportTypes))];
            }
        }

        public string? this[string columnName]
        {
            get
            {
                return columnName switch
                {
                    nameof(Distance) => !Regex.IsMatch(Distance ?? "", @"^\d+([,]\d+)?$")
                                                ? "Invalid distance format (e.g., 5.4 or 123.54)" : null,
                    nameof(EstimatedTime) => !Regex.IsMatch(EstimatedTime ?? "", @"^([01]?[0-9]|2[0-3]):[0-5][0-9]$")
                                                ? "Invalid time format (HH:mm)" : null,
                    nameof(SelectedTransportType) => SelectedTransportType == 0 ? "Transport type is required!" : null,
                    _ => null,
                };
            }
        }

        /** Constructor with Commands **/
        public CreateTourViewModel()
        {
            SaveTourCommand = new RelayCommand(SaveTour, CanExecuteSaveTour);
            SelectedTransportType = TransportTypes.FirstOrDefault(t => t == Model.TransportTypes.Hiking);
        }

        private bool CanExecuteSaveTour()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null);
        }

        private void SaveTour()
        {
            if (!CanExecuteSaveTour())
            {
                Console.WriteLine("Validation failed. Please fill in all fields correctly.");
                return;
            }

            if (!double.TryParse(Distance.Replace(',', '.'), out double parsedDistance))
            {
                Console.WriteLine("Invalid distance format.");
                return;
            }

            Tour tour = new()
            {
                Name = TourName,
                Description = TourDescription,
                From = From,
                To = To,
                Distance = parsedDistance,
                EstimatedTime = EstimatedTime,
                TransportType = SelectedTransportType.ToString()
            };

            var success = _toursService.AddTour(tour);
            if (success)
            {
                CloseWindow?.Invoke(this, EventArgs.Empty);
            }
        }

        /** Validation Functions **/

        Dictionary<string, List<string>> _errors = new();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool HasErrors => _errors.Count > 0;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                return _errors[propertyName];
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }

        public void Validate(string propertyName, object propertyValue)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);
        
            if(results.Any())
            {
                _errors.Add(propertyName, results.Select(r => r.ErrorMessage).ToList());
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                _errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }


        }
    }
}
