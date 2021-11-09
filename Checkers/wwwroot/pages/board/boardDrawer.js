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