using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants.Enums;

namespace Checkers.BL.Models
{
    public class Vector
    {
        public Direction Direction { get;  }

        public int Length { get;  }


        public Vector(Direction direction, int length)
        {
            Direction = direction;
            Length = length;
        }

        public override bool Equals(object obj)
        {
            if (obj==null || !(obj is Vector))
                return false;

            var other = (Vector)obj;

            return Direction == other.Direction && Length == other.Length;
        }

        public override string ToString()
        {
            return "" + Direction + " (" + Length + ")";
        }

        public override int GetHashCode()
        {
            return 10*Length  +  (int) Direction;
        }

    }
}
