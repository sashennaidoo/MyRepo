using System;


namespace Encoders.Providers
{
    public class Base64Provider : BaseProvider
    {
        /// <summary>
        /// The logic of the this class seemed correct, there were minor 
        /// changes made to the class, removing the '=' from the transcode 
        /// character array because it isn't part of the Base64 Index table
        /// </summary>
        public Base64Provider()
        {
            _transcode = new char[64];
            PrepareTranscoder();
        }

        private void PrepareTranscoder()
        {
            for (int i = 0; i < 64; i++)
            {
                _transcode[i] = (char) ((int) 'A' + i);

                if (i > 25 )
                {
                    _transcode[i] = (char)((int)_transcode[i] + 6);
                }
                 if (i > 51)
                {
                    _transcode[i] = (char)((int)_transcode[i] - 0x4b);
                }
            }
            _transcode[62] = '+';
            _transcode[63] = '/';
        }

        public override string Encode(string toEncode) {
            int l = toEncode.Length;
            int conversionbaselength = (l / 3 + (Convert.ToBoolean(l % 3) ? 1 : 0)) * 4;

            char[] output = new char[conversionbaselength];
            for (int i = 0; i < conversionbaselength; i++) {
                output[i] = '=';
            }

            int c = 0;
            int reflex = 0;
            const int s = 0x3f;

            for (int j = 0; j < l; j++) {
                reflex <<= 8;
                reflex &= 0x00ffff00;
                reflex += toEncode[j];

                int x = ((j % 3) + 1) * 2;
                int mask = s << x;
                while (mask >= s) {
                    int pivot = (reflex & mask) >> x;
                    output[c++] = _transcode[pivot];
                    int invert = ~mask;
                    reflex &= invert;
                    mask >>= 6;
                    x -= 6;
                }
            }

            switch (l % 3) {
                case 1:
                    reflex <<= 4;
                    if (output.Length > c++) output[c] = _transcode[reflex];
                    break;
                case 2:
                    reflex <<= 2;
                    if (output.Length > c++) output[c] = _transcode[reflex];
                    break;

            }
            Console.WriteLine("{0} --> {1}\n", toEncode, new string(output));
            return new string(output);
        }

        public override string Decode(string toDecode) {
            int l = toDecode.Length;
            int cb = (l / 4 + ((Convert.ToBoolean(l % 4)) ? 1 : 0)) * 3;
            char[] output = new char[cb];
            int c = 0;
            int bits = 0;
            int reflex = 0;
            for (int j = 0; j < l; j++) {
                reflex <<= 6;
                bits += 6;
                bool fTerminate = ('=' == toDecode[j]);
                if (!fTerminate)
                    reflex += IndexOf(toDecode[j]);

                while (bits >= 8) {
                    int mask = 0x000000ff << (bits % 8);
                    output[c++] = Convert.ToChar((reflex & mask) >> (bits % 8));
                    int invert = ~mask;
                    reflex &= invert;
                    bits -= 8;
                }

                if (fTerminate)
                    break;
            }
            Console.WriteLine("{0} --> {1}\n", toDecode, new string(output));
            return new string(output);
        }
    }
}
