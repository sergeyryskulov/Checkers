var BoardRepository = /** @class */ (function () {
    function BoardRepository() {
    }
    BoardRepository.prototype.registerOnServer = function (callback) {
        var _this = this;
        $.post('/api/registerapi', function (data) {
            _this.userId = data;
            callback(data);
        });
    };
    BoardRepository.prototype.clearGameOnServer = function (callback) {
        $.post('/api/newgameapi?userId=' + this.userId, callback);
    };
    BoardRepository.prototype.getFiguresFromServer = function (callback) {
        $.post('/api/getfiguresapi?userId=' + this.userId, callback);
    };
    BoardRepository.prototype.moveFigureOnServer = function (fromCoord, toCoord, callback) {
        $.post('/api/movefigureapi?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&userId=' + this.userId, callback);
    };
    return BoardRepository;
}());
//# sourceMappingURL=boardRepository.js.map