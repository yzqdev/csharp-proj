﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using HandyControl.Controls;
using KeqingNiuza.RealtimeNotes;
using KeqingNiuza.RealtimeNotes.DailyCheck;

namespace KeqingNiuza.View
{
    /// <summary>
    /// DialyCheckView.xaml 的交互逻辑
    /// </summary>
    public partial class DailyCheckView : UserControl
    {
        public DailyCheckView()
        {
            InitializeComponent();
            Loaded += DialyCheckView_Loaded;
            KeqingNiuza.Core.DailyCheck.Program.PrintLog = PrintLog;
        }


        private string cookies;


        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var link = sender as Hyperlink;
            if (string.IsNullOrWhiteSpace(link.NavigateUri.OriginalString))
            {
                return;
            }
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
        }


        private void DialyCheckView_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists($@"{RealtimeNotes.DailyCheck.Const.UserDataPath}\DailyCheckCookies"))
            {
                try
                {
                    var bytes = File.ReadAllBytes($@"{RealtimeNotes.DailyCheck.Const.UserDataPath}\DailyCheckCookies");
                    cookies = Endecryption.Decrypt(bytes);
                }
                catch (Exception ex)
                {

                    Growl.Warning(ex.Message);
                    //Log.OutputLog(LogType.Warning, "LoadCookies", ex);
                }
            }
        }



        private void PrintLog(string log)
        {
            var time = DateTime.Now.ToString("HH:mm:ss");
            var str = $"[{time}]:{log}\n";
            Dispatcher.Invoke(() => TextBox_Log.Text += str);
        }


        private void Button_UpdateCookie_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Cookie.Text = cookies;
            Panel_Cookie.Visibility = Visibility.Visible;
            Button_SaveCookie.Visibility = Visibility.Visible;
            Button_CheckIn.IsEnabled = false;
            Button_UpdateCookie.IsEnabled = false;
        }

        private async void Button_CheckIn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cookies))
            {
                Growl.Info("没有设置Cookie");
                return;
            }
            TextBox_Log.Text = "";
            Button_CheckIn.IsEnabled = false;
            try
            {
                await KeqingNiuza.Core.DailyCheck.Program.Checkin(new string[] { cookies });
            }
            catch (Exception ex)
            {
                PrintLog(ex.Message);
                //Log.OutputLog(LogType.Warning, "DailyCheckIn", ex);
                var errorLog = $"[{DateTime.Now}]\n{TextBox_Log.Text}\n\n";
                Directory.CreateDirectory(RealtimeNotes.DailyCheck.Const.LogPath);
                File.AppendAllText($@"{RealtimeNotes.DailyCheck.Const.LogPath}\DailyCheck-Error.txt", errorLog);
            }
            finally
            {
                Button_CheckIn.IsEnabled = true;
            }
        }

        private void Button_SaveCookie_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox_Cookie.Text))
            {
                return;
            }
            cookies = TextBox_Cookie.Text;
            try
            {
                var bytes = Endecryption.Encrypt(cookies);
                File.WriteAllBytes($@"{RealtimeNotes.DailyCheck.Const.UserDataPath}\DailyCheckCookies", bytes);
                Growl.Success("保存成功");
                Panel_Cookie.Visibility = Visibility.Collapsed;
                Button_SaveCookie.Visibility = Visibility.Collapsed;
                Button_CheckIn.IsEnabled = true;
                Button_UpdateCookie.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Growl.Warning("保存失败\n" + ex.Message);
                //Log.OutputLog(LogType.Warning, "SaveCookie", ex);
            }
        }
    }
}
