class BoardRepository {

    userId: string;

    public register(callback) {
        $.post('/api/registerapi', (data) => {
            this.userId = data;
            callback(data);
        });
    }

    public newGame(callback) {
        $.post('/api/newGame?userId=' + this.userId, callback);
    }

    public getFigures(callback) {
        $.post('/api/getfigures?userId=' + this.userId, callback);
    }

    public moveFigureServer(fromCoord, toCoord, callback) {

        $.post('/api/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&userId=' + this.userId, callback);
    }

}