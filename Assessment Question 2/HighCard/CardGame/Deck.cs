using System;
using System.Collections.Generic;
using CardGame.Extensions;
using CardGame.Utilities;

namespace CardGame
{
    /// <summary>
    /// This class will perform all deck logic for "hopefully" any card game
    /// The Deck will need to be newed up and initialized seperately
    /// Decks consists of Ace through King where 2 is the lowest and Ace is the highest
    /// and four suites (Spades, Hearts, Clubs, Diamods)
    /// The choice of basing this off a Dictionary was because Dictionary lookup performance
    /// is far faster than that using lists
    /// </summary>
    public class Deck : Dictionary<int, Card>, IDeck
    {

        #region Properties

        public int DeckCount { get { return _deckCount; } }
        public bool IncludeWild { get { return _includeWild; } }
        public int NumberOfCards {
            get { return _cardCollection.Count; }
        }
        #endregion Properties

        #region Variables

        private int _deckCount;
        private bool _includeWild;
        private List<Card> _cardCollection;

        #endregion Variables

        #region Ctor
        /// <summary>
        ///  Creates and instance and setup the Deck object
        ///  Wild cards are not included by default
        /// </summary>
        /// <param name="numberOfDecks">the total number of decks to be part of this parent deck</param>
        /// <param name="icludeWild">should wild cards be included in the deck</param>
        
        internal Deck(int numberOfDecks = 1, bool icludeWild = false)
        {
            _includeWild = IncludeWild;
            _deckCount = numberOfDecks;
        }

        #endregion Ctor

        #region Initialization Logic

        /// <summary>
        ///  Initializes a new Master Deck which will consist of 
        /// </summary>
        /// <param name="shuffle">Determines if the newly initialized deck should be shuffled</param>
        public void InitiliazeDeck(bool shuffle = true)
        {
            Clear();
            int cardCount = 0;
            _cardCollection = new List<Card>();
            for (var i=0; i < DeckCount; i++)
            {
                _cardCollection.AddRange(InitializeSingleDeck());
            }
            _cardCollection.ForEach(card => Add(++cardCount, card));
            if (shuffle && DeckCount > 0 && Count > 0)
                    ShuffleDeck();
        }

        private IEnumerable<Card> InitializeSingleDeck() {
            var deck = new List<Card>();

            foreach (var suit in Enum.GetValues(typeof(Suite))) {
                if ((Suite)suit == Suite.Wild)
                    continue;

                foreach (var rank in Enum.GetValues(typeof(Rank))) {
                    if ((Rank)rank == Rank.Wild)
                        continue;
                    deck.Add(new Card((Suite)suit, (Rank)rank));
                }
            }

            if (!IncludeWild) {
                deck.Add(new Card(Suite.Wild, Rank.Wild));
            }
            return deck;
        }

        #endregion Initialization Logic

        #region Shuffle Logic 

        /// <summary>
        /// Place cards in a random index in the Deck Dictionary object
        /// The chosen technique is the Knuth Shuffle as it seemed the most performant
        /// and as the previously shuffling technique (random shuffle and marking already used indeces)
        /// was slow and seemed error prone.
        /// </summary>
        public void ShuffleDeck()
        {
            var deck = new Deck(_deckCount, _includeWild);
            var random = new RNGWrapper();

            for (int i = Count -1 ; i > 0; i--)
            {
                var rand = random.Next(1, i) + 1;
                // Key at index 0 will never exist in a dictionary
                deck.Add(i, this[rand]);
            }
            Clear();
            deck.ForEach(card => Add(card.Key, card.Value));
        }

        #endregion Shuffle Logic

        #region Dictionary Lookups

        public Card GetCardAtIndex(int index)
        {
            return this[index];
        }

        #endregion Dictionary Lookups

    }
}
