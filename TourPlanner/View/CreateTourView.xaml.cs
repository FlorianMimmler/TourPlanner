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
using TourPlanner.BusinessLayer.ViewModel;

namespace TourPlanner.View
{
    /// <summary>
    /// Interaktionslogik für CreateTourView.xaml
    /// </summary>
    public partial class CreateTourView : Window
    {
        public CreateTourViewModel ViewModel { get; }
        public CreateTourView()
        {
            InitializeComponent();
            ViewModel = new CreateTourViewModel();
            ViewModel.CloseWindow += (s, e) => Close();
            DataContext = ViewModel;
        }
    }
}
