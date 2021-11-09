var BoardDrawer = /** @class */ (function () {
    function BoardDrawer() {
    }
    BoardDrawer.prototype.setFlipClickHandler = function (callback) {
        $('.flip').click(callback);
    };
    BoardDrawer.prototype.setNewGameClickHandler = function (callback) {
        $('.newGame').click(callback);
    };
    BoardDrawer.prototype.drawSquares = function (isFlipped) {
        $('.board').html('');
        var divSquare = '<div  id=s$coord class="square $color"></div>';
        for (var coord = 0; coord < 64; coord++) {
            $('.board').append(divSquare.replace('$coord', '' + (isFlipped ? 63 - coord : coord)).replace('$color', this.isBlackSquareAt(coord) ? 'black' : 'white'));
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
    BoardDrawer.prototype.isBlackSquareAt = function (coord) {
        return ((coord % 8 + Math.floor(coord / 8)) % 2) !== 0;
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