using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopMostFriend {
    public partial class AboutForm : Form {
        public AboutForm() {
            InitializeComponent();
            Text = Locale.String(@"AboutTitle");
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            BackgroundImage = Properties.Resources.about;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = Properties.Resources.about.Size;
            MaximizeBox = MinimizeBox = false;
            MaximumSize = MinimumSize = Size;
            TopMost = true;

            int tabIndex = 0;
        }
    }
}
