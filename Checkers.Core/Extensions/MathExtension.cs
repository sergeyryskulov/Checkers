using System;

namespace Checkers.Core.Extensions
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
