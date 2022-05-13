class ServerRepository {

    _registrationId: string;

    public registerOnServer(position, callback) {
        $.post('/api/register?position=' + position, (data) => {
            this._registrationId = data;
            callback(data);
        });
    }

    public clearGameOnServer(callback) {
        $.post('/api/newgame?registrationId=' + this._registrationId, callback);
    }

    public getFiguresFromServer(callback) {
        $.post('/api/getfigures?registrationId=' + this._registrationId, callback);
    }

    public moveFigureOnServer(fromCoord, toCoord, callback) {

        $.post('/api/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&registrationId=' + this._registrationId, callback);
    }

    public intellectStep(callback) {

        $.post('/api/intellectStep?registrationId=' + this._registrationId, callback);
    }
}