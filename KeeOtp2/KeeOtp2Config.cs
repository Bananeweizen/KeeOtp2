﻿using KeePass;
using System;
using System.Windows.Forms;
using NHotkey.WindowsForms;
using KeePassLib.Native;

namespace KeeOtp2
{
    internal static class KeeOtp2Config
    {
        private const String HOTKEY_NAME = "HotKeyAutoType";

        internal static EventHandler<NHotkey.HotkeyEventArgs> handler { get; set; }

        public static void loadConfig()
        {
            if (OtpTime.getTimeType() == OtpTimeType.CustomNtpServer)
                OtpTime.pollCustomNtpServer();

            if (!NativeLib.IsUnix())
                registerHotKey();
        }

        public static void registerHotKey()
        {
            if (handler != null)
                HotkeyManager.Current.AddOrReplace(HOTKEY_NAME, KeeOtp2Config.HotKeyKeys, handler);
        }

        public static void unregisterHotKey()
        {
            HotkeyManager.Current.Remove(HOTKEY_NAME);
        }



        private const String PATH_PLUGINNAME = "KeeOtp2";

        private const String PATH_USE_HOTKEY = PATH_PLUGINNAME + ".UseHotKey";
        private const String PATH_HOTKEY_SEQUENCE = PATH_PLUGINNAME + ".HotKeySequence";
        private const String PATH_HOTKEY_KEYS = PATH_PLUGINNAME + ".HotKeyKeys";
        private const String PATH_SHOW_CONTEXT_MENU_ITEM = PATH_PLUGINNAME + ".ShowContextMenuItem";
        private const String PATH_USE_LOCAL_HOTKEY = PATH_PLUGINNAME + ".UseLocalHotKey";
        private const String PATH_LOCAL_HOTKEY_KEYS = PATH_PLUGINNAME + ".LocalHotKeyKeys";

        private const String PATH_TIME_TYPE = PATH_PLUGINNAME + ".TimeType";
        private const String PATH_FIXED_TIME_OFFSET = PATH_PLUGINNAME + ".FixedTimeOffset";
        private const String PATH_CUSTOM_NTP_SERVER = PATH_PLUGINNAME + ".CustomNTPServer";
        private const String PATH_OVERRIDE_BUILT_IN = PATH_PLUGINNAME + ".OverrideBuiltIn";

        internal static bool UseHotKey
        {
            get
            {
                if (!NativeLib.IsUnix())
                    return Program.Config.CustomConfig.GetBool(PATH_USE_HOTKEY, true);
                return false;
            }
            set
            {
                Program.Config.CustomConfig.SetBool(PATH_USE_HOTKEY, value);
            }
        }

        internal static string HotKeySequence
        {
            get
            {
                return Program.Config.CustomConfig.GetString(PATH_HOTKEY_SEQUENCE, KeeOtp2Ext.BuiltInTotpPlaceHolder);
            }
            set
            {
                Program.Config.CustomConfig.SetString(PATH_HOTKEY_SEQUENCE, value);
            }
        }

        internal static Keys HotKeyKeys
        {
            get
            {
                return (Keys)Enum.Parse(typeof(Keys), Program.Config.CustomConfig.GetString(PATH_HOTKEY_KEYS, (Keys.Control | Keys.Alt | Keys.T).ToString()));
            }
            set
            {
                Program.Config.CustomConfig.SetString(PATH_HOTKEY_KEYS, value.ToString());
            }
        }

        internal static bool ShowContextMenuItem
        {
            get
            {
                return Program.Config.CustomConfig.GetBool(PATH_SHOW_CONTEXT_MENU_ITEM, true);
            }
            set
            {
                Program.Config.CustomConfig.SetBool(PATH_SHOW_CONTEXT_MENU_ITEM, value);
            }
        }

        internal static bool UseLocalHotKey
        {
            get
            {
                return Program.Config.CustomConfig.GetBool(PATH_USE_LOCAL_HOTKEY, true);
            }
            set
            {
                Program.Config.CustomConfig.SetBool(PATH_USE_LOCAL_HOTKEY, value);
            }
        }

        internal static Keys LocalHotKeyKeys
        {
            get
            {
                return (Keys)Enum.Parse(typeof(Keys), Program.Config.CustomConfig.GetString(PATH_LOCAL_HOTKEY_KEYS, (Keys.Control | Keys.T).ToString()));
            }
            set
            {
                Program.Config.CustomConfig.SetString(PATH_LOCAL_HOTKEY_KEYS, value.ToString());
            }
        }

        internal static OtpTimeType TimeType
        {
            get
            {
                return (OtpTimeType)Enum.Parse(typeof(OtpTimeType), Program.Config.CustomConfig.GetString(PATH_TIME_TYPE, OtpTimeType.SystemTime.ToString()));
            }
            set
            {
                Program.Config.CustomConfig.SetString(PATH_TIME_TYPE, value.ToString());
            }
        }

        internal static long FixedTimeOffset
        {
            get
            {
                return Program.Config.CustomConfig.GetLong(PATH_FIXED_TIME_OFFSET, 0);
            }
            set
            {
                Program.Config.CustomConfig.SetLong(PATH_FIXED_TIME_OFFSET, value);
            }
        }

        internal static string CustomNTPServer
        {
            get
            {
                return Program.Config.CustomConfig.GetString(PATH_CUSTOM_NTP_SERVER, "time-a.nist.gov");
            }
            set
            {
                Program.Config.CustomConfig.SetString(PATH_CUSTOM_NTP_SERVER, value.ToString());
            }
        }

        internal static bool OverrideBuiltInTime
        {
            get
            {
                return Program.Config.CustomConfig.GetBool(PATH_OVERRIDE_BUILT_IN, true);
            }
            set
            {
                Program.Config.CustomConfig.SetBool(PATH_OVERRIDE_BUILT_IN, value);
            }
        }
    }
}
