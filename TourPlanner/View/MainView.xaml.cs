using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.PresentationLayer.View.Subviews.Sidebar;
using TourPlanner.PresentationLayer.ViewModel;

namespace TourPlanner.PresentationLayer.View
{
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

        }

        private void MaincontentView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
