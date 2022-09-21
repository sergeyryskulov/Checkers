using System;
using System.Collections.Generic;
using System.Text;
using Ckeckers.DAL.Repositories;

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

        public string Move(int fromCoord, int toCoord, string boardState)
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
