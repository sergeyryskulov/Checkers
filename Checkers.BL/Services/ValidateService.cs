using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Extensions;
using Checkers.BL.Interfaces;
using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public class ValidateService
    {        
        private IValidatePawnService _validatePawnService;
        private IValidateQueenService _validateQueenService;


        public ValidateService(IValidatePawnService validatePawnService, IValidateQueenService validateQueenService)
        {            
            _validatePawnService = validatePawnService;
            _validateQueenService = validateQueenService;
        }

        public AllowedVectors GetAllowedMoveVectors(int coord, string figures, bool ignoreBlock = false)
        {
            var figure = figures[coord];

            var result = new AllowedVectors()
            {
                Vectors = new List<Vector>(),
                EatFigure = false
            };

            if (figure == Figures.WhitePawn || figure == Figures.BlackPawn)
            {
                result = _validatePawnService.GetAllowedMoveVectors(coord, figures);
            }
            else if (figure == Figures.WhiteQueen || figure == Figures.BlackQueen)
            {
                result = _validateQueenService.GetAllowedMoveVectors(coord, figures);
            }

            if (!ignoreBlock && result.EatFigure==false && result.Vectors.Count > 0 && IsBlocked(coord, figures))
            {
                return new AllowedVectors()
                {
                    Vectors = new List<Vector>(),
                    EatFigure = false
                };
            }

            return result;
        }

        private bool IsBlocked(int coord, string figures)
        {
            var color = figures[coord].GetFigureColor();

            for (int figureCoord = 0; figureCoord < figures.Length; figureCoord++)
            {
                var iteratedFigure = figures[figureCoord];

                if (
                    coord != iteratedFigure &&
                    iteratedFigure.GetFigureColor() == color)
                {
                    if (GetAllowedMoveVectors(figureCoord, figures, true).EatFigure)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
