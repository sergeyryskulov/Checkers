using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.BL.Models
{
    public class AllowedVectors
    {

        private List<Vector> _vectors;

        public IEnumerable<Vector> Vectors
        {
            get { return _vectors; }
        }

        public bool EatFigure { get; }

        public AllowedVectors()
        {
            _vectors = new List<Vector>();
            EatFigure = false;
        }

        public AllowedVectors(List<Vector> vectors, bool eatFigure)
        {
            _vectors = vectors;
            EatFigure = eatFigure;
        }

        public bool AnyVectorExists()
        {
            return _vectors.Count > 0;
        }

        public bool Contains(Vector vector)
        {
            return _vectors.Contains(vector);
        }
    }
}
