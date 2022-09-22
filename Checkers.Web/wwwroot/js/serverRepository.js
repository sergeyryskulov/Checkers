var ServerRepository = /** @class */ (function () {
    function ServerRepository() {
    }
    ServerRepository.prototype.moveFigureOnServer = function (boardState, fromCoord, toCoord, callback) {
        $.post('/api/board/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&registrationId=' + this._registrationId + '&boardState=' + boardState, callback);
    };
    ServerRepository.prototype.intellectStep = function (boardState, callback) {
        $.post('/api/intellect/calculatestep?boardState=' + boardState, callback);
    };
    return ServerRepository;
}());
//# sourceMappingURL=serverRepository.js.map