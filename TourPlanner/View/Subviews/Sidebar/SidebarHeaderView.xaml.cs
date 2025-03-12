using System.Windows;
using System.Windows.Controls;
using TourPlanner.BusinessLayer.ViewModel;

namespace TourPlanner.View.Subviews.Sidebar
{
    /// <summary>
    /// Interaktionslogik für SidebarHeaderView.xaml
    /// </summary>
    public partial class SidebarHeaderView : UserControl
    {
        public SidebarHeaderViewModel ViewModel { get; }

        public event EventHandler OpenCreateTourRequested;

        public SidebarHeaderView()
        {
            InitializeComponent();
            ViewModel = new SidebarHeaderViewModel();
            DataContext = ViewModel;
            ViewModel.OpenCreateTourRequested += (s, e) => OpenCreateTourRequested?.Invoke(this, EventArgs.Empty);
        }
    }

    
}
