class ServerApi {

    registrationId: string;

    public registerOnServer(callback) {
        $.post('/api/register', (data) => {
            this.registrationId = data;
            callback(data);
        });
    }

    public clearGameOnServer(callback) {
        $.post('/api/newgame?registrationId=' + this.registrationId, callback);
    }

    public getFiguresFromServer(callback) {
        $.post('/api/getfigures?registrationId=' + this.registrationId, callback);
    }

    public moveFigureOnServer(fromCoord, toCoord, callback) {

        $.post('/api/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&registrationId=' + this.registrationId, callback);
    }

    public intellectStep(callback) {

        $.post('/api/intellectStep?registrationId=' + this.registrationId, callback);
    }
}