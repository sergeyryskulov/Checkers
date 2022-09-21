using System;
using System.Collections.Generic;
using System.Text;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services
{
    public class MoveFigureService : IMoveFigureService
    {
        private IBoardRepository _boardRepository;
        private IValidateBoardService _validateBoardService;
        private IDirectMoveService _directMoveService;

        public MoveFigureService(IBoardRepository boardRepository,  IValidateBoardService validateBoardService, IDirectMoveService directMoveService)
        {
            _boardRepository = boardRepository;
            _validateBoardService = validateBoardService;
            _directMoveService = directMoveService;
        }

        public string Move(int fromCoord, int toCoord, string registrationId, string boardState)
        {
            var oldState = boardState;

            if (!_validateBoardService.CanMove(oldState, fromCoord, toCoord))
            {
                return oldState;
            }

            var newState = _directMoveService.DirectMove(oldState, fromCoord, toCoord);
            
            _boardRepository.Save(registrationId, newState);
            
            return newState;
        }
    }
}
