var BoardDrawer = /** @class */ (function () {
    function BoardDrawer() {
    }
    BoardDrawer.prototype.setNewGameClickHandler = function (callback) {
        $('.button__new').click(callback);
    };
    BoardDrawer.prototype.drawSquares = function (width) {
        var boardWidth = Math.min(document.documentElement.clientWidth, document.documentElement.clientHeight) - 100;
        $('.board__inner').width(boardWidth);
        $('.board__inner').height(boardWidth);
        $('.board__inner').html('');
        for (var coord = 0; coord < width * width; coord++) {
            var currentSquareColor = this.isBlackSquareAt(coord, width) ? 'black' : 'white';
            var currentSquareHtml = "<div id=s" + coord + " class=\"square square_" + currentSquareColor + "\" ></div>";
            $('.board__inner').append(currentSquareHtml);
        }
    };
    BoardDrawer.prototype.drawMoving = function (fromCoord, toCoord, onComplete) {
        $('#f' + fromCoord).css('position', 'relative');
        $('#f' + fromCoord).css('z-index', 100);
        var leftShift = $('#s' + toCoord).position().left - $('#s' + fromCoord).position().left;
        var topShift = $('#s' + toCoord).position().top - $('#s' + fromCoord).position().top;
        $('#f' + fromCoord).animate({
            left: leftShift,
            top: topShift,
        }, 500, onComplete);
    };
    BoardDrawer.prototype.drawFigure = function (coord, figure) {
        var divFigure = '<div id=f$coord class="figure figure_$figure"></div>';
        $('#s' + coord).html(divFigure.replace('$coord', '' + coord).replace('$figure', figure));
        if (figure == 'P' || figure == 'Q') {
            $('#s' + coord + ' .figure').mousedown(function () {
                $(this).css('cursor', 'url(/pages/board/grab.cur), move');
            }).mouseup(function () {
                $(this).css('cursor', 'pointer');
            }).mouseleave(function () {
                $(this).css('cursor', 'pointer');
            }).draggable({
                start: function () {
                    $(this).css('cursor', 'url(/pages/board/grab.cur), move');
                }
            });
        }
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
    return BoardDrawer;
}());
//# sourceMappingURL=boardDrawer.js.map