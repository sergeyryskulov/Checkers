using Checkers.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkers.Web.Models
{
    public class MoveFigureApiModel
    {

        BoardState BoardState { get; set; }

        public int FromCoord { get; set; }

        public int ToCoord { get; set; }
    }
}
