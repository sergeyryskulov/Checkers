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
        public Direction Base;

        public int Length;

       
        public override bool Equals(object obj)
        {
            if (obj==null || !(obj is Vector))
                return false;

            var other = (Vector)obj;

            return Base == other.Base && Length == other.Length;
        }

        public override int GetHashCode()
        {
            return 10*Length  +  (int) Base;
        }

    }
}
