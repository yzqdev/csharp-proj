using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Diagnostics;
using SevenZip;
using ICSharpCode.SharpZipLib.Zip;
using System.Runtime.Versioning;

namespace ChromeUpdater.Core.Utils
{
    public class ChromeUtil
    {
        public static Ini ConfigIni { get; }
        public static string appLocation { get; }
        static ChromeUtil()
        {
            var fileName = Process.GetCurrentProcess().MainModule.FileName;
            appLocation = Path.GetDirectoryName(fileName);
            if (string.IsNullOrEmpty(appLocation)) throw new NullReferenceException("wrong path");
            //DownloadPath = Path.Combine(exeLocation, "installer");
            //Writeable = HasWriteAccess(exeLocation);
            ConfigIni = new Ini(Path.Combine(appLocation, $"{Path.GetFileNameWithoutExtension(fileName)}.ini"));
        }
        /// <summary>
        /// 快捷方式名称
        /// </summary>
        /// <param name="shortcutName">快捷方式名称</param>
        /// <param name="shortcutPath">快捷方式路径</param>
        /// <param name="targetFileLocation"></param>
        //private void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        //{

        //    string shortcutLocation = Path.Combine(shortcutPath, shortcutName + ".lnk");
        //    IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
        //    IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutLocation);

        //    shortcut.Description = "定义个人文件夹请修改user-data-dir的值";   // The description of the shortcut
        //    shortcut.IconLocation = Path.Combine(targetFileLocation, "chrome.exe");
        //    //起始目录
        //    shortcut.WorkingDirectory = targetFileLocation;
        //    //快捷方式指向的目标
        //    shortcut.TargetPath = Path.Combine(targetFileLocation, "chrome.exe");
        //    //参数
        //    shortcut.Arguments = " --user-data-dir=" + ConfigIni.Read("UserDataDir", "Updater");
        //    shortcut.Save();
        //}

        
        internal static readonly Regex regxExePath = new Regex(@"\w:\\.*\.exe", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        /// <summary>
        /// 获取exe路径
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static string GetExePath(string Path, string Name = null)
        {
            var val = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(Path, false)?.GetValue(Name)?.ToString();
            return string.IsNullOrEmpty(val) ? null : regxExePath.IsMatch(val) ? regxExePath.Match(val).Value : null;
        }
        public static bool GetCurrentChromeExePath(string filename)
        {
             
                if (File.Exists(filename))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            
        }
        [SupportedOSPlatform("Windows")]
        public static bool TryGetSystemChromeExePath(out string filename)
        {
            //当前工作目录
            filename = Environment.CurrentDirectory + "\\chrome.exe";
            if (File.Exists(filename)) return true;
            //当前目录
            filename = Path.GetDirectoryName( Process.GetCurrentProcess().MainModule.FileName) + "\\chrome.exe";
            if (File.Exists(filename)) return true;
            //自动检测
            string progid;
            using (var userChoiceKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice"))
                progid = userChoiceKey?.GetValue("Progid")?.ToString();
            filename = GetExePath($"{progid ?? "ChromeHTML"}\\shell\\open\\command");
            if (!string.IsNullOrEmpty(filename) && filename.IndexOf("\\chrome.exe", StringComparison.OrdinalIgnoreCase) >= 0) return true;
            //查看http文件
            filename = GetExePath("http\\shell\\open\\command");
            if (!string.IsNullOrEmpty(filename) && filename.IndexOf("\\chrome.exe", StringComparison.OrdinalIgnoreCase) >= 0) return true;
            filename = null;
            return false;
        }

        /// <summary>
        /// 7z解压缩是否在同一路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="exeName"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static string GetSameLevelAndCheck(string path, string exeName, string Name = null)
        {
            var val = GetExePath(path, Name);
            if (string.IsNullOrEmpty(val)) return null;
            var sp = Path.GetDirectoryName(val) + "\\" + exeName;
            return File.Exists(sp) ? sp : null;
        }

        public static bool IsX64Image(string filepath)
        {
            using (var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                if (reader.ReadUInt16() != 23117)
                    throw new BadImageFormatException("Not a valid Portable Executable image", filepath);
                stream.Seek(0x3A, SeekOrigin.Current);
                stream.Seek(reader.ReadUInt32(), SeekOrigin.Begin);
                if (reader.ReadUInt32() != 17744)
                    throw new BadImageFormatException("Not a valid Portable Executable image", filepath);
                stream.Seek(20, SeekOrigin.Current);
                return reader.ReadUInt16() == 0x20b;
            }
        }

        public static bool IsBiggerVersion(string v1, string v2)
        {
            var splv1 = v1.Split('.');
            var splv2 = v2.Split('.');
            var lt = Math.Min(splv1.Length, splv2.Length);
            for (var i = 0; i < lt; i++)
            {
                if (int.Parse(splv2[i]) > int.Parse(splv1[i]))
                    return true;
                if (int.Parse(splv2[i]) < int.Parse(splv1[i]))
                    return false;
            }
            return false;
        }

        public static string GetRelativePath(string filespec, string folder = null)
        {
            if (string.IsNullOrEmpty(folder))
            {
                folder = appLocation;
            }
            Uri pathUri = new Uri(filespec != null ? filespec : folder,UriKind.RelativeOrAbsolute);
            // Folders must end in a slash
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                folder += Path.DirectorySeparatorChar;
            }
            Uri folderUri = new Uri(folder);
            return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
        }
        /// <summary>
        /// unZip文件解压缩
        /// </summary>
        /// <param name="sourceFile">要解压的文件</param>
        /// <param name="path">要解压到的目录</param>
        public static void ZipDeCompress(string sourceFile, string path)
        {
            if (!File.Exists(sourceFile))
            {
                throw new ArgumentException("要解压的文件不存在。");
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(sourceFile)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string fileName = Path.GetFileName(theEntry.Name);
                    string directoryName = Path.GetDirectoryName(theEntry.Name);


                    // create directory
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(path + @"\" + directoryName);
                    }
                    if (fileName != string.Empty)
                    {
                        using (FileStream streamWriter = File.Create(path + @"\" + theEntry.Name))
                        {
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
