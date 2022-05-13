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