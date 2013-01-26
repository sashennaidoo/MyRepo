using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGame.Utilities;

namespace CardGame
{
    public static class CardGameFactory
    {
        public static IDeck GenerateDeck(int numberOfDecks, bool icludeWild)
        {
            return new Deck(numberOfDecks, icludeWild);
        }

        public static IDeck GenerateDeck(int numberOfDecks)
        {
            return new Deck(numberOfDecks);
        }

        public static IDeck GenerateDeck()
        {
            return new Deck();
        }

        public static IRandomWrapper GenerateRNGWrapper()
        {
            return new RNGWrapper();
        }

        public static ILogger CreateConsoleLogger()
        {
            return new ConsoleLogger();
        }
    }
}
