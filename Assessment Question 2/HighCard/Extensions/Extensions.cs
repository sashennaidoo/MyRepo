using System;
using System.Collections.Generic;

namespace CardGame.Extensions
{
    /// <summary>
    /// Extension methods for the Deck class
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        ///  Performs an action on the Collection for a specified sequence
        /// </summary>
        /// <typeparam name="T">T in the case would in our case be Card</typeparam>
        /// <param name="sequence">the collection the foreach will iterate over</param>
        /// <param name="action">The action to be performed on the sequence</param>
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action) {
            if (sequence == null) {
                throw new ArgumentNullException("sequence");
            }
            if (action == null) {
                throw new ArgumentNullException("action");
            }
            IEnumerable<T> removeCollection = sequence;

                foreach (var item in sequence) {
                    action(item);
                }
        }

    }
}
