using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Commands;

namespace TourPlanner.PresentationLayer.ViewModel
{
    public class MenuViewModel
    {

        public ICommand LightThemeCommand { get; }
        public ICommand DarkThemeCommand { get; }

        public MenuViewModel()
        {
            LightThemeCommand = new RelayCommand(SwitchToLightTheme);
            DarkThemeCommand = new RelayCommand(SwitchToDarkTheme);
        }

        private void SwitchToDarkTheme()
        {
            AppTheme.ChangeTheme(new Uri("Resources/themes/dark.xaml", UriKind.Relative));
        }

        private void SwitchToLightTheme()
        {
            AppTheme.ChangeTheme(new Uri("Resources/themes/light.xaml", UriKind.Relative));
        }


    }
}
