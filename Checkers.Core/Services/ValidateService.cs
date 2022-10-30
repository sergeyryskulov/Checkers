using System;
using System.Collections.Generic;
using System.Text;
using Checkers.BL.Constants;
using Checkers.BL.Extensions;
using Checkers.BL.Interfaces;
using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public class ValidateService : IValidateFigureService, IValidateEatService
    {
        private IValidatePawnService _validatePawnService;
        private IValidateQueenService _validateQueenService;

        public ValidateService(IValidatePawnService validatePawnService, IValidateQueenService validateQueenService)
        {
            _validatePawnService = validatePawnService;
            _validateQueenService = validateQueenService;
        }

        public bool CanEatFigure(int coord, string figures)
        {
            return GetAllowedMoveVectors(coord, figures).EatFigure;
        }
        
        public AllowedVectors GetAllowedMoveVectors(int coord, string figures)
        {
            var figure = figures[coord];

            var result = new AllowedVectors();

            if (figure == Figures.WhitePawn || figure == Figures.BlackPawn)
            {
                result = _validatePawnService.GetAllowedMoveVectors(coord, figures);
            }
            else if (figure == Figures.WhiteQueen || figure == Figures.BlackQueen)
            {
                result = _validateQueenService.GetAllowedMoveVectors(coord, figures);
            }

            return result;
        }
    }
}
