var Figure = /** @class */ (function () {
    function Figure() {
    }
    Figure.prototype.drawMoving = function (fromCoord, toCoord, onComplete) {
        $('#f' + fromCoord).css('position', 'relative');
        $('#f' + fromCoord).css('z-index', 100);
        var leftShift = $('#s' + toCoord).position().left - $('#s' + fromCoord).position().left;
        var topShift = $('#s' + toCoord).position().top - $('#s' + fromCoord).position().top;
        $('#f' + fromCoord).animate({
            left: leftShift,
            top: topShift,
        }, 500, onComplete);
    };
    Figure.prototype.getHtml = function (coord, figure) {
        return "<div id=f" + coord + " class=\"figure figure_" + figure + "\"></div>";
    };
    Figure.prototype.setHandlers = function (coord, figure) {
        if (figure == 'P' || figure == 'Q') {
            $('#f' + coord).mousedown(function () {
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
    return Figure;
}());
//# sourceMappingURL=figure.js.map