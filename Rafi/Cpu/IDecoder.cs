using System;

namespace Rafi
{
    interface IDecoder
    {
        Op Decode(uint insn);
    }
}
