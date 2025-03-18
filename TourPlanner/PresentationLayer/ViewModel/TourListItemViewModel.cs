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
    public class TourListItemViewModel : ViewModelBase
    {
        private ITourService _tourService;

        public Tour Tour { get; private set; }

        public string Name => Tour.Name;

        public string Id => Tour.Id.ToString();

        public ICommand ModifyCommand { get; }

        public TourListItemViewModel(Tour tour, ITourService tourService)
        {
            ModifyCommand = new RelayCommand(OpenModifyTourView);
            Tour = tour;
            this._tourService = tourService;
        }

        private void OpenModifyTourView()
        {
            ModifyTourViewModel modifyTourViewModel = new ModifyTourViewModel(Tour, _tourService);
            ModifyTourView modifyTourView = new ModifyTourView()
            {
                DataContext = modifyTourViewModel
            };
            modifyTourViewModel.CloseWindow += (s, e) => modifyTourView.Close();
            modifyTourView.ShowDialog();
        }

        public void UpdateTour(Tour tour)
        {
            Tour = tour;
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Id));
        }
    }
}
