class BoardRepository {

    userId: string;

    public registerOnServer(callback) {
        $.post('/api/register', (data) => {
            this.userId = data;
            callback(data);
        });
    }

    public clearGameOnServer(callback) {
        $.post('/api/newgame?userId=' + this.userId, callback);
    }

    public getFiguresFromServer(callback) {
        $.post('/api/getfigures?userId=' + this.userId, callback);
    }

    public moveFigureOnServer(fromCoord, toCoord, callback) {

        $.post('/api/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&userId=' + this.userId, callback);
    }

}