var $;
var Board = /** @class */ (function () {
    function Board() {
    }
    Board.prototype.initBoard = function () {
        var _this = this;
        this._serverRepository = new ServerRepository();
        this._gameDrawer = new GameDrawer();
        var position = '';
        if (window.location.href.split('?pos=').length === 2) {
            position = window.location.href.split('?pos=')[1];
        }
        this._serverRepository.registerOnServer(position, function () {
            _this._gameDrawer.setNewGameClickHandler(function () { return _this._serverRepository.clearGameOnServer(function (clearedFigures) { return _this.showFiguresOnBoard(clearedFigures); }); });
            _this.showBoard();
        });
    };
    Board.prototype.showBoard = function () {
        var _this = this;
        this._serverRepository.getFiguresFromServer(function (data) {
            _this.figuresCache = new Array(data.length - 1);
            var lineSquareCount = Math.sqrt(_this.figuresCache.length);
            _this._gameDrawer.drawSquares(lineSquareCount);
            _this._gameDrawer.setDropFigureOnSquareHandler(function (fromCoord, toCoord) {
                _this.moveFigureOnBoard(fromCoord, toCoord);
                _this._serverRepository.moveFigureOnServer(fromCoord, toCoord, function (data) { return _this.showFiguresOnBoard(data); });
            });
            _this.showFiguresOnBoard(data);
        });
    };
    Board.prototype.moveFigureOnBoard = function (fromCoord, toCoord) {
        var figure = this.figuresCache[fromCoord];
        this.showFigureAt(fromCoord, '1');
        this.showFigureAt(toCoord, figure);
    };
    Board.prototype.getFiguresLength = function (boardState) {
        var figuresLength = boardState.length - 1;
        if (boardState[boardState.length - 1] !== 'w' &&
            boardState[boardState.length - 1] !== 'W' &&
            boardState[boardState.length - 1] !== 'b' &&
            boardState[boardState.length - 1] !== 'B') {
            figuresLength = Math.max(boardState.indexOf('w'), boardState.indexOf('W'), boardState.indexOf('b'), boardState.indexOf('B'));
        }
        return figuresLength;
    };
    Board.prototype.showFiguresOnBoard = function (boardState) {
        var _this = this;
        console.log(boardState);
        var figuresLength = this.getFiguresLength(boardState);
        for (var coord = 0; coord < figuresLength; coord++) {
            this.showFigureAt(coord, boardState[coord]);
        }
        if (boardState[figuresLength] === 'b') {
            this._serverRepository.intellectStep(function (newsBoardState) { return _this.intellectStepCalculated(newsBoardState); });
        }
    };
    Board.prototype.intellectStepCalculated = function (newBoardState) {
        var _this = this;
        var fromFigureCoord = -1;
        var toFigureCoord = -1;
        for (var coord = 0; coord < this.getFiguresLength(newBoardState); coord++) {
            if (newBoardState[coord] == '1' &&
                (this.figuresCache[coord] == 'p' || this.figuresCache[coord] == 'q')) {
                fromFigureCoord = coord;
            }
            if ((newBoardState[coord] == 'p' || newBoardState[coord] == 'q') && (this.figuresCache[coord] == '1')) {
                toFigureCoord = coord;
            }
        }
        if (fromFigureCoord !== -1 && toFigureCoord !== -1) {
            this._gameDrawer.drawMoving(fromFigureCoord, toFigureCoord, function () { return _this.showFiguresOnBoard(newBoardState); });
        }
        else {
            this.showFiguresOnBoard(newBoardState);
        }
    };
    Board.prototype.showFigureAt = function (coord, figure) {
        if (this.figuresCache[coord] === figure) {
            return;
        }
        this.figuresCache[coord] = figure;
        this._gameDrawer.drawFigure(coord, figure);
    };
    return Board;
}());
//# sourceMappingURL=board.js.map