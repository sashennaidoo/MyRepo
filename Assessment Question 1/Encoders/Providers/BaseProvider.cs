namespace Encoders.Providers
{

    /// <summary>
    /// Base class for Conversion providers
    /// 
    /// </summary>
    public abstract class BaseProvider : IProvider
    {
        protected static char[] _transcode;
        protected static int indexTable;

        /// <summary>
        /// Returns the index of a specified character in the transcode array object
        /// </summary>
        /// <param name="ch">The character to find the index of in the transcode
        ///                  array</param>
        /// <returns></returns>
        protected virtual int IndexOf(char ch) {
            int index;
            for (index = 0; index < _transcode.Length; index++)
                if (ch == _transcode[index])
                    break;
            return index;
        }

        public abstract string Encode(string toEncode);
        public abstract string Decode(string toDecode);
    }
}
