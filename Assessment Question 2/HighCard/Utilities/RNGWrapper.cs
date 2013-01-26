using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics.Contracts;

namespace CardGame.Utilities
{

    /// <summary>
    /// Simple wrapper for the RandomNumberGenerator class 
    /// This is because the standard random number generator is not
    /// very 'Random' and the RandomNumberGenerator in the Cryptography
    /// namespace works a little better at being random.
    /// </summary>
    public class RNGWrapper : RandomNumberGenerator, IRandomWrapper
    {

        #region Variables

        private static volatile RandomNumberGenerator _randomNumberGenerator;
        private object _disposeGuard = new object();

        #endregion Variables

        #region CTOR

        internal RNGWrapper()
        {
            _randomNumberGenerator = RandomNumberGenerator.Create();
        }

        #endregion CTOR

        #region Overriden RandomNumberGenerator Methods

        public override void GetBytes(byte[] data) {
            Contract.Requires(_randomNumberGenerator != null);
            _randomNumberGenerator.GetBytes(data);
        }

        public override void GetNonZeroBytes(byte[] data) {
            Contract.Requires(_randomNumberGenerator != null);
            _randomNumberGenerator.GetNonZeroBytes(data);
        }

        #endregion Overriden RandomNumberGenerator Methods

        #region Random Number Generation

        /// <summary>
        /// Returns a non-negative random number to the Max value of Int32 (2,147,483,647)
        /// </summary>
        /// <returns>Random Integer value between 0 and 2,147,483,647</returns>
        public int Next()
        {
            return Next(0, Int32.MaxValue);
        }

        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum
        /// </summary>
        /// <param name="max">The highest number to randomize to</param>
        public int Next(int max)
        {
            return Next(0, max);
        }

        /// <summary>
        /// Returns a random number within a specified range
        /// </summary>
        /// <param name="min">The inclusive lower bound of the random number returned</param>
        /// <param name="max">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue</param>
        /// <returns></returns>
        public int Next(int min, int max)
        {
            return Convert.ToInt32(Math.Round(NextDouble()*(max - min - 1)) + min);
        }

        /// <summary>
        /// Returns a random number between 0.0 and 1.0.
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            Contract.Requires(_randomNumberGenerator != null);
            byte[] data = new byte[4];
            _randomNumberGenerator.GetBytes(data);

            return (double)BitConverter.ToUInt32(data, 0)/UInt32.MaxValue;
        }

        #endregion Random Number Generation

        #region IDisposable Implementation

        void IDisposable.Dispose() {
            lock(_disposeGuard)
            {
                _randomNumberGenerator = null;
            }
        }

        #endregion IDisposable Implementation
    }
}
