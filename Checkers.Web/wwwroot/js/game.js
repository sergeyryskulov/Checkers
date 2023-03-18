var $;
var Game = /** @class */ (function () {
    function Game() {
    }
    Game.prototype.initGame = function () {
        var _this = this;
        this._serverRepository = new ServerRepository();
        this._gameDrawer = new GameDrawer();
        var defaultBoardState = new BoardState();
        defaultBoardState.cells = "1p1p1p1pp1p1p1p11p1p1p1p1111111111111111P1P1P1P11P1P1P1PP1P1P1P1";
        defaultBoardState.turn = 'w';
        defaultBoardState.mustGoFrom = null;
        if (window.location.href.indexOf('cells=') !== -1) {
            defaultBoardState.cells = window.location.href.split('cells=')[1].split('&')[0];
        }
        if (window.location.href.indexOf('turn=') !== -1) {
            defaultBoardState.turn = window.location.href.split('turn=')[1].split('&')[0];
        }
        this._gameDrawer.setNewGameClickHandler(function () { return _this.showFiguresOnBoard(defaultBoardState); });
        this.initBoard(defaultBoardState);
    };
    Game.prototype.initBoard = function (defaultBoardState) {
        var _this = this;
        this._figuresCache = new Array(defaultBoardState.cells.length);
        var lineSquareCount = Math.sqrt(this._figuresCache.length);
        this._gameDrawer.drawSquares(lineSquareCount);
        this._gameDrawer.setDropFigureOnSquareHandler(function (fromCoord, toCoord) {
            _this.moveFigureOnBoard(fromCoord, toCoord);
            _this._serverRepository.moveFigureOnServer(_this._oldBoardState, fromCoord, toCoord, function (newBoardState) { return _this.showFiguresOnBoard(newBoardState); });
        });
        this.showFiguresOnBoard(defaultBoardState);
    };
    Game.prototype.moveFigureOnBoard = function (fromCoord, toCoord) {
        var figure = this._figuresCache[fromCoord];
        this.showFigureAt(fromCoord, '1');
        this.showFigureAt(toCoord, figure);
    };
    Game.prototype.showFiguresOnBoard = function (boardState) {
        var _this = this;
        this._oldBoardState = boardState;
        console.log(boardState);
        for (var coord = 0; coord < boardState.cells.length; coord++) {
            this.showFigureAt(coord, boardState.cells[coord]);
        }
        if (boardState.turn === 'b') {
            this._serverRepository.intellectStep(this._oldBoardState, function (newsBoardState) { return _this.intellectStepCalculated(newsBoardState); });
        }
    };
    Game.prototype.intellectStepCalculated = function (newBoardState) {
        var _this = this;
        var fromFigureCoord = -1;
        var toFigureCoord = -1;
        for (var coord = 0; coord < newBoardState.cells.length; coord++) {
            if (newBoardState.cells[coord] == '1' &&
                (this._figuresCache[coord] == 'p' || this._figuresCache[coord] == 'q')) {
                fromFigureCoord = coord;
            }
            if ((newBoardState.cells[coord] == 'p' || newBoardState.cells[coord] == 'q') && (this._figuresCache[coord] == '1')) {
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