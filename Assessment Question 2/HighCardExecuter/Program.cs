using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HighCard;

namespace HighCardExecuter
{
    class Program
    {
        static void Main(string[] args) {

            var highCard = new Game();
            bool won = highCard.Play();
            if (won) {
                Console.WriteLine("Player Wins");

            }
            else {
                Console.WriteLine("House Wins");
            }

            Console.Read();

        }
    }
}
