using Microsoft.Web.WebView2.Core;
using PresentationLayer.ViewModel;
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

namespace PresentationLayer.View.Subviews.Maincontent
{
    /// <summary>
    /// Interaction logic for MaincontentMap.xaml
    /// </summary>
    public partial class MapView : UserControl
    {
        public MapView()
        {
            InitializeComponent();

            webView.Loaded += InitView;
        }

        private async void InitView(object sender, RoutedEventArgs e)
        {
            var exeDir = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = System.IO.Path.Combine(exeDir, "Map", "map.html");
            var uri = new Uri(filePath);
            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.Settings.IsStatusBarEnabled = false;
            webView.CoreWebView2.Navigate(uri.AbsoluteUri);

            webView.CoreWebView2.NavigationCompleted += CallUpdateMap;

            if (DataContext is MapViewModel vm)
            {
                vm.CallJsFunction = async (jsCode) =>
                {
                    await webView.CoreWebView2.ExecuteScriptAsync(jsCode);
                };
            }
        }

        private void CallUpdateMap(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (DataContext is MapViewModel vm)
            {
                vm.UpdateMap();
            }
        }
    }
}
