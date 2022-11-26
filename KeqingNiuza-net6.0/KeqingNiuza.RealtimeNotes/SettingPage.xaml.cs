using HandyControl.Controls;
using KeqingNiuza.RealtimeNotes.Service;
using System;
using System.Windows;
using KeqingNiuza.RealtimeNotes.Models;
namespace KeqingNiuza.RealtimeNotes {
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : System.Windows.Window {
        public SettingPage() {
            InitializeComponent();
        }

        private void Button_DailyCheckSave_Click(object sender, RoutedEventArgs e) {
            try {
                DailyCheckTask.AddTask(Convert.ToBoolean(JsonConfigHelper.ReadConfig("DailyCheck_IsAutoCheckIn")), Convert.ToDateTime(JsonConfigHelper.ReadConfig("DailyCheck_StartTime")), TimeSpan.Parse(JsonConfigHelper.ReadConfig("DailyCheck_RandomDelay")));
                //Properties.Settings.Default.DailyCheck_IsAutoCheckIn = DailyCheck_IsAutoCheckIn;
                //Properties.Settings.Default.DailyCheck_StartTime = DailyCheck_StartTime;
                //Properties.Settings.Default.DailyCheck_RandomDelay = DailyCheck_RandomDelay;
                //Properties.Settings.Default.DialyCheck_AlwaysShowResult = DialyCheck_AlwaysShowResult;
                Growl.Success("保存成功");
            } catch (Exception ex) {
                Growl.Warning(ex.Message);
                //Log.OutputLog(LogType.Warning, "DailyCheckSettingSave", ex);
            }
        }
    }
}
