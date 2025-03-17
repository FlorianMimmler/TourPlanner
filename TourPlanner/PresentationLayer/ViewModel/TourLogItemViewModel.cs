using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Commands;
using TourPlanner.BusinessLayer.Model;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.PresentationLayer.View;

namespace TourPlanner.PresentationLayer.ViewModel
{
    public class TourLogItemViewModel : ViewModelBase
    {
        private  TourLog _tourLog;
        private readonly TourLogService _tourLogService;
        public string Date => _tourLog.Date ?? "";
        public string Duration => _tourLog.Duration ?? "";
        public string Distance => _tourLog.Distance.ToString() ?? ""; 
        public ICommand OpenModifyTourLogCommand { get; }

        public int Id=> _tourLog.Id;
        public TourLogItemViewModel(TourLog tourLog,TourLogService tourLogService)
        {
            this._tourLog = tourLog;
            this._tourLogService = tourLogService;
            OpenModifyTourLogCommand = new RelayCommand(OpenModifyTourLogView);

        }

        private void OpenModifyTourLogView()
        {
            ModifyTourLogViewModel modifyTourLogViewModel = new(_tourLog,_tourLogService);
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


        }
    }
}
