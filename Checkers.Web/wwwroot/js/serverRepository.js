var ServerRepository = /** @class */ (function () {
    function ServerRepository() {
    }
    ServerRepository.prototype.registerOnServer = function (position, callback) {
        var _this = this;
        $.post('/api/register?position=' + position, function (data) {
            _this._registrationId = data;
            callback(data);
        });
    };
    ServerRepository.prototype.clearGameOnServer = function (callback) {
        $.post('/api/newgame?registrationId=' + this._registrationId, callback);
    };
    ServerRepository.prototype.getFiguresFromServer = function (callback) {
        $.post('/api/getfigures?registrationId=' + this._registrationId, callback);
    };
    ServerRepository.prototype.moveFigureOnServer = function (fromCoord, toCoord, callback) {
        $.post('/api/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&registrationId=' + this._registrationId, callback);
    };
    ServerRepository.prototype.intellectStep = function (callback) {
        $.post('/api/intellectStep?registrationId=' + this._registrationId, callback);
    };
    return ServerRepository;
}());
//# sourceMappingURL=serverRepository.js.map