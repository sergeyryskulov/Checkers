using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.BL.Extensions
{
    public static class MathExtension
    {
        public static int SquareRoot(this int value)
        {
            if (value == 64)
            {
                return 8;
            }
            return (int)Math.Sqrt(value);
        }
    }
}
