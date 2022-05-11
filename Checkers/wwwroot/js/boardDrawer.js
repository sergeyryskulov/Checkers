var BoardDrawer = /** @class */ (function () {
    function BoardDrawer() {
    }
    BoardDrawer.prototype.setNewGameClickHandler = function (callback) {
        $('.button__new').click(callback);
    };
    BoardDrawer.prototype.drawSquares = function (width) {
        var boardWidth = Math.min(document.documentElement.clientWidth, document.documentElement.clientHeight) - 100;
        $('.board__inner').width(boardWidth);
        $('.board__inner').height(boardWidth);
        $('.board__inner').html(new Square().getSquaresHtml(width));
    };
    BoardDrawer.prototype.drawMoving = function (fromCoord, toCoord, onComplete) {
        new Figure().drawMoving(fromCoord, toCoord, onComplete);
    };
    BoardDrawer.prototype.drawFigure = function (coord, figure) {
        $('#s' + coord).html(new Figure().getHtml(coord, figure));
        new Figure().setHandlers(coord, figure);
    };
    BoardDrawer.prototype.setDropFigureOnSquareHandler = function (dropCallback) {
        new Square().setDropFigureOnSquareHandler(dropCallback);
    };
    return BoardDrawer;
}());
//# sourceMappingURL=boardDrawer.js.map