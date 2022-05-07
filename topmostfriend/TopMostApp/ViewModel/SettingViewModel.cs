using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopMostApp.Model;
using TopMostApp.Utils;

namespace TopMostApp.ViewModel
{
    public class SettingViewModel:ObservableObject
    {

        public SettingViewModel()
        {
            Langs = new ObservableCollection<LangInfo>
            {
                new LangInfo { Id = "auto", NameEnglish = "Auto", NameNative = "Auto", TargetVersion = "1.0" },
                new LangInfo { Id = "zh-CN", NameEnglish = "Chinese", NameNative = "简体中文", TargetVersion = "1.0" },
                new LangInfo { Id = "en-US", NameEnglish = "English", NameNative = "English", TargetVersion = "1.0" }
            };
        }
        private ObservableCollection<LangInfo>? _langs;
       public ObservableCollection<LangInfo>? Langs
        {
            get { return _langs; }
            set
            {
                SetProperty(ref _langs, value);
            }
        }
        private LangInfo? _langInfo;
        public LangInfo? LangInfo
        {
            get { return _langInfo; }
            set {
               
                SetProperty(ref _langInfo,value);
                LangHelper.SelectCulture(_langInfo.Id);
            }
        }
    }
}
