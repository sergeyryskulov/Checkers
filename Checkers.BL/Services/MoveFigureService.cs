﻿using Checkers.BL.Extensions;
using Checkers.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.BL.Services
{
    public class MoveFigureService : IMoveFigureService
    {        
        private IValidateBoardService _validateBoardService;
        private IDirectMoveService _directMoveService;

        public MoveFigureService(IValidateBoardService validateBoardService, IDirectMoveService directMoveService)
        {            
            _validateBoardService = validateBoardService;
            _directMoveService = directMoveService;
        }

        public BoardState Move(int fromCoord, int toCoord, BoardState boardState)
        {            

            if (!_validateBoardService.CanMove(boardState, fromCoord, toCoord))
            {
                return boardState;
            }

            var newState = _directMoveService.DirectMove(boardState, fromCoord, toCoord);                        
            
            return newState;
        }
    }
}
