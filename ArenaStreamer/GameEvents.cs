using System;
using System.Threading.Tasks;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Enums;

namespace ASH
{
    public class GameEvents
    {
        public static void GameWon()
        {
            if (checkActiveDeck())
            {
                ArenaRun _run = new ArenaRun(DeckList.Instance.ActiveDeck, GameResult.Win);
                ArenaStats _currentStats = new ArenaStats(_run.Class);
                Task.Run(()=> HelperModule.GameWon(_run, _currentStats));
            }
        }

        public static void GameLost()
        {
            if (checkActiveDeck())
            {
                ArenaRun _run = new ArenaRun(DeckList.Instance.ActiveDeck, GameResult.Loss);
                ArenaStats _currentStats = new ArenaStats(_run.Class);
                Task.Run(() => HelperModule.GameLost(_run, _currentStats));
            }
        }

        public static void GameStart()
        {
            if (checkActiveDeck())
            {
                ArenaRun _run = new ArenaRun(DeckList.Instance.ActiveDeck);
                ArenaStats _currentStats = new ArenaStats(_run.Class);
                Task.Run(() => HelperModule.GameStart(_run, _currentStats));
            }
        }

        private static bool checkActiveDeck()
        {
            if (DeckList.Instance.ActiveDeck != null)
                return true;
            return false;
        }
    }
}
