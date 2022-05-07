using System;
using System.Windows.Controls;
using Hawk.Core.Utils.Plugins;
using WebBrowser = System.Windows.Forms.WebBrowser;

namespace Hawk.ETL.Controls
{
    /// <summary>
    ///     SmartCrawlerUI.xaml 的交互逻辑
    /// </summary>
    [XFrmWork("SmartCrawler")]
    public partial class SmartCrawlerUI : UserControl, ICustomView
    {
        //private readonly WebBrowser browser;

        public SmartCrawlerUI()
        {
            InitializeComponent();
            //browser = new WebBrowser();
            //browser.ScriptErrorsSuppressed = true;
            windowsFormsHost.Source = new Uri("https://cnblogs.com");
        }

        public FrmState FrmState => FrmState.Large;

        public void UpdateHtml(string html)
        {
            windowsFormsHost.CoreWebView2.Navigate("about:blank");
            //browser.Navigate("about:blank");
            if (windowsFormsHost != null && windowsFormsHost.CoreWebView2 != null)
            {
                windowsFormsHost.CoreWebView2.Navigate(html);
            }
            //try
            //{
            //    if (browser.Document != null)
            //    {
            //        browser.Document.Write(string.Empty);
            //    }
            //}
            //catch (Exception )
            //{
            //} // do nothing with this
            //browser.DocumentText = html;
        }
    }
}