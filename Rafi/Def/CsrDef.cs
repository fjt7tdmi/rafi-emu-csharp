using System;

namespace Rafi
{
    internal abstract class CsrDef : BitField
    {
        public CsrDef(uint value)
            : base(value)
        {
        }

        public abstract CsrAddr CsrAddr { get; }
    }
}
