using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;

namespace TourPlanner.PresentationLayer.ViewModel
{
    public class TourInputFormViewModel: ViewModelBase, INotifyDataErrorInfo
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

            if (results.Any())
            {
                if (!_errors.ContainsKey(propertyName))
                {
                    _errors.Add(propertyName, results.Select(r => r.ErrorMessage).ToList());
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                }
            }
            else
            {
                _errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }


        }
    }
}
