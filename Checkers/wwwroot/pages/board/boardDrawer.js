var BoardDrawer = /** @class */ (function () {
    function BoardDrawer() {
    }
    BoardDrawer.prototype.setFlipClickHandler = function (callback) {
        $('.flip').click(callback);
    };
    BoardDrawer.prototype.setNewGameClickHandler = function (callback) {
        $('.newGame').click(callback);
    };
    BoardDrawer.prototype.drawSquares = function (width) {
        $('.board').html('');
        var divSquare = '<div  id=s$coord class="square $color"></div>';
        for (var coord = 0; coord < width * width; coord++) {
            $('.board').append(divSquare.replace('$coord', '' + coord).replace('$color', this.isBlackSquareAt(coord, width) ? 'black' : 'white'));
        }
    };
    BoardDrawer.prototype.drawMoving = function (fromCoord, toCoord, onComplete) {
        $('#f' + fromCoord).css('position', 'relative');
        var leftShift = $('#s' + toCoord).position().left - $('#s' + fromCoord).position().left;
        var topShift = $('#s' + toCoord).position().top - $('#s' + fromCoord).position().top;
        $('#f' + fromCoord).animate({
            left: leftShift,
            top: topShift,
        }, 300, onComplete);
    };
    BoardDrawer.prototype.drawFigure = function (coord, figure) {
        var divFigure = '<div id=f$coord class="figure figure_$figure"></div>';
        $('#s' + coord).html(divFigure.replace('$coord', '' + coord).replace('$figure', figure));
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
            case 'Q': return '&#9813;';
            case 'P': return '&#9817;';
            case 'p': return '&#9823;';
            default:
        }
        return '';
    };
    return BoardDrawer;
}());
//# sourceMappingURL=boardDrawer.js.map