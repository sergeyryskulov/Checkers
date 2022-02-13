using System;
using System.Collections.Generic;
using System.Text;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services
{
    public class MoveAndSaveFigureService : IMoveAndSaveFigureService
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMoveFigureService _moveFigureService;

        public MoveAndSaveFigureService(IBoardRepository boardRepository, IMoveFigureService moveFigureService)
        {
            _boardRepository = boardRepository;
            _moveFigureService = moveFigureService;
        }

        public string MoveAndSaveFigure(int fromCoord, int toCoord, string registrationId)
        {
            var oldState = _boardRepository.Load(registrationId);
            var newState = _moveFigureService.Move(oldState, fromCoord, toCoord);
            if (oldState != newState)
            {
                _boardRepository.Save(registrationId, newState);
            }

            return newState;
        }
    }
}
