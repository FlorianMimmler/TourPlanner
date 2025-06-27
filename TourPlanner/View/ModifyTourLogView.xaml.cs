using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for ModifyTourLogView.xaml
    /// </summary>
    public partial class ModifyTourLogView : Window
    {
        public ModifyTourLogView()
        {
            InitializeComponent();
        }

        // Distance Input Filtering (Only Numbers & Single Decimal Separator)
        private void DistanceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9,.]+$");
        }

        private void DistanceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                // Ensure consistent decimal separator
                textBox.Text = textBox.Text.Replace(".", ",");
            }
        }

        // Time Input Filtering (Only HH:mm Format)
        private void TimeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"[0-9:]");
        }
    }
}
