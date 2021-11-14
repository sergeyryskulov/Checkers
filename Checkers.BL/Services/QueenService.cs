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
    public class QueenService
    {

        private VectorHelper _vectorHelper;
        private MathHelper _mathHelper;
        private ColorHelper _colorHelper;

        

        public QueenService(VectorHelper vectorHelper, MathHelper mathHelper, ColorHelper colorHelper)
        {
            _vectorHelper = vectorHelper;
            _mathHelper = mathHelper;
            _colorHelper = colorHelper;
            ;
        }

        public List<Vector> GetAllowedVectors(int coord, string figures)
        {
            int boardWidth = _mathHelper.Sqrt(figures.Length);

            var color = _colorHelper.GetFigureColor(figures[coord]);
            var oppositeColor = color == FigureColor.White ? FigureColor.Black : FigureColor.White;
            var allowedVectors = new List<Vector>();
            var dieVectors = new List<Vector>();

            foreach (var direction in new[]
            {
                Direction.LeftBottom,
                Direction.LeftTop,
                Direction.RightBottom,
                Direction.RightTop
            })
            {
                bool isDie = false;
                for (int i = 1; i < boardWidth; i++)
                {
                    var vector = new Vector()
                    {
                        Length = i,
                        Direction = direction
                    };

                    var stepCoord = _vectorHelper.VectorToCoord(coord, vector, boardWidth);
                    if (stepCoord == -1)
                    {
                        break;
                    }

                    var figure= figures[stepCoord];
                    
                    if (_colorHelper.GetFigureColor(figure) == FigureColor.Empty)
                    {
                        if (isDie)
                        {
                            dieVectors.Add(vector);
                        }
                        else
                        {
                            allowedVectors.Add(vector);
                        }
                        
                    }
                    else if (_colorHelper.GetFigureColor(figure) == oppositeColor)
                    {
                        if (!isDie)
                        {
                            isDie = true;
                            continue;
                            
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                
            }

            if (dieVectors.Count > 0)
            {
                return dieVectors;
            }
            return allowedVectors;

        }
    }
}
