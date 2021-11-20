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
        var _this = this;
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
            this.serverApi.intellectStep(function (boardState) { return _this.showFiguresOnBoard(boardState); });
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
var ServerApi = /** @class */ (function () {
    function ServerApi() {
    }
    ServerApi.prototype.registerOnServer = function (position, callback) {
        var _this = this;
        $.post('/api/register?position=' + position, function (data) {
            _this.registrationId = data;
            callback(data);
        });
    };
    ServerApi.prototype.clearGameOnServer = function (callback) {
        $.post('/api/newgame?registrationId=' + this.registrationId, callback);
    };
    ServerApi.prototype.getFiguresFromServer = function (callback) {
        $.post('/api/getfigures?registrationId=' + this.registrationId, callback);
    };
    ServerApi.prototype.moveFigureOnServer = function (fromCoord, toCoord, callback) {
        $.post('/api/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&registrationId=' + this.registrationId, callback);
    };
    ServerApi.prototype.intellectStep = function (callback) {
        $.post('/api/intellectStep?registrationId=' + this.registrationId, callback);
    };
    return ServerApi;
}());
//# sourceMappingURL=serverApi.js.map
var BoardDrawer = /** @class */ (function () {
    function BoardDrawer() {
    }
    BoardDrawer.prototype.setFlipClickHandler = function (callback) {
        $('.flip').click(callback);
    };
    BoardDrawer.prototype.setNewGameClickHandler = function (callback) {
        $('.newGame').click(callback);
    };
    BoardDrawer.prototype.drawSquares = function (isFlipped, width) {
        $('.board').html('');
        var divSquare = '<div  id=s$coord class="square $color"></div>';
        for (var coord = 0; coord < width * width; coord++) {
            $('.board').append(divSquare.replace('$coord', '' + (isFlipped ? width * width - 1 - coord : coord)).replace('$color', this.isBlackSquareAt(coord, width) ? 'black' : 'white'));
        }
    };
    BoardDrawer.prototype.drawFigure = function (coord, figure) {
        var divFigure = '<div id=f$coord class=figure>$figure</div>';
        $('#s' + coord).html(divFigure.replace('$coord', '' + coord).replace('$figure', this.gettChessSymbol(figure)));
        $('.figure').draggable();
    };
    BoardDrawer.prototype.setDropFigureOnSquareHandler = function (dropCallback) {
        $('.square').droppable({
            drop: function (event, ui) {
                var fromCoord = ui.draggable.attr('id').substring(1);
                var toCoord = this.id.substring(1);
                dropCallback(fromCoord, toCoord);
            }
        });
    };
    BoardDrawer.prototype.isBlackSquareAt = function (coord, width) {
        return ((coord % width + Math.floor(coord / width)) % 2) !== 0;
    };
    BoardDrawer.prototype.gettChessSymbol = function (figure) {
        switch (figure) {
            case 'K': return '&#9812;';
            case 'Q': return '&#9813;';
            case 'R': return '&#9814;';
            case 'B': return '&#9815;';
            case 'N': return '&#9816;';
            case 'P': return '&#9817;';
            case 'k': return '&#9818;';
            case 'q': return '&#9819;';
            case 'r': return '&#9820;';
            case 'b': return '&#9821;';
            case 'n': return '&#9822;';
            case 'p': return '&#9823;';
            case 'k': return '&#9824;';
            default:
        }
        return '';
    };
    return BoardDrawer;
}());
//# sourceMappingURL=boardDrawer.js.map