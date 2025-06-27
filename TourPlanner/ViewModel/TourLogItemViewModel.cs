using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Domain.Model;
using BusinessLayer.Services;
using PresentationLayer.View;
using PresentationLayer.Commands;

namespace PresentationLayer.ViewModel
{
    public class TourLogItemViewModel : ViewModelBase
    {
        private  TourLog _tourLog;
        private readonly ITourLogService _tourLogService;
        public string Date => _tourLog.Date.ToShortDateString() ?? "";
        public string Duration => _tourLog.Duration ?? "";
        public string Distance => _tourLog.Distance.ToString() ?? "";
        public string Difficulty => _tourLog.Difficulty.ToString() ?? "";
        public string Rating => _tourLog.Rating.ToString() ?? "";
        public string Comment => _tourLog.Comment ?? "";
        public ICommand OpenModifyTourLogCommand { get; }

        public Guid Id => _tourLog.Id;
        public TourLogItemViewModel(TourLog tourLog, ITourLogService tourLogService)
        {
            _tourLog = tourLog;
            _tourLogService = tourLogService;
            OpenModifyTourLogCommand = new RelayCommand(OpenModifyTourLogView);

        }

        private void OpenModifyTourLogView()
        {
            ModifyTourLogViewModel modifyTourLogViewModel = new(_tourLog, _tourLogService);
            ModifyTourLogView modifyTourLogView = new()
            {
                DataContext = modifyTourLogViewModel

            };
            modifyTourLogViewModel.CloseWindow += (s, e) => modifyTourLogView.Close();
            modifyTourLogView.ShowDialog();
        }

        internal void Update(TourLog tourLog)
        {
            _tourLog = tourLog;
            OnPropertyChanged(nameof(Distance));
            OnPropertyChanged(nameof(Duration));
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(Difficulty));
            OnPropertyChanged(nameof(Rating));
            OnPropertyChanged(nameof(Comment));

        }
    }
}
