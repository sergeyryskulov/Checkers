class GameDrawer {


    _squareDrawer: SquareDrawer;
    _figureDrawer: FigureDrawer;
    _boardDrawer: BoardDrawer;

    constructor() {
        this._squareDrawer = new SquareDrawer();
        this._figureDrawer = new FigureDrawer();
        this._boardDrawer = new BoardDrawer();
    }

    public setNewGameClickHandler(callback) {
        $('.button__new').click(callback);
    }

    public drawSquares(width) {
        let squaresHtml = this._squareDrawer.getSquaresHtml(width);

        this._boardDrawer.drawBoard(squaresHtml);
    }

    public drawMoving(fromCoord, toCoord, onComplete) {
        this._figureDrawer.drawMoving(fromCoord, toCoord, onComplete);
    }

    public drawFigure(coord, figure) {
        let figuresHtml = this._figureDrawer.getHtml(coord, figure);
        $('#s' + coord).html(figuresHtml);
        this._figureDrawer.setHandlers(coord, figure);
    }

    public setDropFigureOnSquareHandler(dropCallback) {
        this._squareDrawer.setDropFigureOnSquareHandler(dropCallback);
    }
}