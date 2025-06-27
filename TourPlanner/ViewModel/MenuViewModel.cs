using Microsoft.Win32;
using PresentationLayer;
using PresentationLayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessLayer.Services;

namespace PresentationLayer.ViewModel
{
    public class MenuViewModel
    {

        public ICommand LightThemeCommand { get; }
        public ICommand DarkThemeCommand { get; }

        public ICommand OpenExportTourFormCommand { get; }

        public ICommand OpenImportTourFormCommand { get; }

        private TourExportService _tourOutputService;
        private TourImportService _tourImportService;

        public MenuViewModel(TourExportService tourOutputService, TourImportService tourImportService)
        {
            LightThemeCommand = new RelayCommand(SwitchToLightTheme);
            DarkThemeCommand = new RelayCommand(SwitchToDarkTheme);
            OpenExportTourFormCommand = new AsyncRelayCommand(OpenExportTourForm);
            OpenImportTourFormCommand = new AsyncRelayCommand(OpenImportTourForm);
            _tourOutputService = tourOutputService;
            _tourImportService = tourImportService;
        }

        private void SwitchToDarkTheme()
        {
            AppTheme.ChangeTheme(new Uri("Resources/themes/dark.xaml", UriKind.Relative));
        }

        private void SwitchToLightTheme()
        {
            AppTheme.ChangeTheme(new Uri("Resources/themes/light.xaml", UriKind.Relative));
        }

        public async Task OpenExportTourForm()
        {
            var dialog = new OpenFolderDialog
            {
                Title = "Select Export Folder",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (dialog.ShowDialog() == true)
            {
                await _tourOutputService.ExportToursToJson(dialog.FolderName);
            }
            
        }

        public async Task OpenImportTourForm()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                Title = "Select Tour JSON File"
            };

            if (dialog.ShowDialog() == true)
            {
                await _tourImportService.ImportToursFromJson(dialog.FileName);
            }
        }

    }
}
