using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using ASH.Properties;
using Plugin = ASH.ArenaStreamerHelperPlugin;

namespace ASH.Controls
{
    /// <summary>
    /// Interaction logic for SettingsControl.xaml
    /// </summary>
    public partial class PluginSettings : System.Windows.Controls.UserControl
    {

        private bool _initialized;

        public PluginSettings()
        {
            InitializeComponent();
            LoadSettings();
            _initialized = true;
        }

        private void LoadSettings()
        {
            checkBox_ActiveDeck.IsChecked = PluginConfig.Instance.ActiveDeckEnable;
            checkBox_ArenaRuns.IsChecked = PluginConfig.Instance.ArenaRunsEnable;
            radioButton_Ascending.IsChecked = PluginConfig.Instance.ArenaRunsOrder;
            radioButton_Descending.IsChecked = !PluginConfig.Instance.ArenaRunsOrder;
            textBox_ActiveDeckFile.Text = PluginConfig.Instance.ActiveDeckPath;
            textBox_ArenaRunsFile.Text = PluginConfig.Instance.ArenaRunsPath;
            textBox_MaxRuns.Text = PluginConfig.Instance.ArenaRunsLimit.ToString();
            checkBox_AdvancedStats.IsChecked = PluginConfig.Instance.AdvancedStatsEnable;
            textBox_AdvancedStatsFolder.Text = PluginConfig.Instance.AdvancedStatsPath;

        }

        private void checkBox_ActiveDeck_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            PluginConfig.Instance.ActiveDeckEnable = true;
            PluginConfig.Save();
        }



        private void checkBox_ActiveDeck_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            PluginConfig.Instance.ActiveDeckEnable = false;
            PluginConfig.Save();
        }

        private void buttonBrowse_ActiveDeck_Click(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            textBox_ActiveDeckFile.Text = getFilePath();
            PluginConfig.Instance.ActiveDeckPath = textBox_ActiveDeckFile.Text;
            PluginConfig.Save();
        }

        private void buttonBrowse_ArenaRuns_Click(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            textBox_ArenaRunsFile.Text = getFilePath();
            PluginConfig.Instance.ArenaRunsPath = textBox_ArenaRunsFile.Text;
            PluginConfig.Save();
        }

        private string getFilePath()
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Title = "Select or create a text file";

                openFile.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
                openFile.FilterIndex = 1;
                openFile.Multiselect = false;

                DialogResult result = openFile.ShowDialog();

                if (result == DialogResult.OK)
                {
                    return openFile.FileName;
                }
            }
            return string.Empty;
        }

        private string getFolderPath()
        {
            using (FolderBrowserDialog openFolder = new FolderBrowserDialog())
            {
                openFolder.Description = "Select or create a folder.";
                openFolder.ShowNewFolderButton = true;
                openFolder.RootFolder = System.Environment.SpecialFolder.MyComputer;
                DialogResult result = openFolder.ShowDialog();

                if (result == DialogResult.OK)
                {
                    return openFolder.SelectedPath;
                }
            }

            return string.Empty;
        }

        private void checkBox_ArenaRuns_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            PluginConfig.Instance.ArenaRunsEnable = true;
            PluginConfig.Save();
        }

        private void checkBox_ArenaRuns_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            PluginConfig.Instance.ArenaRunsEnable = false;
            PluginConfig.Save();
        }

        private void textBox_MaxRuns_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_initialized)
                return;

            int limit = 0;
            bool result = int.TryParse(textBox_MaxRuns.Text, out limit);
            if (result)
            {
                PluginConfig.Instance.ArenaRunsLimit = limit;
                PluginConfig.Save();
            }
            else
            {
                textBox_MaxRuns.Text = PluginConfig.Instance.ArenaRunsLimit.ToString();
            }
        }

        private void radioButton_Descending_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            PluginConfig.Instance.ArenaRunsOrder = false;
            PluginConfig.Save();
        }

        private void radioButton_Ascending_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            PluginConfig.Instance.ArenaRunsOrder = true;
            PluginConfig.Save();
        }

        private void checkBox_AdvancedStats_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            PluginConfig.Instance.AdvancedStatsEnable = true;
            PluginConfig.Save();
        }

        private void checkBox_AdvancedStats_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            PluginConfig.Instance.AdvancedStatsEnable = false;
            PluginConfig.Save();
        }

        private void buttonBrowse_AdvancedStats_Click(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            textBox_AdvancedStatsFolder.Text = getFolderPath();
            PluginConfig.Instance.AdvancedStatsPath = textBox_AdvancedStatsFolder.Text;
            PluginConfig.Save();
        }
    }
}
