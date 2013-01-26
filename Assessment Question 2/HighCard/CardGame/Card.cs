using System;

namespace CardGame
{
    public class Card : IComparable
    {
        #region Variables

        private Suite _suite;
        private Rank _rank;

        #endregion Variables

        #region Properties

        public Suite CardSuite { get { return _suite; } }
        public Rank CardRank { get { return _rank; } }

        #endregion Properties

        #region CTOR

        /// <summary>
        /// Initializes a card and sets the cards rank and suite
        /// </summary>
        /// <param name="suite">The suite of the newly instantiated card</param>
        /// <param name="rank">The suite of the newly instantiated card</param>
        public Card(Suite suite, Rank rank) {
            _suite = suite;
            _rank = rank;
        }

        #endregion CTOR

        #region IComparable implementation

        /// <summary>
        /// Implementation of IComparable interface member CompareTo
        /// To facilitate for comparison by card rank and card suite
        /// If the rank of the cards being compared are the same, the
        /// suites for the cards will be compared, 0 (zero) indicates
        /// the cards are identical
        /// </summary>
        /// <param name="otherCard">The card being compared to</param>
        /// <returns>an integer indicating the difference between the rank/suite of the cards</returns>
        public int CompareTo(object otherCard) {
            Card other = (Card) otherCard;
            if (_rank - other.CardRank == 0)
            {
                return _suite - other.CardSuite;
            }
            else
            {
                return _rank - other.CardRank;
            }

        }

        #endregion IComparable implementation
    }
}
