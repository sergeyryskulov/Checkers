var BoardState = /** @class */ (function () {
    function BoardState() {
    }
    return BoardState;
}());
var ServerRepository = /** @class */ (function () {
    function ServerRepository() {
    }
    ServerRepository.prototype.moveFigureOnServer = function (boardState, fromCoord, toCoord, callback) {
        $.post('/api/board/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&cells=' + boardState.cells + '&turn=' + boardState.turn + '&mustGoFrom' + boardState.mustGoFrom, callback);
    };
    ServerRepository.prototype.intellectStep = function (boardState, callback) {
        $.post('/api/intellect/calculatestep?cells=' + boardState.cells + '&turn=' + boardState.turn + '&mustGoFrom' + boardState.mustGoFrom, callback);
    };
    return ServerRepository;
}());
//# sourceMappingURL=serverRepository.js.map