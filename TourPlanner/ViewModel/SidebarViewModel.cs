using PresentationLayer.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;

namespace PresentationLayer.ViewModel
{
    public class SidebarViewModel
    {
        public ToursListViewModel ToursListViewModel { get; }
        public SidebarHeaderViewModel SidebarHeaderViewModel { get; }
        private readonly SearchStore _searchStore;
        public SidebarViewModel(SelectedTourStore _selectedTourStore, ITourService tourService, ICreateTourReportService createTourReportService, SearchStore searchStore, ITourFilterService tourFilterService)
        {
            ToursListViewModel = new ToursListViewModel(_selectedTourStore, tourService, createTourReportService, searchStore, tourFilterService);
            SidebarHeaderViewModel = new SidebarHeaderViewModel(tourService);

            _searchStore = searchStore;
        }

        public string SearchText
        {
            get => _searchStore.SearchText;
            set => _searchStore.SearchText = value;
        }

    }
}
