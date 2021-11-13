using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.BL.Constants.Enums;
using Checkers.BL.Helper;
using Checkers.BL.Models;

namespace Checkers.BL.Services
{
    public class WhitePawnService
    {
        private VectorHelper _vectorHelper;
        private MathHelper _mathHelper;

        public WhitePawnService(VectorHelper vectorHelper, MathHelper mathHelper)
        {
            _vectorHelper = vectorHelper;
            _mathHelper = mathHelper;
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
                    if (figures[coordOneStepTop] == '1')
                    {
                        allowedVectors.Add(vectorOneStepTop);
                    }
                    else if (GetFigureColor(figures[coordOneStepTop]) == "black")
                    {
                        var coordTwoStepTop = _vectorHelper.Move(coordOneStepTop, vectorOneStepTop, boardWidth);
                        if (coordTwoStepTop != -1)
                        {
                            if (figures[coordTwoStepTop] == '1')
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

        private string GetFigureColor(char figure)
        {
            if ("rnbqkp".Contains(figure))
            {
                return "white";
            }
            if ("RNBQKP".Contains(figure))
            {
                return "black";
            }

            return "empty";
        }
    }
}
