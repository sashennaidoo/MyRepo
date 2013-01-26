using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encoders.Providers
{
    public interface IProvider
    {
        string Encode(string toEncode);
        string Decode(string toDecode);
    } 
}
