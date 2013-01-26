using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame.Utilities
{
    public interface ILogger
    {
        void WriteMessage(string message);
        bool Read(char[] toFind, string errorMessage, out char response);
    }
}
