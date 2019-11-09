using System;

namespace Rafi
{
    internal class Csr
    {
        private readonly uint[] values = new uint[0x1000];

        public uint this[int index]
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
