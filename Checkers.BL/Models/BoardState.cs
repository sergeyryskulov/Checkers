﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.BL.Models
{
    public struct BoardState
    {        
        public string Cells { get; set; }

        public char Turn { get; set; }

        public int? MustGoFrom { get; set; }
    }
}
