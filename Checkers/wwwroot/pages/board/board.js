var $;
var Board = /** @class */ (function () {
    function Board() {
        this.isFlipped = false;
    }
    Board.prototype.initBoard = function () {
        var _this = this;
        this.serverApi = new ServerApi();
        this.boardDrawer = new BoardDrawer();
        var position = '';
        if (window.location.href.split('?pos=').length === 2) {
            position = window.location.href.split('?pos=')[1];
        }
        this.serverApi.registerOnServer(position, function () {
            _this.boardDrawer.setFlipClickHandler(function () { return _this.flipBoard(); });
            _this.boardDrawer.setNewGameClickHandler(function () { return _this.serverApi.clearGameOnServer(function (clearedFigures) { return _this.showFiguresOnBoard(clearedFigures); }); });
            _this.showBoard();
        });
    };
    Board.prototype.showBoard = function () {
        var _this = this;
        this.serverApi.getFiguresFromServer(function (data) {
            _this.figuresCache = new Array(data.length - 1);
            var lineSquareCount = Math.sqrt(_this.figuresCache.length);
            ;
            var boardWidth = Math.min(document.documentElement.clientWidth, document.documentElement.clientHeight) - 60;
            $('.board').width(boardWidth);
            $('.board').height(boardWidth);
            _this.boardDrawer.drawSquares(_this.isFlipped, lineSquareCount);
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
        var _this = this;
        console.log(boardState);
        var figuresLength = boardState.length - 1;
        if (boardState[boardState.length - 1] !== 'w' &&
            boardState[boardState.length - 1] !== 'W' &&
            boardState[boardState.length - 1] !== 'b' &&
            boardState[boardState.length - 1] !== 'B') {
            figuresLength = Math.max(boardState.indexOf('w'), boardState.indexOf('W'), boardState.indexOf('b'), boardState.indexOf('B'));
        }
        for (var coord = 0; coord < figuresLength; coord++) {
            this.showFigureAt(coord, boardState[coord]);
        }
        $('.state').text(boardState);
        if (boardState[figuresLength] === 'w') {
            $('.turn').text('Ход белых');
        }
        if (boardState[figuresLength] === 'W') {
            $('.turn').text('Белые выиграли!');
        }
        else if (boardState[figuresLength] === 'b') {
            $('.turn').text('Ход черных');
            var timeOut = 0;
            if (boardState[boardState.length - 1] !== 'b') {
                timeOut = 500;
            }
            this.serverApi.intellectStep(function (newsBoardState) { return setTimeout(function () { return _this.showFiguresOnBoard(newsBoardState); }, timeOut); });
        }
        else if (boardState[figuresLength] === 'B') {
            $('.turn').text('Черные выиграли!');
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