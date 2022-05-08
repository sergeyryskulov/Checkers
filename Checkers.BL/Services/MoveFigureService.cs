using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants;
using Checkers.BL.Extensions;
using Checkers.BL.Models;
using Ckeckers.DAL.Repositories;

namespace Checkers.BL.Services
{
    public class MoveFigureService : IMoveFigureService
    {        
        
        private DirectMoveService _directMoveService;

        private ValidateBoardService _validateBoardService;

        public MoveFigureService(DirectMoveService directMoveService, ValidateBoardService validateBoardService)
        {
        
            _directMoveService = directMoveService;
            _validateBoardService = validateBoardService;
        }


        public string Move(string boardStateString, int fromCoord, int toCoord)
        {

            if (!_validateBoardService.CanMove(boardStateString, fromCoord, toCoord))
            {
                return boardStateString;
            }

            return _directMoveService.DirectMove(boardStateString, fromCoord, toCoord);
        }
    }
}
