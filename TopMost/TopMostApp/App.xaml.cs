using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TopMostApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public const string TITLE = @"Top Most Friend";

        private const string GUID =
#if DEBUG
            @"{1A22D9CA-2AA9-48F2-B007-3A48CF205CDD}";
#else
            @"{5BE25191-E1E2-48A7-B038-E986CD989E91}";
#endif
        private static readonly Mutex GlobalMutex = new Mutex(true, GUID);

        private const string CUSTOM_LANGUAGE = @"TopMostFriendLanguage.xml";

        public const string FOREGROUND_HOTKEY_ATOM = @"{86795D64-770D-4BD6-AA26-FA638FBAABCF}";
#if DEBUG
        public const string FOREGROUND_HOTKEY_SETTING = @"ForegroundHotKey_DEBUG";
#else
        public const string FOREGROUND_HOTKEY_SETTING = @"ForegroundHotKey";
#endif

        public const string PROCESS_SEPARATOR_SETTING = @"InsertProcessSeparator";
        public const string LIST_SELF_SETTING = @"ListSelf";
        public const string SHOW_EMPTY_WINDOW_SETTING = @"ShowEmptyWindowTitles";
        public const string LIST_BACKGROUND_PATH_SETTING = @"ListBackgroundPath";
        public const string LIST_BACKGROUND_LAYOUT_SETTING = @"ListBackgroundLayout";
        public const string ALWAYS_ADMIN_SETTING = @"RunAsAdministrator";
        public const string TOGGLE_BALLOON_SETTING = @"ShowNotificationOnHotKey";
        public static readonly bool ToggleBalloonDefault = Environment.OSVersion.Version.Major < 10;
        public const string SHIFT_CLICK_BLACKLIST = @"ShiftClickToBlacklist";
        public const string TITLE_BLACKLIST = @"TitleBlacklist";
        public const string SHOW_HOTKEY_ICON = @"ShowHotkeyIcon";
        public const string SHOW_WINDOW_LIST = @"ShowWindowList";
        public const string LAST_VERSION = @"LastVersion";
        public const string ALWAYS_RETRY_ELEVATED = @"AlwaysRetryElevated";
        public const string REVERT_ON_EXIT = @"RevertOnExit";
        public const string LANGUAGE = @"Language";

          private TaskbarIcon? notifyIcon;
        private static Icon OriginalIcon;

        protected override void OnStartup(StartupEventArgs e)
        { base.OnStartup(e);

            notifyIcon = (TaskbarIcon)Current.FindResource("TaskbarIcon");
           
        }
        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }

    }
}
