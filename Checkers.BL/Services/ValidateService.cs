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
        private readonly IValidateFigureService _validateFigureService;

        public ValidateService(IValidateFigureService validateFigureService)
        {
            _validateFigureService = validateFigureService;
        }

        public AllowedVectors GetAllowedMoveVectors(int coord, string figures)
        {
            
            var result = _validateFigureService.GetAllowedMoveVectors(coord, figures);

            if (result.EatFigure==false && result.Vectors.Count > 0 && IsBlocked(coord, figures))
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
            var color = figures[coord].ToFigureColor();

            for (int figureCoord = 0; figureCoord < figures.Length; figureCoord++)
            {
                var iteratedFigure = figures[figureCoord];

                if (
                    coord != iteratedFigure &&
                    iteratedFigure.ToFigureColor() == color)
                {
                    if (_validateFigureService.GetAllowedMoveVectors(figureCoord, figures).EatFigure)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
