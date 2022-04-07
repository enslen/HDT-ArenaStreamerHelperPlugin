using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Threading.Tasks;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Plugins;
using Hearthstone_Deck_Tracker.Utility;
using Hearthstone_Deck_Tracker.Utility.Logging;


namespace ASH
{
    public class PluginConfig
    {

        private static PluginConfig _config;
        public const string _configFile = "ArenaStreamerConfig.xml";
        public static readonly string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                                                    + @"\HearthstoneDeckTracker";

        [DefaultValue(true)]
        public bool ActiveDeckEnable = true;

        [DefaultValue("")]
        public string ActiveDeckPath = "";

        [DefaultValue(true)]
        public bool ArenaRunsEnable = true;

        [DefaultValue("")]
        public string ArenaRunsPath = "";

        [DefaultValue(8)]
        public int ArenaRunsLimit = 8;

        [DefaultValue(true)]
        public bool ArenaRunsOrder = true;

        [DefaultValue(true)]
        public bool AdvancedStatsEnable = true;

        [DefaultValue("")]
        public string AdvancedStatsPath = "";

        public string ConfigPath => AppDataPath + "\\" + _configFile;

        public static PluginConfig Instance
        {
            get
            {
                if (_config != null)
                    return _config;
                _config = new PluginConfig();
                _config.ResetAll();
                return _config;
            }
        }

        public static void Save() => XmlManager<PluginConfig>.Save(Instance.ConfigPath, Instance);

        public static void Load()
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                var config = Path.Combine(AppDataPath, _configFile);
                if (File.Exists(_configFile))
                {
                    _config = XmlManager<PluginConfig>.Load(_configFile);
                }
                else if (File.Exists(config))
                {
                    _config = XmlManager<PluginConfig>.Load(config);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                _config = new PluginConfig();
                _config.ResetAll();
            }
        }

        public void ResetAll()
        {
            foreach (var field in GetType().GetFields())
            {
                var attr = (DefaultValueAttribute)field.GetCustomAttributes(typeof(DefaultValueAttribute), false).FirstOrDefault();
                if (attr != null)
                    field.SetValue(this, attr.Value);
            }
        }
    }
}
