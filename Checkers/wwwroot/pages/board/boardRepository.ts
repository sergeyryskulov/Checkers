class BoardRepository {

    userId: string;

    public registerOnServer(callback) {
        $.post('/api/registerapi', (data) => {
            this.userId = data;
            callback(data);
        });
    }

    public clearGameOnServer(callback) {
        $.post('/api/newGame?userId=' + this.userId, callback);
    }

    public getFiguresFromServer(callback) {
        $.post('/api/getfiguresapi?userId=' + this.userId, callback);
    }

    public moveFigureOnServer(fromCoord, toCoord, callback) {

        $.post('/api/movefigureapi?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&userId=' + this.userId, callback);
    }

}