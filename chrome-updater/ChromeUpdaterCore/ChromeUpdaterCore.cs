using ChromeUpdater.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Runtime.InteropServices;//互动服务
using System.Diagnostics;
 
using Ookii.Dialogs.Wpf;
using System.Runtime.Versioning;
using ChromeUpdater.Core.Utils;
using SevenZip;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using ChromeUpdater.Model;

namespace ChromeUpdater.Core
{
    [SupportedOSPlatform("Windows")]
    public sealed class ChromeUpdaterCore : ObservableObject
    {
        #region MVVM

       

        #region Private properties and ctor
        private readonly Progress<int> _downloadProgress = new Progress<int>();
        private ChromeUpdate chromeUpdate;
        public ChromeUpdaterCore()
        {

            _downloadProgress.ProgressChanged += (s, value) =>
            {
                DownloadPercent = value;
            };
            setDefaultIni();
            LoadConfig();
        }

        private void setDefaultIni()
        {
            ConfigIni.Write("defaultLocation", @"D:\programs\Chrome", "Updater");
        }
        #endregion

        #region Properties

        private AppUpdate _updateInfo;
        public AppUpdate UpdateInfo
        {
            get { return _updateInfo; }
            set { SetProperty(ref _updateInfo, value); }
        }

       

        private ChromeInfo _currentChromeInfo;
        public ChromeInfo CurrentChromeInfo
        {
            get { return _currentChromeInfo; }
            set { SetProperty(ref _currentChromeInfo, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); DownloadPercent = -1; }
        }

        private string _title = "ChromeUpdater";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _selectedPath;
        public string SelectedPath
        {
            get { return _selectedPath; }
            set
            {
                if (value != _selectedPath && Writeable && Directory.Exists(value))
                    ConfigIni.Write("UserDataDir", "user-dir","Updater");
                    ConfigIni.Write("LastPath", ChromeUtil.GetRelativePath(value), "Updater");
                SetProperty(ref _selectedPath, value);
                LoadChromeInfo();
            }
        }

        private Branch _branchSelected;
        public Branch BranchSelected
        {
            get { return _branchSelected; }
            set { SetProperty(ref _branchSelected, value); SelectChanged(); }
        }

        private bool _isX64Selected;
        public bool IsX64Selected
        {
            get { return _isX64Selected; }
            set { SetProperty(ref _isX64Selected, value); SelectChanged(); }
        }

        private bool _keepOldversion;
        public bool KeepOldversion
        {
            get { return _keepOldversion; }
            set
            {
                if (value != _keepOldversion && Writeable) ConfigIni.Write("KeepLastVersion", value ? "1" : "0", "Updater");
               SetProperty(ref _keepOldversion, value); 
            }
        }

        private bool _keepInstaller;
        public bool KeepInstaller
        {
            get { return _keepInstaller; }
            set
            {
                if (value != _keepInstaller && Writeable) ConfigIni.Write("KeepInstaller", value ? "1" : "0", "Updater");
              SetProperty(ref _keepInstaller, value);
            }
        }

        private int _downloadPercent;
        public int DownloadPercent
        {
            get { return _downloadPercent; }
            set { SetProperty(ref _downloadPercent, value); }
        }

        private IMessageService _messageService;
        public IMessageService MessageService => _messageService ?? (_messageService = ServiceManager.Instance.IsServiceExists<IMessageService>() ? ServiceManager.Instance.GetService<IMessageService>() : null);
        #endregion

        #region Methods
        private void LoadConfig()
        {
            IsX64Selected = true;
            if (File.Exists(ConfigIni.Path))
            {
                KeepOldversion = ConfigIni.Read("KeepLastVersion", "Updater") == "1";
                KeepInstaller = ConfigIni.Read("KeepInstaller", "Updater") == "1";
                var lastpath = ConfigIni.Read("LastPath", "Updater");
                //
                if (!string.IsNullOrEmpty(lastpath))
                {
                    var bk = Path.GetFullPath(lastpath);
                    if (Directory.Exists(bk))
                        SelectedPath = bk;
                    return;
                }
                else
                {
                    SelectedPath = ConfigIni.Read("defaultLocation", "Updater");
                }
            }
            string exePath = SelectedPath + @"\chrome.exe";
            if (ChromeUtil.GetCurrentChromeExePath(    exePath))
            {
               
                if (MessageBox.Show("检测到当前目录已经有chrome,是否打开?", "提示", MessageBoxButton.OKCancel ) == MessageBoxResult.OK)

                {
                    Process.Start(new ProcessStartInfo(exePath) { Arguments = " --user-data-dir=user-dir" });
                    //delete

                }
            }
        }

        private void SelectChanged()
        {
            if (chromeUpdate != null)
                UpdateInfo = chromeUpdate.GetUpdate(BranchSelected, IsX64Selected);
        }
        /// <summary>
        /// 保存当前chrome
        /// </summary>
        private void SaveBranch()
        {
            if (BranchSelected == CurrentChromeInfo?.Branch) return;
            if (!string.IsNullOrEmpty(SelectedPath) && HasWriteAccess(SelectedPath))
            {
                File.WriteAllText($"{SelectedPath}\\branch", BranchSelected.ToString());
            }
             
        }
        
        /// <summary>
        /// 获取当前chrome的信息
        /// </summary>
        private void LoadChromeInfo()
        {
            if (SelectedPath == null)
            {
                Title = "ChromeUpdater";
                return;
            }
            if (Directory.Exists(SelectedPath))
            {
                var chromeExePath = Path.Combine(SelectedPath, "chrome.exe");
                if (File.Exists(chromeExePath))
                {
                     
                    //todo
                    var version = FileVersionInfo.GetVersionInfo(chromeExePath);
                    var gcPath = Path.Combine(SelectedPath, "GreenChrome.ini");
                    Branch? bch = null;
                    var bfPath = Path.Combine(SelectedPath, "branch");
                    if (File.Exists(bfPath))
                    {
                        if (Enum.TryParse(File.ReadAllText(bfPath), out Branch b))
                            bch = b;
                    }
                    else if (File.Exists(gcPath))
                    {
                        var ini = new Ini(gcPath);
                        if (Enum.TryParse(ini.Read("检查版本", "检查更新"), out Branch b))
                            bch = b;
                    }
                    var ix = ChromeUtil.IsX64Image(chromeExePath);
                    CurrentChromeInfo = new ChromeInfo(version.FileVersion, ix, bch);
                    IsX64Selected = ix;
                }
                else
                {
                    CurrentChromeInfo = null;
                }
                Title = CurrentChromeInfo == null ? "ChromeUpdater - 当前目录没有找到chrome.exe" : $"ChromeUpdater - 当前版本:{CurrentChromeInfo}";
            }
            else
            {
                Title = "ChromeUpdater - 请选择一个有效的文件夹";
            }
        }

        private async Task ReportException(Exception ex)
        {
            if (MessageService != null)
            {
                if (await MessageService.ShowAsync($"遇到错误：{ex.Message}，请问是否要复制出错详情？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    Clipboard.SetText(ex.StackTrace);
            }
        }

        private Task MsgBox(string text, string title = null)
        {
            return MessageService == null ? Task.Delay(5) : MessageService.ShowAsync(text, title);
        }
        #endregion

        #region Commands
        private ICommand _cmdCheckUpdate;
        public ICommand CmdCheckUpdate => _cmdCheckUpdate ?? (_cmdCheckUpdate = new AsyncCommand(async () =>
        {
            if (IsBusy) return;
            IsBusy = true;
            chromeUpdate = await GetUpdateFromShuax();
            UpdateInfo = chromeUpdate?.GetUpdate(BranchSelected, IsX64Selected);
            Debug.WriteLine(UpdateInfo.ToString());
            IsBusy = false;
        }));

        private ICommand _cmdFolderBrowse;
        public ICommand CmdFolderBrowse => _cmdFolderBrowse ?? (_cmdFolderBrowse = new AsyncCommand(async () =>
        {
            if (IsBusy) return;

            var dialog = new VistaFolderBrowserDialog();
            dialog.Description = "请选择文件夹.";
            dialog.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.
 
            if ((bool)dialog.ShowDialog()  )
            {
                SelectedPath = dialog.SelectedPath;
                MessageBox.Show(SelectedPath);
            }
            await Task.Delay(1);
        }));

        private ICommand _cmdDownload;
        public ICommand CmdDownload => _cmdDownload ?? (_cmdDownload = new AsyncCommand(async () =>
        {
            if (IsBusy) return;
            if (UpdateInfo == null)
            {
                await MsgBox("请先查询！");
                return;
            }
            IsBusy = true;
            try
            {
                var file = Path.Combine(DownloadPath, UpdateInfo.name);
                if (!File.Exists(file))
                { await DownloadFile(UpdateInfo.url[0], UpdateInfo.name, UpdateInfo.sha1, _downloadProgress); }
                if (File.Exists(file))
                {
                     Process.Start("Explorer.exe", $"/select,\"{file}\"");
                }
            }
            catch (Exception ex)
            {
                await ReportException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }));

        private ICommand _cmdDownloadAndExtract;
        /// <summary>
        /// 下载并解压
        /// </summary>
        public ICommand CmdDownloadAndExtract => _cmdDownloadAndExtract ?? (_cmdDownloadAndExtract = new AsyncCommand(async () =>
        {
            if (IsBusy) return;
            if (UpdateInfo == null)
            {
                await MsgBox("请先查询！");
                return;
            }
            IsBusy = true;
            try
            {
                Debug.WriteLine("下载路径"+DownloadPath);
                var file = Path.Combine(DownloadPath, UpdateInfo.name);
                //MessageBox.Show(file, "这是file的路径");
                if (!File.Exists(file))
                {
                    await DownloadFile(UpdateInfo.url[0], UpdateInfo.name, UpdateInfo.sha1, _downloadProgress);
                }
                DownloadPercent = -1;
                bool canExtract = false;

                if (!Directory.Exists(SelectedPath))
                {
                    Directory.CreateDirectory(SelectedPath);
                }
                if (Directory.GetFiles(SelectedPath).Length > 0)
                {
                    if (CurrentChromeInfo != null)
                    {
                        canExtract = IsX64Selected != CurrentChromeInfo.IsX64 || BranchSelected != CurrentChromeInfo.Branch || ChromeUtil.IsBiggerVersion(CurrentChromeInfo.Version, UpdateInfo.version);
                    }
                    else
                    {
                        await MsgBox("请注意，您选择的文件夹不为空并且里面没有找到chrome，请谨慎操作！");
                        canExtract = true;
                    }
                }
                else
                {
                    canExtract = true;
                }
                if (!canExtract)
                {
                    await MsgBox("更新包的版本和您本地的版本一致，不需要再次覆盖！", "提示");
                    return;
                }
            //D:\Documents\Visual Studio 2022\Projects\chrome - updater\ChromeUpdater.ArthasUI\bin\Debug\net6.0 - windows\installer\96.0.4664.45_chrome_installer.exe
                if (File.Exists(file))
                {
                    //chrome.7z文件
                    var c7z = Path.Combine(AppDomain.CurrentDomain.BaseDirectory+@"\installer", "chrome.7z");
                     await Task.Run(async () =>
                    {
                       ExtractFile(file, DownloadPath);
                         
                         ExtractFile(c7z, SelectedPath);
                     
                    });
                    Debug.WriteLine("解压结束");

                    await Task.Run(() =>
                    {
                        var chromeExePath = Path.Combine(SelectedPath, "chrome.exe");
                        var oldChrome = Path.Combine(SelectedPath, "old_chrome.exe");
                        if (KeepOldversion)
                        {
                            if (File.Exists(oldChrome))
                            {
                                //删除老版本的老版本
                                var oldversion = Path.Combine(SelectedPath, System.Diagnostics.FileVersionInfo.GetVersionInfo(oldChrome).FileVersion);
                                if (Directory.Exists(oldversion))
                                    Win32Api.IO.DeleteFile(oldversion);
                                File.Delete(oldversion);

                            }
                            if (File.Exists(chromeExePath)) File.Move(chromeExePath, oldChrome);
                        }
                        else
                        {
                            //不留活口
                            if (File.Exists(chromeExePath))
                            {
                                var cv = Path.Combine(SelectedPath, FileVersionInfo.GetVersionInfo(chromeExePath).FileVersion);
                                if (Directory.Exists(cv))
                                { Win32Api.IO.DeleteFile(cv); }
                            }
                        }
                        Win32Api.IO.MoveUp(Path.Combine(SelectedPath, "Chrome-bin"));
                        if (!KeepInstaller)
                        {
                            File.Delete(file);
                        }
                    });
                    if (File.Exists(c7z))
                    {
                        Debug.WriteLine("删除这个");
                        File.Delete(c7z);
                    }
                }  
                SaveBranch();
                //CreateShortcut("谷歌浏览器", SelectedPath,SelectedPath);
                LoadChromeInfo();
              
                UpdateInfo = chromeUpdate?.GetUpdate(BranchSelected, IsX64Selected);
            }
            catch (Exception ex)
            {
                await ReportException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }));

        private ICommand _cmdCheckGCUpdate;
        public ICommand CmdCheckGCUpdate => _cmdCheckGCUpdate ?? (_cmdCheckGCUpdate = new AsyncCommand(async () =>
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                Console.WriteLine("没有greenchrome");
            }
            catch (Exception ex)
            {
                await ReportException(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }));

        private ICommand _cmdDownloadGC;
        public ICommand CmdDownloadGC => _cmdDownloadGC ?? (_cmdDownloadGC = new AsyncCommand(async () =>
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                Console.WriteLine("deprecated");
            }
            catch (Exception ex)
            {
                await ReportException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }));

        private ICommand _cmdCopyToClipboard;
        public ICommand CmdCopyToClipboard => _cmdCopyToClipboard ?? (_cmdCopyToClipboard = new AsyncCommand<string>(async str =>
        {
            try
            {
                Clipboard.Clear();
                if (string.IsNullOrEmpty(str))
                {
                    if (UpdateInfo != null)
                        Clipboard.SetDataObject(UpdateInfo.url[0]);
                }
                else
                {
                    Clipboard.SetDataObject(str);
                }
                if (MessageService != null)
                    await MessageService.ShowAsync("复制成功", "提示");
            }
            catch (Exception ex)
            {
                await ReportException(ex);
            }
        }));
        #endregion

        #endregion

        #region Utils(static)

        public static string DownloadPath { get; }
        public static string CD { get; }
        public static Ini ConfigIni { get; }
        public static bool Writeable { get; }

        #region Static ctor
        static ChromeUpdaterCore()
        {
            var fileName = Process.GetCurrentProcess().MainModule.FileName;
            CD = Path.GetDirectoryName(fileName);
            if (string.IsNullOrEmpty(CD)) throw new NullReferenceException("wrong path");
            DownloadPath = Path.Combine(CD, "installer");
            Writeable = HasWriteAccess(CD);
            ConfigIni = new Ini(Path.Combine(CD, $"{Path.GetFileNameWithoutExtension(fileName)}.ini"));
        }
        #endregion

        #region Update
        public static async Task<AppUpdate> GetUpdateFromGoogle(Branch branch, bool isX64, int timeout = 8000)
        {
            AppUpdate cu;
            var hc = new HttpClient { Timeout = TimeSpan.FromMilliseconds(timeout) };
            const string url = "https://tools.google.com/service/update2";
            string appid, ap, ap64;
            switch (branch)
            {
                case Branch.Stable:
                    //appid = "4DC8B4CA-1BDA-483E-B5FA-D3C12E15B62D";
                    appid = "8A69D345-D564-463C-AFF1-A69D9E530F96";
                    ap = "-multi-chrome";
                    ap64 = "x64-stable-multi-chrome";
                    break;
                case Branch.Beta:
                    //appid = "4DC8B4CA-1BDA-483E-B5FA-D3C12E15B62D";
                    appid = "8A69D345-D564-463C-AFF1-A69D9E530F96";
                    ap = "1.1-beta";
                    ap64 = "x64-beta-multi-chrome";
                    break;
                case Branch.Dev:
                    //appid = "4DC8B4CA-1BDA-483E-B5FA-D3C12E15B62D";
                    appid = "8A69D345-D564-463C-AFF1-A69D9E530F96";
                    ap = "2.0-dev";
                    ap64 = "x64-dev-multi-chrome";
                    break;
                case Branch.Canary:
                    appid = "4EA16AC7-FD5A-47C3-875B-DBF4A2008C20";
                    ap = "";
                    ap64 = "x64-canary";
                    break;
                default:
                    appid = "";
                    ap = "";
                    ap64 = "";
                    break;
            }
            HttpContent postData = new StringContent(isX64 ? @"<?xml version=""1.0"" encoding=""UTF-8""?><request protocol=""3.0"" version=""1.3.23.9"" shell_version=""1.3.21.103"" ismachine=""0"" sessionid=""{3597644B-2952-4F92-AE55-D315F45F80A5}"" installsource=""ondemandcheckforupdate"" requestid=""{CD7523AD-A40D-49F4-AEEF-8C114B804658}"" dedup=""cr""><os platform=""win"" version=""6.1"" sp=""Service Pack 1"" arch=""x64""/><app appid=""{" + appid + @"}"" version="""" nextversion="""" ap=""" + ap64 + @""" lang="""" brand=""GGLS"" client=""""><updatecheck/></app></request>" : @"<?xml version=""1.0"" encoding=""UTF-8""?><request protocol=""3.0"" version=""1.3.23.9"" shell_version=""1.3.21.103"" ismachine=""0"" sessionid=""{3597644B-2952-4F92-AE55-D315F45F80A5}"" installsource=""ondemandcheckforupdate"" requestid=""{CD7523AD-A40D-49F4-AEEF-8C114B804658}"" dedup=""cr""><os platform=""win"" version=""6.1"" sp=""Service Pack 1"" arch=""x86""/><app appid=""{" + appid + @"}"" version="""" nextversion="""" ap=""" + ap + @""" lang="""" brand=""GGLS"" client=""""><updatecheck/></app></request>");
            try
            {
                // ReSharper disable PossibleNullReferenceException
                var result = await hc.PostAsync(url, postData);
                result.EnsureSuccessStatusCode();
                var doc = new XmlDocument();
                doc.LoadXml(await result.Content.ReadAsStringAsync());
                var response = doc.ChildNodes[1];
                var app = response.ChildNodes[1];
                var updatecheck = app.ChildNodes[0];
                var urls = updatecheck.ChildNodes[0];
                var manifest = updatecheck.ChildNodes[1];
                var version = manifest.Attributes["version"].Value;
                //var action = manifest.ChildNodes[0].ChildNodes[0];
                var package = manifest.ChildNodes[1].ChildNodes[0];
                var size = package.Attributes["size"].Value;
                var name = package.Attributes["name"].Value;
                var hash_sha256 = package.Attributes["hash_sha256"].Value;
                var hash = package.Attributes["hash"].Value;
                cu = new AppUpdate {
                    url = (from XmlNode u in urls.ChildNodes select u.Attributes["codebase"].Value + name).ToArray(),
                    size = long.Parse(size),
                    name = name,
                    version = version,
                    sha256 = hash_sha256?.ToUpper()
                };
                if (!string.IsNullOrEmpty(hash))
                    cu.sha1 = BitConverter.ToString(Convert.FromBase64String(hash)).Replace("-", "");
                // ReSharper restore PossibleNullReferenceException
            }
            catch
            {
                throw new Exception("获取失败，可能是连接超时或APPID变更导致");
            }
            return cu;
        }
        /// <summary>
        /// 从国内api获取谷歌浏览器更新
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static async Task<ChromeUpdate> GetUpdateFromShuax(int timeout = 8000)
        {
            //try mirror first
            var hc = new HttpClient { Timeout = TimeSpan.FromSeconds(3) };
            try
            {
                var oldStr = "https://api.pzhacm.org/iivb/cu.json";
                //var newStr = "http://localhost:5209/ChromeVersion/version";
                var newStr = "http://localhost:8550/common/chromeVersion";
                var res = await hc.GetStringAsync(oldStr);
                Debug.Write(res);
                return SimpleJson.SimpleJson.DeserializeObject<ChromeUpdate>(res);
            }
            catch
            {
                //oops
            }
             return null;
        }

         

        public static async Task DownloadFile(string url, string fileName, string sha1 = null, IProgress<int> progress = null)
        {
            if (!Directory.Exists(DownloadPath))
                Directory.CreateDirectory(DownloadPath);
            try
            {
                var temlFiles = new List<string>();
                temlFiles.AddRange(Directory.GetFiles(DownloadPath, "*.td.cfg"));
                temlFiles.AddRange(Directory.GetFiles(DownloadPath, "*.td"));
                temlFiles.ForEach(File.Delete);
            }
            catch
            {
                //TODO
            }
            var tempFileName = Guid.NewGuid().ToString("N") + ".td";
            var tempFileNamePath = DownloadPath + "\\" + tempFileName;
            if (File.Exists(CD + "\\xldl.dll") & Directory.Exists(CD + "\\download") & !Win32Api.Is64BitProcess)
            {//可以用迅雷
                if (!Xunlei.XL_Init())
                {
                    Xunlei.XL_UnInit();
                    throw new Exception("迅雷引擎初始化失败");
                }
                var param = new Xunlei.DownTaskParam { szTaskUrl = url, szFilename = tempFileName, szSavePath = DownloadPath };
                try
                {
                    var handel = Xunlei.XL_CreateTask(param);
                    Xunlei.XL_StartTask(handel);
                    while (true)
                    {
                        await Task.Delay(100);
                        var info = new Xunlei.DownTaskInfo();
                        Xunlei.XL_QueryTaskInfoEx(handel, info);
                        progress?.Report((int)Math.Min(100, info.fPercent * 100));
                        if (info.stat == Xunlei.DownTaskStatus.TscComplete || info.stat == Xunlei.DownTaskStatus.TscError)
                            break;
                    }
                }
                catch (Exception)
                {
                    Xunlei.XL_DelTempFile(param);
                    throw;
                }
                finally
                {
                    progress?.Report(0);
                    Xunlei.XL_UnInit();
                }
            }
            else
            {
                using (var wc = new System.Net.WebClient())
                {
                    wc.DownloadProgressChanged += (ss, ee) =>
                    {
                        progress?.Report(ee.ProgressPercentage);
                    };
                    try
                    {
                        await wc.DownloadFileTaskAsync(url, tempFileNamePath);
                    }
                    catch (Exception)
                    {
                        if (File.Exists(tempFileNamePath))
                            File.Delete(tempFileNamePath);
                        throw;
                    }
                    finally
                    {
                        progress?.Report(0);
                    }
                }
            }
            if (File.Exists(tempFileNamePath))
            {
                if (!string.IsNullOrEmpty(sha1) && SHA1_HashFile(tempFileNamePath) != sha1)
                {
                    File.Delete(tempFileNamePath);
                    throw new Exception("下载失败(SHA1校验不正确)!");
                }
                File.Move(tempFileNamePath, Path.Combine(DownloadPath, fileName));
            }
        }
        #endregion

        #region Hash
        public static string MD5_Hash(string str_md5_in, bool remove = true)
        {
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes_md5_in = Encoding.Default.GetBytes(str_md5_in);
            byte[] bytes_md5_out = md5.ComputeHash(bytes_md5_in);
            string str_md5_out = BitConverter.ToString(bytes_md5_out);
            if (remove) str_md5_out = str_md5_out.Replace("-", "");
            return str_md5_out;
        }

        public static string SHA1_Hash(string str_sha1_in, bool remove = true)
        {
            var sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = Encoding.Default.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            if (remove) str_sha1_out = str_sha1_out.Replace("-", "");
            return str_sha1_out;
        }

        public static string SHA1_HashFile(string str_sha1_in, bool remove = true)
        {
            var sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = File.ReadAllBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            if (remove) str_sha1_out = str_sha1_out.Replace("-", "");
            return str_sha1_out;
        }
        #endregion

        #region Permission

        public static bool IsAdministrator()
        {
            bool isAdmin;
            WindowsIdentity user = null;
            try
            {
                user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException)
            {
                isAdmin = false;
            }
            catch (Exception)
            {
                isAdmin = false;
            }
            finally
            {
                user?.Dispose();
            }
            return isAdmin;
        }
        /// <summary>
        /// 检查是否有写入文件的权限
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static bool HasWriteAccess(string FilePath)
        {
            try
            {
                FileSystemSecurity security;
                 
                if (File.Exists(FilePath))
                {
                    security = new FileInfo(FilePath).GetAccessControl();
                }
                else
                {
                    var d = Path.GetDirectoryName(FilePath);
                    if (d == null) return false;
                    security =new DirectoryInfo(d).GetAccessControl() ;
                }
                var rules = security.GetAccessRules(true, true, typeof(NTAccount));
                var curr = WindowsIdentity.GetCurrent();
                var currentuser = new WindowsPrincipal(curr);
                bool result = false;
                foreach (FileSystemAccessRule rule in rules)
                {
                    if (0 == (rule.FileSystemRights &
                              (FileSystemRights.WriteData | FileSystemRights.Write)))
                    {
                        continue;
                    }

                    if (rule.IdentityReference.Value.StartsWith("S-1-"))
                    {
                        var sid = new SecurityIdentifier(rule.IdentityReference.Value);
                        if (!currentuser.IsInRole(sid))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (!currentuser.IsInRole(rule.IdentityReference.Value))
                        {
                            continue;
                        }
                    }

                    if (rule.AccessControlType == AccessControlType.Deny)
                        return false;
                    if (rule.AccessControlType == AccessControlType.Allow)
                        result = true;
                }
                return result;
            }
            catch
            {
                return false;
            }
        }

        #endregion



        
        /// <summary>


        /// <param name="sourceFile"></param>
        /// <param name="destinationPath"></param>
        /// <param name="silence"></param>
        /// <exception cref="Exception"></exception>
        /// </summary>
        public static void ExtractFileUseExe(string file, string destinationPath, bool silence = true)
        {
            //D:\Documents\Visual Studio 2022\Projects\chrome-updater\ChromeUpdater.ArthasUI\bin\Debug\net6.0-windows\installer\96.0.4664.45_chrome_installer.exe
            Debug.WriteLine(file);
            Debug.WriteLine(destinationPath); //D:\tmp\chrome

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }
            var extractor = new ProcessStartInfo(Path.Combine(CD + "\\lib", "7za.exe"), $"x \"{file}\" -o\"{destinationPath}\" -aoa -y");
            //MessageBox.Show(extractor.FileName);
            //"C:\\Users\\yzqde\\source\\repos\\ChromeUpdater\\ChromeUpdater.ArthasUI\\bin\\Debug\\7za.exe"
            bool isRAR = false;
           
            if (!File.Exists(extractor.FileName))
            {
                //如果找不到7z就去网上下载
                using (var wc = new System.Net.WebClient())
                {
                    wc.DownloadFile($"https://github.com/copyer98/my-utils/blob/main/7zxa.dll", "7zxa.dll");
                    wc.DownloadFile($"https://github.com/copyer98/my-utils/blob/main/7za.exe", extractor.FileName);
                }
            }
            if (!File.Exists(extractor.FileName))
            {
                throw new Exception("extractor disappeared!");
            }
            //"C:\\Users\\yzqde\\source\\repos\\ChromeUpdater\\ChromeUpdater.ArthasUI\\bin\\Debug\\7za.exe"
           
 

            if (isRAR)
            {
                if (silence)
                {
#if DEBUG
                    var proc = Process.Start(extractor);
                    if (proc != null)
                    {
                        WaitForHandel(proc);
                        FuckCancel(proc);
                        proc.WaitForExit();
                    }
#else
                            using (var desktop = Onyeyiri.Desktop.CreateDesktop("chromeextract"))
                            {
                                var p = desktop.CreateProcess(extractor.FileName + " " + extractor.Arguments);
                                p?.WaitForExit();
                                desktop.Close();
                            }
#endif
                }
                else
                {
                    var proc = System.Diagnostics.Process.Start(extractor);
                    if (proc != null)
                    {
                        WaitForHandel(proc);
                        FuckCancel(proc);
                        proc.WaitForExit();
                    }
                }
            }
            else
            {
                if (silence)
                {
                    extractor.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    extractor.CreateNoWindow = true;
                    extractor.UseShellExecute = false;
                    extractor.RedirectStandardError = true;
                    Process.Start(extractor)?.WaitForExit();
                }
                else
                {
                    var proc = Process.Start(extractor);
                    if (proc != null)
                    {
                        WaitForHandel(proc);
                        FuckCancel(proc);

                        var sb = new StringBuilder(20);
                        do
                        {
                            if (proc.HasExited)
                                break;
                            Win32Api.SendMessage(proc.MainWindowHandle, 0x000D, (IntPtr)sb.Capacity, sb);
                            System.Threading.Thread.Sleep(100);
                        } while (sb.Length <= 0 || !(sb[0] == '1' & sb[1] == '0' & sb[2] == '0'));
                        if (!proc.HasExited) proc.Kill();
                    }
                }
            }
        }
        /// <summary>
        /// 解压缩安装包
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="destinationPath">文件路径</param>
        /// <param name="silence">是否静默解压</param>
        public static   void  ExtractFile(string sourceFile, string destinationPath, bool silence = true)
        {

            SevenZipBase.SetLibraryPath(@"lib\7z64.dll");
             
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }
        //D:\Documents\Visual Studio 2022\Projects\chrome - updater\ChromeUpdater.ArthasUI\bin\Debug\net6.0 - windows\\installer\chrome.7z
            Debug.WriteLine("当前文件名称" + sourceFile);
            //D:\tmp\chrome
            Debug.WriteLine("解压路径" + destinationPath);
            var extractor = new SevenZipExtractor(Path.Combine(CD, sourceFile));
            Debug.WriteLine("压缩包文件数量" + extractor.ArchiveFileData.Count);
            try
            {
                for (var i = 0; i < extractor.ArchiveFileData.Count; i++)
                {
                    extractor.ExtractFiles (destinationPath, extractor.ArchiveFileData[i].Index);
                    Debug.WriteLine($"这是第{i}个");
                }
                extractor.Dispose();
                 
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误");
                
            }
            
        } 
        public static void WaitForHandel( Process p)
        {
            while (p.MainWindowHandle == IntPtr.Zero)
                Thread.Sleep(100);
        }

        public static void FuckCancel(System.Diagnostics.Process p)
        {
            Win32Api.DeleteMenu(Win32Api.GetSystemMenu(p.Handle, false), Win32Api.SC_CLOSE, Win32Api.MF_BYCOMMAND);
            var hcancel = Win32Api.FindWindowEx(p.MainWindowHandle, 0, null, "取消");
            if (hcancel == IntPtr.Zero)
                hcancel = Win32Api.FindWindowEx(p.MainWindowHandle, 0, null, "Cancel");
            if (hcancel != IntPtr.Zero) Win32Api.EnableWindow(hcancel, false);
        }
        #endregion
        
    }
}
