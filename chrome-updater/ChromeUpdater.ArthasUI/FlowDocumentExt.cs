using Arthas.Controls;
using Arthas.Utility.Media;
using System;
using System.Diagnostics;
using System.Windows.Documents;

namespace ChromeUpdater.ArthasUI
{
    #region FlowDocumentExt
    public static class FlowDocumentExt
    {
        public static RgbaColor Red { get; } = new RgbaColor(0xf6, 0x53, 0x14);
        public static RgbaColor Green { get; } = new RgbaColor(0x7c, 0xbb, 0x00);
        public static RgbaColor Blue { get; } = new RgbaColor(0x00, 0xa1, 0xf1);
        public static RgbaColor Yellow { get; } = new RgbaColor(0xff, 0xbb, 0x00);
        static FlowDocumentExt()
        {
            Default = new FlowDocument();
            Default.AddLine();
            Default.AddLine("欢迎使用Chrome更新器！");
            Default.AddLine(Environment.NewLine);
            Default.AddLine("本更新器可以查询Chrome安装文件的下载地址，");
            Default.AddLine("如果您设置了系统代理，会尝试前往谷歌官方网站查询，如果没有代理则会从耍下的服务器获取。");
            Default.AddLine(Environment.NewLine);
            Default.Add("如果遇到问题可以前往 耍下");
            Default.Add("交", new RgbaColor(202, 202, 202, 100));
            Default.Add("流 群：");
            Default.Add("14724233", "tencent://groupwpa/?subcmd=all\u0026param=7B2267726F757055696E223A31343732343233332C2274696D655374616D70223A313435373135343134397D0A");
            Default.Add("进行交流。");
            Default.AddLine(Environment.NewLine);
            Default.Add("本工具发布地址为：");
            Default.Add("https://csharp.love/chrome_update_tool.html", "https://csharp.love/chrome_update_tool.html");
            Default.AddLine(Environment.NewLine);
            Default.Add("三位大佬的github地址");
            Default.AddLine(Environment.NewLine);
            Default.Add("大佬1", "https://github.com/1217950746");
            Default.AddLine(Environment.NewLine);
            Default.Add("刷下", "https://github.com/shuax");
            Default.AddLine(Environment.NewLine);
            Default.Add("tku", "https://github.com/TkYu");
        }
        public static FlowDocument Default { get; }
        public static void AddLine(this FlowDocument Document, string content = null, RgbaColor rgba = null, Action action = null)
        {
            Run run = new(content);
            if (action == null)
            {
                if (rgba != null) { run.Foreground = rgba.SolidColorBrush; }
                Document.Blocks.Add(new Paragraph(run));
            }
            else
            {
                Hyperlink hl = new(run);
                if (rgba != null) { hl.Foreground = rgba.SolidColorBrush; }
                hl.Click += delegate { action(); };
                hl.MouseLeftButtonDown += delegate { action(); };
                Document.Blocks.Add(new Paragraph(hl));
            }
        }
        public static void Add(this FlowDocument Document, string content, RgbaColor rgba = null, Action action = null)
        {
            if (Document.Blocks.Count <= 0)
            {
                Document.Blocks.Add(new Paragraph());
            }
            Run run = new Run(content);
            if (action == null)
            {
                if (rgba != null) { run.Foreground = rgba.SolidColorBrush; }
                (Document.Blocks.LastBlock as Paragraph)?.Inlines.Add(run);
            }
            else
            {
                Hyperlink hl = new(run);
                if (rgba != null) { hl.Foreground = rgba.SolidColorBrush; }
                hl.Click += delegate { action(); };
                hl.MouseLeftButtonDown += delegate { action(); };
                (Document.Blocks.LastBlock as Paragraph)?.Inlines.Add(hl);
            }
        }
        public static void AddImage(this FlowDocument Document, System.Windows.Media.ImageSource image, Action action)
        {
            if (Document.Blocks.Count <= 0)
            {
                Document.Blocks.Add(new Paragraph());
            }
            if (action == null)
            {
                (Document.Blocks.LastBlock as Paragraph)?.Inlines.Add(new InlineUIContainer(new MetroImage(image)));
            }
            else
            {
                Hyperlink hl = new Hyperlink(new InlineUIContainer(new MetroImage(image))) { Foreground = null };
                hl.Click += delegate { action(); };
                hl.MouseLeftButtonDown += delegate { action(); };
                (Document.Blocks.LastBlock as Paragraph)?.Inlines.Add(hl);
            }
        }
        public static void AddLine(this FlowDocument Document, string title, string url) {
            Document.AddLine(title, null, () => { Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); });
        }
        public static void Add(this FlowDocument Document, string title, string url) {
            Document.Add(title, null, () =>
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            });
            
            }
    }
    #endregion
}
