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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLayer.Services;

namespace PresentationLayer.View.Subviews.Sidebar
{
    /// <summary>
    /// Interaktionslogik für SidebarView.xaml
    /// </summary>
    public partial class SidebarView : UserControl
    {
        public SidebarView()
        {
            InitializeComponent();
        }
        /*private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox box)
            {
                if (string.IsNullOrEmpty(box.Text))
                    box.Background = (ImageBrush)FindResource("search_icon");
                else
                    box.Background = null;
            }
        }
        */
    }
}
