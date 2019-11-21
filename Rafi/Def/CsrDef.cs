using System;

namespace Rafi
{
    internal abstract class CsrDef : BitField64
    {
        public CsrDef(ulong value)
            : base(value)
        {
        }

        public abstract CsrAddr CsrAddr { get; }
    }
}
