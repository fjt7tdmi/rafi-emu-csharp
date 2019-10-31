﻿using System;

namespace Rafi
{
    internal class Core
    {
        public FpReg FpReg { get; } = new FpReg();

        public IntReg IntReg { get; } = new IntReg();

        public uint Pc { get; set; } = 0;

        public uint NextPc { get; set; } = 0;
    }
}