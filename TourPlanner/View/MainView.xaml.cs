﻿using System;
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
using TourPlanner.BusinessLayer.ViewModel;
using TourPlanner.View.Subviews.Sidebar;

namespace TourPlanner.View
{
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private readonly TourService _tourService;
        public MainView()
        {
            InitializeComponent();

            _tourService = new TourService();

            sidebarView.OpenCreateTourRequested += (s, e) =>
            {
                CreateTourView createTourView = new CreateTourView();

                createTourView.ShowDialog();
            };

        }

       
    }
}
