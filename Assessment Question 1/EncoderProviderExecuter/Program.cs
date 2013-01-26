using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Encoders.Providers;

namespace EncoderProviderExecuter
{
    class Program
    {
        static void Main(string[] args) {

            string test_string = "This is a test string";

            Base64Provider provider = new Base64Provider();

            // The test initially failed if the encoding and decoding 
            // were succesful
            // This was due to
            if (!Convert.ToBoolean(String.Compare(test_string, provider.Decode(provider.Encode(test_string))))) {
                Console.WriteLine("Test succeeded");
            }
            else {
                Console.WriteLine("Test failed");
            }
            Console.Read();
        }
    }
}
