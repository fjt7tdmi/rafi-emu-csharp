using System;

namespace Rafi
{
    internal class Csr64
    {
        private readonly ulong[] values = new ulong[0x1000];

        public ulong this[int index]
        {
            get => values[index];
            set => values[index] = value;
        }

        public T Read<T>() where T: CsrDef, new()
        {
            var t = new T();
            t.Value = values[(int)t.CsrAddr];
            return t;
        }

        public void Write<T>(T t) where T : CsrDef
        {
            values[(int)t.CsrAddr] = t.Value;
        }
    }
}
