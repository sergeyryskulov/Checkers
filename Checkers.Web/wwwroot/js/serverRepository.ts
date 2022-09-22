class ServerRepository {

    _registrationId: string;

    public moveFigureOnServer(boardState, fromCoord, toCoord, callback) {

        $.post('/api/board/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&registrationId=' + this._registrationId +'&boardState=' + boardState, callback);
    }

    public intellectStep(boardState, callback) {
        $.post('/api/intellect/calculatestep?boardState=' + boardState, callback);
    }
}