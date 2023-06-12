using System;
using System.Text;
using Checkers.DomainModels.Enums;

namespace Checkers.DomainModels.Models;

public class Board
{
    private string _cells;

    private enum Figure
    {
        WhitePawn = 'P',
        BlackPawn = 'p',
        WhiteQueen = 'Q',
        BlackQueen = 'q',
        Empty = '1'
    }

    public Board(string cells)
    {
        _cells = cells;
    }

    public int CellsCount
    {
        get
        {
            return _cells.Length;
        }
    }

    public int BoardWidth
    {
        get
        {
            return SquareRoot(_cells.Length);
        }
    }

    public void MoveFigure(int fromPosition, int toPosition, bool convertToQueen)
    {            
        Figure resultFigure = (Figure)_cells[fromPosition];
        if (convertToQueen)
        {
            if (resultFigure == Figure.WhitePawn)
            {
                resultFigure = Figure.WhiteQueen;
            }
            if (resultFigure == Figure.BlackPawn)
            {
                resultFigure = Figure.BlackQueen;
            }
        }

        StringBuilder stringBuilder = new StringBuilder(_cells);
        stringBuilder[fromPosition] = (char) Figure.Empty;
        stringBuilder[toPosition] = (char)resultFigure;
        _cells = stringBuilder.ToString();
    }

    public void DeleteFigure(int position)
    {           
        StringBuilder stringBuilder = new StringBuilder(_cells);
        stringBuilder[position] = (char)Figure.Empty;            
        _cells = stringBuilder.ToString();
    }

    public bool ContainsAnyWhiteFigure()
    {
        return _cells.Contains((char)Figure.WhitePawn) || _cells.Contains((char)Figure.WhiteQueen);
    }

    public bool ContainsAnyBlackFigure()
    {
        return _cells.Contains((char)Figure.BlackPawn) || _cells.Contains((char)Figure.BlackQueen);
    }

    public FigureColor FigureColorAt(int position)
    {
        var figure = GetFigureAt(position);
        if (figure == Figure.WhitePawn || figure == Figure.WhiteQueen)
        {
            return FigureColor.White;
        }
        else if (figure == Figure.BlackPawn || figure == Figure.BlackQueen)
        {
            return FigureColor.Black;
        }

        return FigureColor.Empty;
    }

    public bool IsPawnAt(int position)
    {
        var figure = GetFigureAt(position);
        return figure == Figure.WhitePawn || figure == Figure.BlackPawn;
    }

    public bool IsQueenAt(int position)
    {
        var figure = GetFigureAt(position);
        return figure == Figure.WhiteQueen || figure == Figure.BlackQueen;
    }

    public  bool IsWhiteFigureAt(int position)
    {
        var figure = GetFigureAt(position); 
        return figure == Figure.WhitePawn || figure == Figure.WhiteQueen;
    }

    public bool IsBlackFigureAt(int position)
    {
        var figure = GetFigureAt(position);
        return figure == Figure.BlackPawn || figure == Figure.BlackQueen;
    }

    public bool IsEmptyCellAt(int position)
    {
        return GetFigureAt(position) == Figure.Empty;
    }

    public override string ToString()
    {
        return _cells.ToString();
    }

    private Figure GetFigureAt(int position)
    { 
        return (Figure) _cells[position];
    }

    private int SquareRoot(int value)
    {
        if (value == 64)
        {
            return 8;
        }
        return (int)Math.Sqrt(value);
    }
}