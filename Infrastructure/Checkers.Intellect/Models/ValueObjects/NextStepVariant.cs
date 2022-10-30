using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.BL.Models
{
    public class NextStepVariant
    {
        public NextStepVariant(BoardState resultState, BoardState firstStepOfResultState)
        {
            ResultState = resultState;
            FirstStepOfResultState = firstStepOfResultState;
        }

        public BoardState ResultState { get; }

        public BoardState FirstStepOfResultState { get; }
    }
}
