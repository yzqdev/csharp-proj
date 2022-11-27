﻿using EyesGuard.Views.Animations;
using EyesGuard.Extensions;
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
using static EyesGuard.App;

namespace EyesGuard.Views.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public GuardStates ProtectionState
        {
            get { return (GuardStates)GetValue(ProtectionStateProperty); }
            set { SetValue(ProtectionStateProperty, value);
                UpdatePageText();

                // Ignore paused protecting
                App.Configuration.ProtectionState = value;

                App.Configuration.SaveSettingsToFile();
            }
        }
        public static readonly DependencyProperty ProtectionStateProperty =
            DependencyProperty.Register("ProtectionState", typeof(GuardStates), typeof(MainPage), new PropertyMetadata(GuardStates.Protecting));

        public MainPage()
        {
            InitializeComponent();
            CurrentMainPage = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.Background = Brushes.Transparent;
            }

            ProtectionState = App.Configuration.ProtectionState;

            DataContext = App.UIViewModels.ShortLongBreakTimeRemaining;
        }

        private void GuardButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.CheckIfResting()) return;

            if (ProtectionState == GuardStates.Protecting)
            {
                ProtectionState = GuardStates.NotProtecting;
                if(App.Configuration.SaveStats) UpdateIntruptOfStats(GuardStates.NotProtecting);
            }
            else if (ProtectionState == GuardStates.NotProtecting)
            {
                ProtectionState = GuardStates.Protecting;
            }
            else if (ProtectionState == GuardStates.PausedProtecting)
            {
                App.ResumeProtection();
            }
        }

        private async void UpdatePageText()
        {
            PageText.Opacity = 0;

            if (ProtectionState == GuardStates.Protecting)
            {
                PageText.Text = App.LocalizedEnvironment.Translation.EyesGuard.GuardStatus.Running;
            }
            else if (ProtectionState == GuardStates.NotProtecting)
            {
                PageText.Text = App.LocalizedEnvironment.Translation.EyesGuard.GuardStatus.Stopped;
            }
            else if (ProtectionState == GuardStates.PausedProtecting)
            {
                PageText.Text = App.LocalizedEnvironment.Translation.EyesGuard.GuardStatus.Paused;
            }

            await PageText.ShowUsingLinearAnimationAsync();
        }

        private void StackPanel_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if((bool)e.NewValue)
            {
                foreach (UIElement uie in ((StackPanel)sender).Children)
                {
                    uie.ShowUsingLinearAnimationAsync();
                }
            }
        }
    }
}
