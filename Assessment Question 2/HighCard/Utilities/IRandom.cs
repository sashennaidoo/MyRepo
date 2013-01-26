using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame.Utilities
{
    public interface IRandomWrapper : IDisposable
    {
        int Next();
        int Next(int max);
        int Next(int min, int max);
    }
}
