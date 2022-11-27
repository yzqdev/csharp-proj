﻿using EyesGuard.Extensions;
using EyesGuard.Views.Pages;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EyesGuard.Views.Menus
{
    public partial class UserLoginMenu : UserControl
    {
        public UserLoginMenu()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            App.ShowWarning(App.LocalizedEnvironment.Translation.Application.LoginWarning, WarningPage.PageStates.Info);
        }
    }
}
