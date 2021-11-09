var BoardRepository = /** @class */ (function () {
    function BoardRepository() {
    }
    BoardRepository.prototype.register = function (callback) {
        var _this = this;
        $.post('/api/registerapi', function (data) {
            _this.userId = data;
            callback(data);
        });
    };
    BoardRepository.prototype.newGame = function (callback) {
        $.post('/api/newGame?userId=' + this.userId, callback);
    };
    BoardRepository.prototype.getFigures = function (callback) {
        $.post('/api/getfigures?userId=' + this.userId, callback);
    };
    BoardRepository.prototype.moveFigureServer = function (fromCoord, toCoord, callback) {
        $.post('/api/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&userId=' + this.userId, callback);
    };
    return BoardRepository;
}());
//# sourceMappingURL=boardRepository.js.map