using System;
using System.IO;
using System.Linq;
using Hearthstone_Deck_Tracker.Utility.Logging;
using Hearthstone_Deck_Tracker.Enums;
using ASH.Properties;

namespace ASH
{
    public class HelperModule
    {
        public static void GameWon(ArenaRun run, ArenaStats currentStats)
        {
            updateActiveDeck(run.StatString);
            endArenaRun(run);
            updateAdvancedStatsFiles(currentStats);
        }

        public static void GameLost(ArenaRun run, ArenaStats currentStats)
        {
            updateActiveDeck(run.StatString);
            endArenaRun(run);
            updateAdvancedStatsFiles(currentStats);
        }

        public static void GameStart(ArenaRun run, ArenaStats currentStats)
        {
            updateActiveDeck(run.StatString);
            //endArenaRun(run);
            updateAdvancedStatsFiles(currentStats);
        }

        private static void updateActiveDeck(string stats)
        {
            if (PluginConfig.Instance.ActiveDeckEnable)
            {
                UpdateTextFile(PluginConfig.Instance.ActiveDeckPath, stats);
            }
        }

        private static void endArenaRun(ArenaRun run)
        {
            if (run.IsArenaDeck && run.IsArenaRunCompleted)
            {
                string stats = run.StatClassStringFormatted;
                updateArenaRunsFile(stats);
            }
        }

        private static string padStringWithSpace(string text)
        {
            if (text.Length == 1)
            {
                text = " " + text;
            }
            return text;
        }

        private static void updateAdvancedStatsFiles(ArenaStats currentStats)
        {
            if (PluginConfig.Instance.AdvancedStatsEnable)
            {
                string folderPath = PluginConfig.Instance.AdvancedStatsPath;
                string currentClassAverage = folderPath + "\\CurrentClass\\Average.txt";
                string currentClassRuns = folderPath + "\\CurrentClass\\Runs.txt";
                string currentClassWinLoss = folderPath + "\\CurrentClass\\WinLoss.txt";
                string currentClassWinRate = folderPath + "\\CurrentClass\\WinRate.txt";
                string currentClassBestRun = folderPath + "\\CurrentClass\\BestRun.txt";
                string currentClassAllStats = folderPath + "\\CurrentClass\\AllStats.txt";
                string currentClassLabels = folderPath + "\\CurrentClass\\Labels.txt";
                string currentClassText = folderPath + "\\CurrentClass\\Class.txt";
                string allClassAverage = folderPath + "\\AllClasses\\Average.txt";
                string allClassRuns = folderPath + "\\AllClasses\\Runs.txt";
                string allClassWinLoss = folderPath + "\\AllClasses\\WinLoss.txt";
                string allClassWinRate = folderPath + "\\AllClasses\\WinRate.txt";
                string allClassBestRun = folderPath + "\\AllClasses\\BestRun.txt";
                string allClassAllStats = folderPath + "\\AllClasses\\AllStats.txt";
                string allClassLabels = folderPath + "\\AllClasses\\Labels.txt";
                UpdateTextFile(currentClassAverage, currentStats.CurrentClassTextStats.Average);
                UpdateTextFile(currentClassRuns, currentStats.CurrentClassTextStats.RunCount);
                UpdateTextFile(currentClassWinLoss, currentStats.CurrentClassTextStats.WinLoss);
                UpdateTextFile(currentClassWinRate, currentStats.CurrentClassTextStats.WinRate);
                UpdateTextFile(currentClassBestRun, currentStats.CurrentClassTextStats.BestRun);
                UpdateTextFile(currentClassAllStats, currentStats.CurrentClassTextStats.CombinedFileStats(true));
                UpdateTextFile(currentClassText, currentStats.CurrentClass);
                UpdateTextFile(allClassAverage, currentStats.AllClassTextStats.Average);
                UpdateTextFile(allClassRuns, currentStats.AllClassTextStats.RunCount);
                UpdateTextFile(allClassWinLoss, currentStats.AllClassTextStats.WinLoss);
                UpdateTextFile(allClassWinRate, currentStats.AllClassTextStats.WinRate);
                UpdateTextFile(allClassBestRun, currentStats.AllClassTextStats.BestRun);
                UpdateTextFile(allClassAllStats, currentStats.AllClassTextStats.CombinedFileStats(false));
                writeStatLabels(currentClassLabels, true);
                writeStatLabels(allClassLabels, false);
            }

        }

        private static void writeStatLabels(string filePath, bool bestRun)
        {
            if (!File.Exists(filePath))
            {
                string labelText = "    Average:" + Environment.NewLine
                    + "       Runs:" + Environment.NewLine
                    + "Wins-Losses:" + Environment.NewLine
                    + "   Win Rate:";
                if (bestRun)
                    labelText = labelText + Environment.NewLine + "   Best Run:";
                UpdateTextFile(filePath, labelText, false);
            }
        }

        public static void UpdateTextFile(string filePath, string text, bool append = false)
        {
            try
            {
                FileInfo file = new FileInfo(filePath);
                file.Directory.Create();
                if (append)
                {
                    File.AppendAllText(filePath, text);
                }
                else
                {
                    File.WriteAllText(filePath, text);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error updating text file: " + ex.Message, "ArenaStreamerHelper");
            }
        }

        public static int ReadIntegerFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string file = File.ReadAllText(filePath);
                int num;
                if (Int32.TryParse(file, out num))
                    return num;
            }

            return 0;
        }

        private static void updateArenaRunsFile(string text)
        {
            if (PluginConfig.Instance.ArenaRunsEnable)
            {
                try
                {
                    var lines = File.ReadAllLines(PluginConfig.Instance.ArenaRunsPath);

                    if (PluginConfig.Instance.ArenaRunsOrder == true)
                    {
                        writeToFileAscending(text);
                    }
                    else
                    {
                        writeToFileDescending(text);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Error updating arena runs file: " + ex.Message, "ArenaStreamerHelper");
                }
            }
        }

        private static void writeToFileAscending(string text)
        {
            if (File.Exists(PluginConfig.Instance.ArenaRunsPath))
            {
                var lines = File.ReadAllLines(PluginConfig.Instance.ArenaRunsPath);

                if (lines.Length > 0)
                {
                    if (lines.Length >= PluginConfig.Instance.ArenaRunsLimit)
                    {
                        File.WriteAllLines(PluginConfig.Instance.ArenaRunsPath, lines.Skip(1));
                    }
                    else
                    {
                        File.WriteAllLines(PluginConfig.Instance.ArenaRunsPath, lines);
                    }
                }
            }
            File.AppendAllText(PluginConfig.Instance.ArenaRunsPath, text);
        }
        
        //This was necessary because the File Write/AppendAllLines methods add line breaks after each line
        //Only noticed it when doing descending order, it does not affect ascending order since the Write/AppendAllText does not add extra lines
        //This could probably be better done with StreamReader/Writer, both of these methods are a somewhat dirty fix
        private static void writeToFileDescending(string text)
        {
            if (File.Exists(PluginConfig.Instance.ArenaRunsPath))
            {
                var lines = File.ReadAllLines(PluginConfig.Instance.ArenaRunsPath);
                File.WriteAllText(PluginConfig.Instance.ArenaRunsPath, text);

                if (lines.Length > 0)
                {
                    int linesCount;

                    if (lines.Length >= PluginConfig.Instance.ArenaRunsLimit)
                    {
                        linesCount = lines.Length - 1;
                    }
                    else
                    {
                        linesCount = lines.Length;
                    }

                    for (int x = 0; x < linesCount; x++)
                    {
                        File.AppendAllText(PluginConfig.Instance.ArenaRunsPath, Environment.NewLine + lines[x]);
                    }
                }
            }
            else
            {
                File.WriteAllText(PluginConfig.Instance.ArenaRunsPath, text);
            }
        }

    }
}
