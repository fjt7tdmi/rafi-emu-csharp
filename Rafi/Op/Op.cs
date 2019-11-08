using System;

namespace Rafi
{
    internal class Op
    {
        public virtual void Execute(Core core)
        {
        }

        public virtual Trap PostCheckTrap(Core core)
        {
            return null;
        }
    }
}
