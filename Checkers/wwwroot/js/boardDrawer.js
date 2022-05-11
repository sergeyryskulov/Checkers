var BoardDrawer = /** @class */ (function () {
    function BoardDrawer() {
        this._squareDrawer = new SquareDrawer();
        this._figureDrawer = new FigureDrawer();
    }
    BoardDrawer.prototype.setNewGameClickHandler = function (callback) {
        $('.button__new').click(callback);
    };
    BoardDrawer.prototype.drawSquares = function (width) {
        var boardWidth = Math.min(document.documentElement.clientWidth, document.documentElement.clientHeight) - 100;
        $('.board__inner').width(boardWidth);
        $('.board__inner').height(boardWidth);
        var squaresHtml = this._squareDrawer.getSquaresHtml(width);
        $('.board__inner').html(squaresHtml);
    };
    BoardDrawer.prototype.drawMoving = function (fromCoord, toCoord, onComplete) {
        this._figureDrawer.drawMoving(fromCoord, toCoord, onComplete);
    };
    BoardDrawer.prototype.drawFigure = function (coord, figure) {
        var figuresHtml = this._figureDrawer.getHtml(coord, figure);
        $('#s' + coord).html(figuresHtml);
        this._figureDrawer.setHandlers(coord, figure);
    };
    BoardDrawer.prototype.setDropFigureOnSquareHandler = function (dropCallback) {
        this._squareDrawer.setDropFigureOnSquareHandler(dropCallback);
    };
    return BoardDrawer;
}());
//# sourceMappingURL=boardDrawer.js.map