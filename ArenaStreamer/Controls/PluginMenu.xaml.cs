using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;
using Hearthstone_Deck_Tracker.API;
using ASH.Properties;

namespace ASH.Controls
{
    /// <summary>
    /// Interaction logic for PluginMenu.xaml
    /// </summary>
    public partial class PluginMenu : MenuItem  
    {
        public PluginMenu()
        {
            InitializeComponent();
        }

        private void MenuItem_Settings_Click(object sender, RoutedEventArgs e)
        {
            ArenaStreamerHelperPlugin.OpenSettingsFlyout();
        }

    }
}
