using System;
using System.Windows.Controls;
using HDTAPI = Hearthstone_Deck_Tracker.API;
using HDTStats = Hearthstone_Deck_Tracker.Stats.CompiledStats;
using Hearthstone_Deck_Tracker.Plugins;
using MahApps.Metro.Controls;

namespace ASH
{
    public class ArenaStreamerHelperPlugin : IPlugin
    {
        private MenuItem _ashMenuItem;
        private static Flyout _settings;

        public static Flyout SettingsFlyout
        {
            get
            {
                if (_settings == null)
                    SetSettingsFlyout();
                return _settings;
            }
        }

        public string Author
        {
            get { return "enslen"; }
        }

        public string ButtonText
        {
            get { return "Settings"; }
        }

        public string Description
        {
            get { return "A simple plugin exporting deck/arena stats to text files."; }
        }

        public MenuItem MenuItem
        {
            get { return _ashMenuItem; }
        }

        public string Name
        {
            get { return "Arena Streamer Helper"; }
        }

        public void OnButtonPress()
        {
            OpenSettingsFlyout();
        }

        public void OnLoad()
        {
            PluginConfig.Load();
            _ashMenuItem = new Controls.PluginMenu();
            SetSettingsFlyout();
            HDTAPI.GameEvents.OnGameStart.Add(GameEvents.GameStart);
            HDTAPI.GameEvents.OnGameWon.Add(GameEvents.GameWon);
            HDTAPI.GameEvents.OnGameLost.Add(GameEvents.GameLost);
        }

        public void OnUnload()
        {
            _settings.IsOpen = false;
        }

        public void OnUpdate()
        {
        }

        public Version Version
        {
            get { return new Version(0, 2, 0); }
        }

        public static void OpenSettingsFlyout()
        {
            if (_settings != null)
            {
                _settings.IsOpen = true;
            }
        }

        private static void SetSettingsFlyout()
        {
            var window = Hearthstone_Deck_Tracker.API.Core.MainWindow;
            var flyouts = window.Flyouts.Items;

            var settings = new Flyout();
            settings.Name = "PluginSettingsFlyout";
            settings.Position = Position.Left;
            //Panel.SetZIndex(settings, 100);
            settings.Header = "Arena Streamer Helper Settings";
            settings.Content = new Controls.PluginSettings();
            
            //newflyout.Width = 250;
            //settings.Theme = FlyoutTheme.Accent;
            flyouts.Add(settings);

            _settings = settings;
        }
    }
}
