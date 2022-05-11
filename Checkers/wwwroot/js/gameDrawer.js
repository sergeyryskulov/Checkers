var GameDrawer = /** @class */ (function () {
    function GameDrawer() {
        this._squareDrawer = new SquareDrawer();
        this._figureDrawer = new FigureDrawer();
        this._boardDrawer = new BoardDrawer();
    }
    GameDrawer.prototype.setNewGameClickHandler = function (callback) {
        $('.button__new').click(callback);
    };
    GameDrawer.prototype.drawSquares = function (width) {
        var squaresHtml = this._squareDrawer.getSquaresHtml(width);
        this._boardDrawer.drawBoard(squaresHtml);
    };
    GameDrawer.prototype.drawMoving = function (fromCoord, toCoord, onComplete) {
        this._figureDrawer.drawMoving(fromCoord, toCoord, onComplete);
    };
    GameDrawer.prototype.drawFigure = function (coord, figure) {
        var figuresHtml = this._figureDrawer.getHtml(coord, figure);
        $('#s' + coord).html(figuresHtml);
        this._figureDrawer.setHandlers(coord, figure);
    };
    GameDrawer.prototype.setDropFigureOnSquareHandler = function (dropCallback) {
        this._squareDrawer.setDropFigureOnSquareHandler(dropCallback);
    };
    return GameDrawer;
}());
//# sourceMappingURL=gameDrawer.js.map