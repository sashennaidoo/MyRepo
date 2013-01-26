using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGame;

namespace CardGame
{
    public interface IDeck
    {
        void InitiliazeDeck(bool shuffle = true);
        void ShuffleDeck();
        Card GetCardAtIndex(int index);

        int DeckCount { get; }
        int NumberOfCards { get; }
    }
}
