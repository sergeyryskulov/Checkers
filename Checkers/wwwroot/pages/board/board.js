var $;
var Board = /** @class */ (function () {
    function Board() {
        this.isFlipped = false;
    }
    Board.prototype.initBoard = function () {
        var _this = this;
        this.serverApi = new ServerApi();
        this.boardDrawer = new BoardDrawer();
        this.serverApi.registerOnServer(function () {
            _this.boardDrawer.setFlipClickHandler(function () { return _this.flipBoard(); });
            _this.boardDrawer.setNewGameClickHandler(function () { return _this.serverApi.clearGameOnServer(function (clearedFigures) { return _this.showFiguresOnBoard(clearedFigures); }); });
            _this.showBoard();
        });
    };
    Board.prototype.showBoard = function () {
        var _this = this;
        this.serverApi.getFiguresFromServer(function (data) {
            _this.figuresCache = new Array(data.length - 1);
            var width = Math.sqrt(_this.figuresCache.length);
            $('.board').width(width * 80);
            $('.board').height(width * 80);
            _this.boardDrawer.drawSquares(_this.isFlipped, width);
            _this.boardDrawer.setDropFigureOnSquareHandler(function (fromCoord, toCoord) {
                _this.moveFigureOnBoard(fromCoord, toCoord);
                _this.serverApi.moveFigureOnServer(fromCoord, toCoord, function (data) { return _this.showFiguresOnBoard(data); });
            });
            _this.showFiguresOnBoard(data);
        });
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
    Board.prototype.showFiguresOnBoard = function (boardState) {
        var figuresLength = boardState.length - 1;
        if (boardState[boardState.length - 1] !== 'w' &&
            boardState[boardState.length - 1] !== 'b') {
            figuresLength = Math.max(boardState.indexOf('w'), boardState.indexOf('b'));
        }
        for (var coord = 0; coord < figuresLength; coord++) {
            this.showFigureAt(coord, boardState[coord]);
        }
        if (boardState[figuresLength] == 'w') {
            $('.turn').text('Ход белых');
        }
        else {
            $('.turn').text('Ход черных');
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