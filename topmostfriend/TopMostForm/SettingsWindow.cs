using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopMostForm
{
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();
            this.SetLang();
        }
        public void SetLang()
        {
            hotKeyGroup.Text=Locale.String(@"SettingsHotKeysTitle");
            flagsGroup.Text = Locale.String(@"SettingsOptionsTitle");
            langGroup.Text = Locale.String(@"SettingsLanguageTitle");
            otherGroup.Text = Locale.String(@"SettingsOtherTitle");

            toggleHotKey.Text = Locale.String(@"SettingsHotKeysToggle");
        }
        public static SettingsWindow Instance;

        public static void Display()
        {
            if (Instance != null)
            {
                Instance.Show();
                return;
            }

            Instance = new SettingsWindow();
            Instance.Show();
        }
    }
}
