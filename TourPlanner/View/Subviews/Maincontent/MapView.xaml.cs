using Microsoft.Web.WebView2.Core;
using PresentationLayer.Stores;
using PresentationLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

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

                vm.CallCreateImageFunction = async (string filename) =>
                {

                    await SaveMapImage(filename);
                };
            }

            webView.CoreWebView2.WebMessageReceived += async (sender, e) =>
            {
                var message = e.TryGetWebMessageAsString();
                if (message == "map_ready" && DataContext is MapViewModel vm && vm.ImageFilename is not null)
                {
                    await SaveMapImage(vm.ImageFilename);
                    vm.ImageFilename = null;
                }
            };
        }

        private async Task SaveMapImage(string filename)
        {
            var path_to_save = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TourMapImages");
            Directory.CreateDirectory(path_to_save);

            var filepath = Path.Combine(path_to_save, filename + "_image.png");
            
            using var memoryStream = new MemoryStream();
            await webView.CoreWebView2.CapturePreviewAsync(CoreWebView2CapturePreviewImageFormat.Png, memoryStream);
            File.WriteAllBytes(filepath, memoryStream.ToArray());
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
