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