var BoardState = /** @class */ (function () {
    function BoardState() {
    }
    return BoardState;
}());
var ServerRepository = /** @class */ (function () {
    function ServerRepository() {
    }
    ServerRepository.prototype.moveFigureOnServer = function (boardState, fromCoord, toCoord, callback) {
        $.get('/api/board/movefigure?fromPosition=' + fromCoord + '&toPosition=' + toCoord + '&cells=' + boardState.cells + '&turn=' + boardState.turn + '&mustGoFrom=' + boardState.mustGoFrom, callback);
    };
    ServerRepository.prototype.intellectStep = function (boardState, callback) {
        $.get('/api/intellect/calculatestep?cells=' + boardState.cells + '&turn=' + boardState.turn + '&mustGoFrom=' + boardState.mustGoFrom, callback);
    };
    return ServerRepository;
}());
//# sourceMappingURL=serverRepository.js.map