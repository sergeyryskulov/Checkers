class BoardDrawer {


    _squareDrawer: SquareDrawer;
    _figureDrawer : FigureDrawer;

    constructor() {
        this._squareDrawer = new SquareDrawer();
        this._figureDrawer = new FigureDrawer();
    }

    public setNewGameClickHandler(callback) {
        $('.button__new').click(callback);
    }

    public drawSquares(width) {
        let boardWidth = Math.min(document.documentElement.clientWidth, document.documentElement.clientHeight) - 100;

        $('.board__inner').width(boardWidth);
        $('.board__inner').height(boardWidth);

        let squaresHtml= this._squareDrawer.getSquaresHtml(width);

        $('.board__inner').html(squaresHtml);

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