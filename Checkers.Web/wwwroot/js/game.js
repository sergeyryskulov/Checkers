var $;
var Game = /** @class */ (function () {
    function Game() {
    }
    Game.prototype.initGame = function () {
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
    Game.prototype.showBoard = function () {
        var _this = this;
        this._serverRepository.getFiguresFromServer(function (defaultBoardState) {
            _this._figuresCache = new Array(defaultBoardState.length - 1);
            var lineSquareCount = Math.sqrt(_this._figuresCache.length);
            _this._gameDrawer.drawSquares(lineSquareCount);
            _this._gameDrawer.setDropFigureOnSquareHandler(function (fromCoord, toCoord) {
                _this.moveFigureOnBoard(fromCoord, toCoord);
                _this._serverRepository.moveFigureOnServer(_this._oldBoardState, fromCoord, toCoord, function (newBoardState) { return _this.showFiguresOnBoard(newBoardState); });
            });
            _this.showFiguresOnBoard(defaultBoardState);
        });
    };
    Game.prototype.moveFigureOnBoard = function (fromCoord, toCoord) {
        var figure = this._figuresCache[fromCoord];
        this.showFigureAt(fromCoord, '1');
        this.showFigureAt(toCoord, figure);
    };
    Game.prototype.getFiguresLength = function (boardState) {
        var figuresLength = boardState.length - 1;
        if (boardState[boardState.length - 1] !== 'w' &&
            boardState[boardState.length - 1] !== 'W' &&
            boardState[boardState.length - 1] !== 'b' &&
            boardState[boardState.length - 1] !== 'B') {
            figuresLength = Math.max(boardState.indexOf('w'), boardState.indexOf('W'), boardState.indexOf('b'), boardState.indexOf('B'));
        }
        return figuresLength;
    };
    Game.prototype.showFiguresOnBoard = function (boardState) {
        var _this = this;
        this._oldBoardState = boardState;
        console.log(boardState);
        var figuresLength = this.getFiguresLength(boardState);
        for (var coord = 0; coord < figuresLength; coord++) {
            this.showFigureAt(coord, boardState[coord]);
        }
        if (boardState[figuresLength] === 'b') {
            this._serverRepository.intellectStep(this._oldBoardState, function (newsBoardState) { return _this.intellectStepCalculated(newsBoardState); });
        }
    };
    Game.prototype.intellectStepCalculated = function (newBoardState) {
        var _this = this;
        var fromFigureCoord = -1;
        var toFigureCoord = -1;
        for (var coord = 0; coord < this.getFiguresLength(newBoardState); coord++) {
            if (newBoardState[coord] == '1' &&
                (this._figuresCache[coord] == 'p' || this._figuresCache[coord] == 'q')) {
                fromFigureCoord = coord;
            }
            if ((newBoardState[coord] == 'p' || newBoardState[coord] == 'q') && (this._figuresCache[coord] == '1')) {
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
    Game.prototype.showFigureAt = function (coord, figure) {
        if (this._figuresCache[coord] === figure) {
            return;
        }
        this._figuresCache[coord] = figure;
        this._gameDrawer.drawFigure(coord, figure);
    };
    return Game;
}());
//# sourceMappingURL=game.js.map