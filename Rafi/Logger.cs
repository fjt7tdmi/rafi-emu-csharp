using System;

namespace Rafi
{
    internal class Logger
    {
        public void Trace(string message)
        {
#if false
            Console.WriteLine(message);
#endif
        }
    }
}
