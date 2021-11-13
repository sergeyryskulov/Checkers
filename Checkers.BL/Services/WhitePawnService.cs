using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Helper;
using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public class WhitePawnService
    {
        private VectorHelper _vectorHelper;
        private MathHelper _mathHelper;
        private ColorHelper _colorHelper;


        public WhitePawnService(VectorHelper vectorHelper, MathHelper mathHelper, ColorHelper colorHelper)
        {
            _vectorHelper = vectorHelper;
            _mathHelper = mathHelper;
            _colorHelper = colorHelper;
        }


        public List<Vector> GetAllowedVectors(int coord, string figures)
        {
            int boardWidth= _mathHelper.Sqrt(figures.Length);

            var allowedVectors = new List<Vector>();
            foreach (var vectorOneStepTop in new[]
            {
                new Vector() { Length = 1, Direction = Direction.LeftTop },
                new Vector() { Length = 1, Direction = Direction.RightTop },
            })
            {
                var coordOneStepTop = _vectorHelper.Move(coord, vectorOneStepTop, boardWidth);
                
                if (coordOneStepTop != -1 )
                {
                    if (figures[coordOneStepTop] == Figures.Empty)
                    {
                        allowedVectors.Add(vectorOneStepTop);
                    }
                    else if (_colorHelper.GetFigureColor(figures[coordOneStepTop]) == FigureColor.Black)
                    {
                        var coordTwoStepTop = _vectorHelper.Move(coordOneStepTop, vectorOneStepTop, boardWidth);
                        if (coordTwoStepTop != -1)
                        {
                            if (figures[coordTwoStepTop] == Figures.Empty)
                            {
                                allowedVectors.Add(new Vector()
                                {
                                    Length = 2,
                                    Direction = vectorOneStepTop.Direction
                                });
                            }
                        }
                    }
                    
                }
            }


            return allowedVectors;

        }

     
    }
}
