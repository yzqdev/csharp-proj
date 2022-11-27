﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace EyesGuard.Views.Pages
{
    /// <summary>
    /// Interaction logic for FeedbackPage.xaml
    /// </summary>
    public partial class FeedbackPage : Page
    {
        public FeedbackPage()
        {
            InitializeComponent();
        }

        private void Feedback_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Process.Start("https://github.com/avestura/EyesGuard/issues"));
            App.GetMainWindow().MainFrame.Navigate(new MainPage());
        }
    }
}
