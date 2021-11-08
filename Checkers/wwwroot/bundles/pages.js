var $;
var Board = /** @class */ (function () {
    function Board() {
        this.isDragging = false;
        this.isFlipped = false;
    }
    Board.prototype.initBoard = function () {
        this.register();
    };
    Board.prototype.register = function () {
        var _this = this;
        $.post('/api/registerapi', function (data) { return _this.registered(data); });
    };
    Board.prototype.registered = function (data) {
        var _this = this;
        this.userId = data;
        this.start();
        setInterval(function () { return _this.getFiguresServer(); }, 10000);
        $('.newGame').click(function () { return _this.newGameServer(); });
        $('.flip').click(function () { return _this.flipBoard(); });
    };
    Board.prototype.start = function () {
        this.map = new Array(64);
        this.addSquares();
        this.getFiguresServer();
    };
    Board.prototype.flipBoard = function () {
        this.isFlipped = !this.isFlipped;
        this.start();
    };
    Board.prototype.newGameServer = function () {
        var _this = this;
        $.post('/api/newGame?userId=' + this.userId, function (data) { return _this.showFigures(data); });
    };
    Board.prototype.getFiguresServer = function () {
        var _this = this;
        if (this.isDragging) {
            return;
        }
        $.post('/api/getfigures?userId=' + this.userId, function (data) { return _this.showFigures(data); });
    };
    Board.prototype.setDraggable = function () {
        var that = this;
        $('.figure').draggable({
            start: function () {
                that.isDragging = true;
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
                that.moveFigureServer(fromCoord, toCoord);
                that.isDragging = false;
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
    Board.prototype.moveFigureServer = function (fromCoord, toCoord) {
        var _this = this;
        $.post('/api/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&userId=' + this.userId, function (data) { return _this.showFigures(data); });
    };
    return Board;
}());
//# sourceMappingURL=board.js.map