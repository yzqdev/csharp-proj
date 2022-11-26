using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using KeqingNiuza.RealtimeNotes.Properties;
namespace KeqingNiuza.RealtimeNotes.Models {
    internal class SettingPageModel:INotifyPropertyChanged{



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


      



        private bool _DailyCheck_IsAutoCheckIn = Convert.ToBoolean(JsonConfigHelper.ReadConfig("DailyCheck_IsAutoCheckIn")) ;
        public bool DailyCheck_IsAutoCheckIn {
            get { return _DailyCheck_IsAutoCheckIn; }
            set {
                _DailyCheck_IsAutoCheckIn = value;
                OnPropertyChanged();
            }
        }


        private DateTime _DailyCheck_StartTime =Convert.ToDateTime(JsonConfigHelper.ReadConfig("DailyCheck_StartTime"));
        public DateTime DailyCheck_StartTime {
            get { return _DailyCheck_StartTime; }
            set {
                _DailyCheck_StartTime = value;
                OnPropertyChanged();
            }
        }


        private TimeSpan _DailyCheck_RandomDelay = TimeSpan.Parse(JsonConfigHelper.ReadConfig("DailyCheck_RandomDelay "));
        public TimeSpan DailyCheck_RandomDelay {
            get { return _DailyCheck_RandomDelay; }
            set {
                _DailyCheck_RandomDelay = value;
                OnPropertyChanged();
            }
        }



        private bool _DialyCheck_AlwaysShowResult = Convert.ToBoolean(JsonConfigHelper.ReadConfig("DialyCheck_AlwaysShowResult"));
        public bool DialyCheck_AlwaysShowResult {
            get { return _DialyCheck_AlwaysShowResult; }
            set {
                _DialyCheck_AlwaysShowResult = value;
                OnPropertyChanged();
            }
        }
    }
}
