var FigureDrawer = /** @class */ (function () {
    function FigureDrawer() {
    }
    FigureDrawer.prototype.drawMoving = function (fromCoord, toCoord, onComplete) {
        $('#f' + fromCoord).css('position', 'relative');
        $('#f' + fromCoord).css('z-index', 100);
        var leftShift = $('#s' + toCoord).position().left - $('#s' + fromCoord).position().left;
        var topShift = $('#s' + toCoord).position().top - $('#s' + fromCoord).position().top;
        $('#f' + fromCoord).animate({
            left: leftShift,
            top: topShift,
        }, 500, onComplete);
    };
    FigureDrawer.prototype.getHtml = function (coord, figure) {
        return "<div id=f" + coord + " class=\"figure figure_" + figure + "\"></div>";
    };
    FigureDrawer.prototype.setHandlers = function (coord, figure) {
        if (figure == 'P' || figure == 'Q') {
            $('#f' + coord).mousedown(function () {
                $(this).css('cursor', 'url(/images/movingCursor.cur), move');
            }).mouseup(function () {
                $(this).css('cursor', 'pointer');
            }).mouseleave(function () {
                $(this).css('cursor', 'pointer');
            }).draggable({
                start: function () {
                    $(this).css('cursor', 'url(/images/movingCursor.cur), move');
                }
            });
        }
    };
    return FigureDrawer;
}());
//# sourceMappingURL=figureDrawer.js.map