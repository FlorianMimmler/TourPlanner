using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TourPlanner.PresentationLayer
{
    class AppTheme
    {

        public static void ChangeTheme(Uri themeuri)
        {
            ResourceDictionary themeDictionary = new ResourceDictionary() { Source = themeuri };

            ResourceDictionary oldThemeDictionary = App.Current.Resources.MergedDictionaries.First(dic => dic.Source.ToString().Contains("themes"));

            App.Current.Resources.MergedDictionaries.Remove(oldThemeDictionary);
            App.Current.Resources.MergedDictionaries.Add(themeDictionary);
        }

    }
}
