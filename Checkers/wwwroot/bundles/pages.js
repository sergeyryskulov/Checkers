var $;
var Board = /** @class */ (function () {
    function Board() {
        this.isFlipped = false;
    }
    Board.prototype.initBoard = function () {
        var _this = this;
        this.boardRepository = new BoardRepository();
        this.boardDrawer = new BoardDrawer();
        this.boardRepository.register(function (data) {
            _this.startGame();
            _this.boardDrawer.setFlipHandler(function () { return _this.flipBoard(); });
            _this.boardDrawer.setNewGameHabdler(function () { return _this.boardRepository.newGame(function (data) { return _this.showFigures(data); }); });
        });
    };
    Board.prototype.startGame = function () {
        var _this = this;
        this.map = new Array(64);
        this.boardDrawer.addSquares(this.isFlipped, function (fromCoord, toCoord) {
            _this.moveFigure(fromCoord, toCoord);
            _this.boardRepository.moveFigureServer(fromCoord, toCoord, function (data) { return _this.showFigures(data); });
        });
        this.boardRepository.getFigures(function (data) { return _this.showFigures(data); });
    };
    Board.prototype.flipBoard = function () {
        this.isFlipped = !this.isFlipped;
        this.startGame();
    };
    Board.prototype.setDraggable = function () {
        $('.figure').draggable({
            start: function () {
                // that.isDragging = true;
            }
        });
    };
    Board.prototype.moveFigure = function (fromCoord, toCoord) {
        var figure = this.map[fromCoord];
        this.showFigureAt(fromCoord, '1');
        this.showFigureAt(toCoord, figure);
    };
    Board.prototype.showFigures = function (figures) {
        for (var coord = 0; coord < 64; coord++) {
            this.showFigureAt(coord, figures[coord]);
        }
    };
    Board.prototype.gettChessSymbol = function (figure) {
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
    Board.prototype.showFigureAt = function (coord, figure) {
        if (this.map[coord] === figure) {
            return;
        }
        this.map[coord] = figure;
        var divFigure = '<div id=f$coord class=figure>$figure</div>';
        $('#s' + coord).html(divFigure.replace('$coord', '' + coord).replace('$figure', this.gettChessSymbol(figure)));
        this.setDraggable();
    };
    return Board;
}());
//# sourceMappingURL=board.js.map
var BoardRepository = /** @class */ (function () {
    function BoardRepository() {
    }
    BoardRepository.prototype.register = function (callback) {
        var _this = this;
        $.post('/api/registerapi', function (data) {
            _this.userId = data;
            callback(data);
        });
    };
    BoardRepository.prototype.newGame = function (callback) {
        $.post('/api/newGame?userId=' + this.userId, callback);
    };
    BoardRepository.prototype.getFigures = function (callback) {
        $.post('/api/getfigures?userId=' + this.userId, callback);
    };
    BoardRepository.prototype.moveFigureServer = function (fromCoord, toCoord, callback) {
        $.post('/api/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&userId=' + this.userId, callback);
    };
    return BoardRepository;
}());
//# sourceMappingURL=boardRepository.js.map
var BoardDrawer = /** @class */ (function () {
    function BoardDrawer() {
    }
    BoardDrawer.prototype.setFlipHandler = function (callback) {
        $('.flip').click(callback);
    };
    BoardDrawer.prototype.setNewGameHabdler = function (callback) {
        $('.newGame').click(callback);
    };
    BoardDrawer.prototype.addSquares = function (isFlipped, dropCallback) {
        $('.board').html('');
        var divSquare = '<div  id=s$coord class="square $color"></div>';
        for (var coord = 0; coord < 64; coord++) {
            $('.board').append(divSquare.replace('$coord', '' + (isFlipped ? 63 - coord : coord)).replace('$color', this.isBlackSquareAt(coord) ? 'black' : 'white'));
        }
        this.setDroppable(dropCallback);
    };
    BoardDrawer.prototype.isBlackSquareAt = function (coord) {
        return ((coord % 8 + Math.floor(coord / 8)) % 2) !== 0;
    };
    BoardDrawer.prototype.setDroppable = function (dropCallback) {
        var that = this;
        $('.square').droppable({
            drop: function (event, ui) {
                var fromCoord = ui.draggable.attr('id').substring(1);
                var toCoord = this.id.substring(1);
                dropCallback(fromCoord, toCoord);
                //   that.moveFigure(fromCoord, toCoord);
                // that.boardRepository.moveFigureServer(fromCoord, toCoord, (data) => that.showFigures(data));
            }
        });
    };
    return BoardDrawer;
}());
//# sourceMappingURL=boardDrawer.js.map