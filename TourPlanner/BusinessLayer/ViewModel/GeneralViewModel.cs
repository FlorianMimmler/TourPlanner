using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TourPlanner.BusinessLayer.Model;
using System.Diagnostics.Metrics;
using System.Windows;
using TourPlanner.View.Subviews.Maincontent;

namespace TourPlanner.BusinessLayer.ViewModel
{
    class GeneralViewModel : DependencyObject
    {

        public static readonly DependencyProperty TourProperty =
            DependencyProperty.Register("Tour", typeof(Tour), typeof(GeneralView), new PropertyMetadata(null));

        public Tour Tour
        {
            get { return (Tour)GetValue(TourProperty); }
            set
            {
                SetValue(TourProperty, value);
            }
        }

        public GeneralViewModel()
        {
            // Sample data for Tour Logs
            Tour = new() { Id = 1, Name = "Wienerwald" };

        }


    }
}
