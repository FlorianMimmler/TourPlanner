using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer
{
    class AppTheme
    {

        public static void ChangeTheme(Uri themeuri)
        {
            ResourceDictionary themeDictionary = new ResourceDictionary() { Source = themeuri };

            ResourceDictionary oldThemeDictionary = Application.Current.Resources.MergedDictionaries.First(dic => dic.Source.ToString().Contains("themes"));

            Application.Current.Resources.MergedDictionaries.Remove(oldThemeDictionary);
            Application.Current.Resources.MergedDictionaries.Add(themeDictionary);
        }

    }
}
