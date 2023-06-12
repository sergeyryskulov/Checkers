using System.Collections.Generic;

namespace Checkers.Rules.Models;

internal class AllowedVectors
{

    private List<Vector> _vectorList;
    public IEnumerable<Vector> Vectors => _vectorList;

    public bool EatFigure { get; }

    public AllowedVectors()
    {
        _vectorList = new List<Vector>();
        EatFigure = false;
    }

    public AllowedVectors(List<Vector> vectors, bool eatFigure)
    {
        _vectorList = vectors;
        EatFigure = eatFigure;
    }

    public bool AnyVectorExists()
    {
        return _vectorList.Count > 0;
    }       
}