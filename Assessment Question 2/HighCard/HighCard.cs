using System;
using CardGame;
using CardGame.Utilities;

namespace HighCard
{
    public class Game
    {
        #region Variables

        private readonly IRandomWrapper _randomizer;
        private readonly IDeck _deck;
        private readonly ILogger _consoleLogger;

        #endregion Variables

        #region CTOR

        public Game() {
            _consoleLogger = CardGameFactory.CreateConsoleLogger();

            _consoleLogger.WriteMessage("How many decks would you like to play with");
            int decksToPlay;
            while (!Int32.TryParse(Console.ReadLine(), out decksToPlay)) {
                _consoleLogger.WriteMessage("Value entered is not a number");
                _consoleLogger.WriteMessage("How many decks would you like to play with");
            }
            _deck = CardGameFactory.GenerateDeck(decksToPlay);
            _randomizer = CardGameFactory.GenerateRNGWrapper();

            _deck.InitiliazeDeck();
        }

        #endregion CTOR

        #region Gameplay Methods

        public bool Play() {
            bool playerWon = DrawCard();
            _randomizer.Dispose();

            return playerWon;
        }

        private bool DrawCard() {
            bool playAgain = false;
            do {
                Card playerCard = Deal();
                Card opponentCard = Deal();

                WriteInformation("Player", playerCard);
                WriteInformation("Opponent", opponentCard);

                int playerRelativeCardValue = playerCard.CompareTo(opponentCard);
                if (playerRelativeCardValue == 0) {
                    
                    _consoleLogger.WriteMessage("Game is Tie");
                    playAgain = Replay(); 
                }
                else {
                    return GetPlayerWinState(playerRelativeCardValue);
                }
            } while (playAgain);

            return false;
        }

        private Card Deal() {
            int cardIndex = _randomizer.Next(1, _deck.NumberOfCards);
            Card card = _deck.GetCardAtIndex(cardIndex);
            return card;
        }

        private bool Replay() {
            char[] possibleResponses = new char[] { 'Y', 'N' };
            char ans;
            bool invalidResponse = true;

            do {
                _consoleLogger.WriteMessage("Deal again (Y/N)");
                invalidResponse = _consoleLogger.Read(possibleResponses,
                                                      string.Format("Please Enter either {0} or {1}",
                                                                    possibleResponses[0], possibleResponses[1]), out ans);
            } while (invalidResponse);
            return ans == 'Y' ? true : false;

        }

        // ReSharper disable MemberCanBeMadeStatic.Local
        private bool GetPlayerWinState(int playerRelativeCardValue)
            // ReSharper restore MemberCanBeMadeStatic.Local
        {
            return playerRelativeCardValue > 0 ? true : false;
        }

        #endregion Gameplay Methods

        #region Logger Methods

        private void WriteInformation(string playerName, Card card)
        {
            _consoleLogger.WriteMessage(string.Format("{0} Card Information: {1} {2}", playerName, card.CardRank, card.CardSuite));
        }

        #endregion Logger Methods

    }

}
