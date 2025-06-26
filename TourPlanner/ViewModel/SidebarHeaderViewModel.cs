using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Commands;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.PresentationLayer.View;

namespace PresentationLayer.ViewModel
{
    public class SidebarHeaderViewModel
    {
        private ITourService _tourService;

        public ICommand OpenCreateTourCommand { get; }

        public SidebarHeaderViewModel(ITourService tourService)
        {
            OpenCreateTourCommand = new RelayCommand(OpenCreateTour);
            _tourService = tourService;
        }

        private void OpenCreateTour()
        {
            CreateTourViewModel createTourViewModel = new CreateTourViewModel(_tourService);
            CreateTourView createTourView = new CreateTourView()
            {
                DataContext = createTourViewModel
            };
            createTourViewModel.CloseWindow += (s, e) => createTourView.Close();
            createTourView.ShowDialog();
        }
    }
}
