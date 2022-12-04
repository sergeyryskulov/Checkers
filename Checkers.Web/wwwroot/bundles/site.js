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
            defaultBoardState.cells = window.location.href.split('cells=')[1];
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
var BoardDrawer = /** @class */ (function () {
    function BoardDrawer() {
    }
    BoardDrawer.prototype.drawBoard = function (squaresHtml) {
        var boardWidth = Math.min(document.documentElement.clientWidth, document.documentElement.clientHeight) - 100;
        $('.board__inner').width(boardWidth);
        $('.board__inner').height(boardWidth);
        $('.board__inner').html(squaresHtml);
    };
    return BoardDrawer;
}());
//# sourceMappingURL=boardDrawer.js.map
var SquareDrawer = /** @class */ (function () {
    function SquareDrawer() {
    }
    SquareDrawer.prototype.getSquaresHtml = function (width) {
        var result = '';
        for (var coord = 0; coord < width * width; coord++) {
            var currentSquareColor = this.isBlackSquareAt(coord, width) ? 'black' : 'white';
            var currentSquareHtml = "<div id=s" + coord + " class=\"square square_" + currentSquareColor + "\" ></div>";
            result += currentSquareHtml;
        }
        return result;
    };
    SquareDrawer.prototype.setDropFigureOnSquareHandler = function (dropCallback) {
        $('.square').droppable({
            drop: function (event, ui) {
                var fromCoord = ui.draggable.attr('id').substring(1);
                var toCoord = this.id.substring(1);
                dropCallback(fromCoord, toCoord);
            }
        });
    };
    SquareDrawer.prototype.isBlackSquareAt = function (coord, width) {
        return ((coord % width + Math.floor(coord / width)) % 2) !== 0;
    };
    return SquareDrawer;
}());
//# sourceMappingURL=squareDrawer.js.map
var FigureDrawer = /** @class */ (function () {
    function FigureDrawer() {
    }
    FigureDrawer.prototype.drawMoving = function (fromCoord, toCoord, onComplete) {
        $('#f' + fromCoord).css('position', 'relative');
        $('#f' + fromCoord).css('z-index', 100);
        var leftShift = $('#s' + toCoord).position().left - $('#s' + fromCoord).position().left;
        var topShift = $('#s' + toCoord).position().top - $('#s' + fromCoord).position().top;
        $('#f' + fromCoord).animate({
            left: leftShift,
            top: topShift,
        }, 500, onComplete);
    };
    FigureDrawer.prototype.getHtml = function (coord, figure) {
        return "<div id=f" + coord + " class=\"figure figure_" + figure + "\"></div>";
    };
    FigureDrawer.prototype.setHandlers = function (coord, figure) {
        if (figure == 'P' || figure == 'Q') {
            $('#f' + coord).mousedown(function () {
                $(this).css('cursor', 'url(/images/movingCursor.cur), move');
            }).mouseup(function () {
                $(this).css('cursor', 'pointer');
            }).mouseleave(function () {
                $(this).css('cursor', 'pointer');
            }).draggable({
                start: function () {
                    $(this).css('cursor', 'url(/images/movingCursor.cur), move');
                }
            });
        }
    };
    return FigureDrawer;
}());
//# sourceMappingURL=figureDrawer.js.map
var RulesDrawer = /** @class */ (function () {
    function RulesDrawer() {
    }
    RulesDrawer.prototype.drawRules = function () {
        $('.rules__dialog').show();
        $('.rules__cover').show();
        $('.rules__close').click(function () {
            $('.rules__cover').hide();
            $('.rules__dialog').hide();
        });
    };
    return RulesDrawer;
}());
//# sourceMappingURL=rulesDrawer.js.map
var GameDrawer = /** @class */ (function () {
    function GameDrawer() {
        this._squareDrawer = new SquareDrawer();
        this._figureDrawer = new FigureDrawer();
        this._boardDrawer = new BoardDrawer();
        this._rulesDrawer = new RulesDrawer();
    }
    GameDrawer.prototype.setNewGameClickHandler = function (callback) {
        var _this = this;
        $('.button__new').click(callback);
        $('.button__rules').click(function () { return _this._rulesDrawer.drawRules(); });
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
var BoardState = /** @class */ (function () {
    function BoardState() {
    }
    return BoardState;
}());
var ServerRepository = /** @class */ (function () {
    function ServerRepository() {
    }
    ServerRepository.prototype.moveFigureOnServer = function (boardState, fromCoord, toCoord, callback) {
        $.get('/api/board/movefigure?fromPosition=' + fromCoord + '&toPosition=' + toCoord + '&cells=' + boardState.cells + '&turn=' + boardState.turn + '&mustGoFrom=' + boardState.mustGoFrom, callback);
    };
    ServerRepository.prototype.intellectStep = function (boardState, callback) {
        $.get('/api/intellect/calculatestep?cells=' + boardState.cells + '&turn=' + boardState.turn + '&mustGoFrom=' + boardState.mustGoFrom, callback);
    };
    return ServerRepository;
}());
//# sourceMappingURL=serverRepository.js.map