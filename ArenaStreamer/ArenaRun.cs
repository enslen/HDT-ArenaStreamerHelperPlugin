using System.Collections.Generic;
using System.Linq;
using Hearthstone_Deck_Tracker.Stats;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using ASH.Properties;

namespace ASH
{
    public class ArenaRun
    {
        private Deck _deck;
        private string _class;
        private int _wins = 0;
        private int _losses = 0;
        private int _draws = 0;
        private GameResult _lastGameResult = GameResult.None;

        public ArenaRun(Deck deck)
        {
            _deck = deck;
            updateStats();
            _class = deck.Class;
        }

        // HDT GameEvents occur before stats are updated so we check game result and utilize that in updating stats
        public ArenaRun(Deck deck, GameResult result)
        {
            _deck = deck;
            updateStats(result);
            _class = deck.Class;
            _lastGameResult = result;
        }

        public bool IsArenaDeck
        {
            get
            { return _deck.IsArenaDeck; }
        }

        public bool IsNewArenaRun
        {
            get
            {
                if ((_wins == 0) && (_losses == 0) && (_draws == 0))
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsArenaRunCompleted
        {
            get
            {
                if ((_wins == 12) || (_losses == 3))
                {
                    return true;
                }
                return false;
            }
        }

        public GameResult LastGameResult
        {
            get
            {
                return _lastGameResult;
            }
        }

        public string Class
        {
            get
            { return _class; }
        }

        public int Wins
        {
            get
            { return _wins; }
        }

        public int Losses
        {
            get
            { return _losses; }
        }

        public int Draws
        {
            get
            { return _draws; }
        }

        public string StatString
        {
            get
            { return _wins.ToString() + "-" + _losses.ToString(); }
        }

        public string StatClassString
        {
            get
            { return StatString + " " + Class; }
        }

        public string StatClassStringFormatted
        {
            get
            { return statStringFormatted(StatString) + " " + Class; }
        }

        private string statStringFormatted(string text)
        {
            if (text.Length == 3)
            {
                text = " " + text;
            }
            return text;
        }

        private void updateStats(GameResult result = GameResult.None)
        {
            var relevantGames = _deck.GetRelevantGames();

            _wins = getGameCount(relevantGames, GameResult.Win);
            _losses = getGameCount(relevantGames, GameResult.Loss);
            //_draws = getGameCount(relevantGames, GameResult.Draw);
            // HDT GameEvents occur before games are recorded so update stats accordingly
            if (result == GameResult.Win)
                _wins += 1;
            if (result == GameResult.Loss)
                _losses += 1;
            //I chose not to add in a draw result here and for the GameEvents
            //Draws are just extremely rare for the most part and probably not worth showing
            //Maybe I will revisit this later...
            //if (result == GameResult.Draw)
            //_draws += 1;
        }

        private int getGameCount(List<GameStats> games, GameResult result)
        {
            if (games.Count == 0)
                return 0;

            return games.Count(g => g.Result == result);
        }

    }
}
