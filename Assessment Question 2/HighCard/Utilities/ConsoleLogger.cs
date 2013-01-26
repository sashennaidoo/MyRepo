using System;
using System.Linq;
using CardGame.Extensions;

namespace CardGame.Utilities
{
    /// <summary>
    /// Simple logger that logs to console
    /// The reason this exists is because it can be swapped out for 
    /// another a type of writer, ie. a trace writer
    /// </summary>
    class ConsoleLogger : ILogger
    {
        /// <summary>
        /// Writes a string of information to the console
        /// </summary>
        /// <param name="message">string to be written</param>
        public void WriteMessage(string message) {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Reads and tests and input from the console
        /// </summary>
        /// <param name="toFind">characters to find</param>
        /// <param name="errorMessage">error message to display if the character set "toFind" was not found</param>
        /// <param name="response">the actual response character (the first character of the string)</param>
        /// <returns>The result of whether any characters were found in the input stream</returns>
        public bool Read(char[] toFind, string errorMessage, out char response) {
            string consoleInfo = Console.ReadLine();
            bool found = false;

            if (consoleInfo != null)
            {
                response = consoleInfo.ToUpper().First();
                toFind.ForEach(f => found = (consoleInfo.ToUpper().First().Equals(f)));
            }
            else
            {
                response = char.MinValue;
            }
            if (!found)
            {
                WriteMessage(errorMessage);
            }
            else
            {
                return found = true;
            }
            return found;
        }
    }
}
