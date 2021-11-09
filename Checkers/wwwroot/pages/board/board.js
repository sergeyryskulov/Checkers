var $;
var Board = /** @class */ (function () {
    function Board() {
        this.isFlipped = false;
    }
    Board.prototype.initBoard = function () {
        var _this = this;
        this.boardRepository = new BoardRepository();
        this.boardDrawer = new BoardDrawer();
        this.boardRepository.registerOnServer(function () {
            _this.boardDrawer.setFlipClickHandler(function () { return _this.flipBoard(); });
            _this.boardDrawer.setNewGameClickHandler(function () { return _this.boardRepository.clearGameOnServer(function (clearedFigures) { return _this.showFiguresOnBoard(clearedFigures); }); });
            _this.showBoard();
        });
    };
    Board.prototype.showBoard = function () {
        var _this = this;
        this.figuresCache = new Array(64);
        this.boardDrawer.drawSquares(this.isFlipped);
        this.boardDrawer.setDropFigureOnSquareHandler(function (fromCoord, toCoord) {
            _this.moveFigureOnBoard(fromCoord, toCoord);
            _this.boardRepository.moveFigureOnServer(fromCoord, toCoord, function (data) { return _this.showFiguresOnBoard(data); });
        });
        this.boardRepository.getFiguresFromServer(function (data) { return _this.showFiguresOnBoard(data); });
    };
    Board.prototype.flipBoard = function () {
        this.isFlipped = !this.isFlipped;
        this.showBoard();
    };
    Board.prototype.moveFigureOnBoard = function (fromCoord, toCoord) {
        var figure = this.figuresCache[fromCoord];
        this.showFigureAt(fromCoord, '1');
        this.showFigureAt(toCoord, figure);
    };
    Board.prototype.showFiguresOnBoard = function (figures) {
        for (var coord = 0; coord < 64; coord++) {
            this.showFigureAt(coord, figures[coord]);
        }
    };
    Board.prototype.showFigureAt = function (coord, figure) {
        if (this.figuresCache[coord] === figure) {
            return;
        }
        this.figuresCache[coord] = figure;
        this.boardDrawer.drawFigure(coord, figure);
    };
    return Board;
}());
//# sourceMappingURL=board.js.map