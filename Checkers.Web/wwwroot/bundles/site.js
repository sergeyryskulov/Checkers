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
        this._serverRepository.getFiguresFromServer(function (data) {
            _this._figuresCache = new Array(data.length - 1);
            var lineSquareCount = Math.sqrt(_this._figuresCache.length);
            _this._gameDrawer.drawSquares(lineSquareCount);
            _this._gameDrawer.setDropFigureOnSquareHandler(function (fromCoord, toCoord) {
                _this.moveFigureOnBoard(fromCoord, toCoord);
                _this._serverRepository.moveFigureOnServer(fromCoord, toCoord, function (data) { return _this.showFiguresOnBoard(data); });
            });
            _this.showFiguresOnBoard(data);
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
        console.log(boardState);
        var figuresLength = this.getFiguresLength(boardState);
        for (var coord = 0; coord < figuresLength; coord++) {
            this.showFigureAt(coord, boardState[coord]);
        }
        if (boardState[figuresLength] === 'b') {
            this._serverRepository.intellectStep(function (newsBoardState) { return _this.intellectStepCalculated(newsBoardState); });
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
var ServerRepository = /** @class */ (function () {
    function ServerRepository() {
    }
    ServerRepository.prototype.registerOnServer = function (position, callback) {
        var _this = this;
        $.post('/api/register?position=' + position, function (data) {
            _this._registrationId = data;
            callback(data);
        });
    };
    ServerRepository.prototype.clearGameOnServer = function (callback) {
        $.post('/api/newgame?registrationId=' + this._registrationId, callback);
    };
    ServerRepository.prototype.getFiguresFromServer = function (callback) {
        $.post('/api/getfigures?registrationId=' + this._registrationId, callback);
    };
    ServerRepository.prototype.moveFigureOnServer = function (fromCoord, toCoord, callback) {
        $.post('/api/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&registrationId=' + this._registrationId, callback);
    };
    ServerRepository.prototype.intellectStep = function (callback) {
        $.post('/api/intellectStep?registrationId=' + this._registrationId, callback);
    };
    return ServerRepository;
}());
//# sourceMappingURL=serverRepository.js.map