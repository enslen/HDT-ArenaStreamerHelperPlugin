using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HDTStats = Hearthstone_Deck_Tracker.Stats.CompiledStats;
using Hearthstone_Deck_Tracker.Utility.Logging;
using Hearthstone_Deck_Tracker.Enums;
using HDT = Hearthstone_Deck_Tracker.Hearthstone;

namespace ASH
{
    public class ArenaStats
    {
        private string _currentClass;
        private object _allClassStats;
        private object _classStats;
        private TextStats _currentClassTextStats;
        private TextStats _allClassTextStats;

        public ArenaStats(string currentClass)
        {
            _currentClass = currentClass;
            _classStats = getClassStats(_currentClass);
            _allClassStats = getFilteredRuns(false).GroupBy(x => true).Select(x => new HDTStats.ClassStats("All", x)).FirstOrDefault();

            if (_classStats != null)
            {
                _currentClassTextStats = new TextStats((HDTStats.ClassStats)_classStats);
            }
            else
            {
                _currentClassTextStats = new TextStats();
            }

            if (_allClassStats != null)
            {
                _allClassTextStats = new TextStats((HDTStats.ClassStats)_allClassStats);
            }
            else
            {
                _allClassTextStats = new TextStats();
            }
            
        }

        private HDTStats.ClassStats getClassStats(string @class)
        {
            var runs = getFilteredRuns(false).Where(x => x.Class == @class).ToList();
            return !runs.Any() ? null : new HDTStats.ClassStats(@class, runs);
        }

        private IEnumerable<HDTStats.ArenaRun> getFilteredRuns(bool classFilter = false)
        {
            var filtered = HDTStats.ArenaStats.Instance.Runs;
            filtered = filtered.Where(x => !x.Deck.Archived);
            if (classFilter)
                filtered = filtered.Where(x => x.Class == _currentClass);
            return filtered;
        }

        public string CurrentClass
        {
            get
            {
                return _currentClass;
            }
        }

        public TextStats CurrentClassTextStats
        {
            get
            {
                return _currentClassTextStats;
            }
        }

        public TextStats AllClassTextStats
        {
            get
            {
                return _allClassTextStats;
            }
        }

        public class TextStats
        {
            private string _average;
            private string _runCount;
            private string _winLoss;
            private string _winRate;
            private string _bestRun;
            public TextStats()
            {
                _average = "0";
                _runCount = "0";
                _winLoss = "0-0";
                _winRate = "0%";
                _bestRun = "0-0";
            }
            public TextStats(HDTStats.ClassStats stats)
            {
                _average = stats.AverageWins.ToString();
                _runCount = stats.Runs.ToString();
                _winLoss = stats.Wins.ToString() + "-" + stats.Losses.ToString();
                _winRate = stats.WinRatePercent.ToString() + "%";
                _bestRun = stats.BestRun.Wins.ToString() + "-" + stats.BestRun.Losses.ToString();
                if (stats.Class == "All")
                    _bestRun = _bestRun + " " + stats.BestRun.Class;
            }

            public string CombinedFileStats(bool bestRun = true)
            {
                string text = string.Empty;
                text += _average;
                text += Environment.NewLine;
                text += _runCount;
                text += Environment.NewLine;
                text += _winLoss;
                text += Environment.NewLine;
                text += _winRate;
                // Best run is really only viable when looking at a single class
                // Add option to remove from combined file for all classes
                // Best run for all classes is still output to separate text file
                if (bestRun)
                {
                    text += Environment.NewLine;
                    text += _bestRun;
                }
                return text;
            }
            public string Average
            {
                get
                {
                    return _average;
                }
            }
            public string RunCount
            {
                get
                {
                    return _runCount;
                }
            }
            public string WinLoss
            {
                get
                {
                    return _winLoss;
                }
            }
            public string WinRate
            {
                get
                {
                    return _winRate;
                }
            }
            public string BestRun
            {
                get
                {
                    return _bestRun;
                }
            }
        }

    
    }
}
