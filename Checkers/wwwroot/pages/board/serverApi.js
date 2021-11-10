var ServerApi = /** @class */ (function () {
    function ServerApi() {
    }
    ServerApi.prototype.registerOnServer = function (callback) {
        var _this = this;
        $.post('/api/register', function (data) {
            _this.userId = data;
            callback(data);
        });
    };
    ServerApi.prototype.clearGameOnServer = function (callback) {
        $.post('/api/newgame?userId=' + this.userId, callback);
    };
    ServerApi.prototype.getFiguresFromServer = function (callback) {
        $.post('/api/getfigures?userId=' + this.userId, callback);
    };
    ServerApi.prototype.moveFigureOnServer = function (fromCoord, toCoord, callback) {
        $.post('/api/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&userId=' + this.userId, callback);
    };
    return ServerApi;
}());
//# sourceMappingURL=serverApi.js.map