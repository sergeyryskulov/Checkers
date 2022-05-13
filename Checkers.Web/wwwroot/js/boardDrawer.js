var BoardDrawer = /** @class */ (function () {
    function BoardDrawer() {
    }
    BoardDrawer.prototype.drawBoard = function (squaresHtml) {
        var boardWidth = Math.min(document.documentElement.clientWidth, document.documentElement.clientHeight) - 100;
        $('.board__inner').width(boardWidth);
        $('.board__inner').height(boardWidth);
        $('.board__inner').html(squaresHtml);
    };
    return BoardDrawer;
}());
//# sourceMappingURL=boardDrawer.js.map