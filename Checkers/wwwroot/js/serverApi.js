var ServerApi = /** @class */ (function () {
    function ServerApi() {
    }
    ServerApi.prototype.registerOnServer = function (position, callback) {
        var _this = this;
        $.post('/api/register?position=' + position, function (data) {
            _this.registrationId = data;
            callback(data);
        });
    };
    ServerApi.prototype.clearGameOnServer = function (callback) {
        $.post('/api/newgame?registrationId=' + this.registrationId, callback);
    };
    ServerApi.prototype.getFiguresFromServer = function (callback) {
        $.post('/api/getfigures?registrationId=' + this.registrationId, callback);
    };
    ServerApi.prototype.moveFigureOnServer = function (fromCoord, toCoord, callback) {
        $.post('/api/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&registrationId=' + this.registrationId, callback);
    };
    ServerApi.prototype.intellectStep = function (callback) {
        $.post('/api/intellectStep?registrationId=' + this.registrationId, callback);
    };
    return ServerApi;
}());
//# sourceMappingURL=serverApi.js.map