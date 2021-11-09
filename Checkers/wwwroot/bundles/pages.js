var $;
var Board = /** @class */ (function () {
    function Board() {
        this.isFlipped = false;
    }
    Board.prototype.initBoard = function () {
        var _this = this;
        this.boardRepository = new BoardRepository();
        this.boardRepository.register(function (data) {
            _this.start();
            $('.newGame').click(function () { return _this.boardRepository.newGame(function (data) { return _this.showFigures(data); }); });
            $('.flip').click(function () { return _this.flipBoard(); });
        });
    };
    Board.prototype.start = function () {
        var _this = this;
        this.map = new Array(64);
        this.addSquares();
        this.boardRepository.getFigures(function (data) { return _this.showFigures(data); });
    };
    Board.prototype.flipBoard = function () {
        this.isFlipped = !this.isFlipped;
        this.start();
    };
    Board.prototype.setDraggable = function () {
        $('.figure').draggable({
            start: function () {
                // that.isDragging = true;
            }
        });
    };
    Board.prototype.setDroppable = function () {
        var that = this;
        $('.square').droppable({
            drop: function (event, ui) {
                var fromCoord = ui.draggable.attr('id').substring(1);
                var toCoord = this.id.substring(1);
                that.moveFigure(fromCoord, toCoord);
                that.boardRepository.moveFigureServer(fromCoord, toCoord, function (data) { return that.showFigures(data); });
                //  that.isDragging = false;
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
    Board.prototype.addSquares = function () {
        $('.board').html('');
        var divSquare = '<div  id=s$coord class="square $color"></div>';
        for (var coord = 0; coord < 64; coord++) {
            $('.board').append(divSquare.replace('$coord', '' + (this.isFlipped ? 63 - coord : coord)).replace('$color', this.isBlackSquareAt(coord) ? 'black' : 'white'));
        }
        this.setDroppable();
    };
    Board.prototype.isBlackSquareAt = function (coord) {
        return ((coord % 8 + Math.floor(coord / 8)) % 2) !== 0;
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