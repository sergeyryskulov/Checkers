class BoardState{

    cells: string;

    turn: string;

    mustGoFrom?: number;
}

class ServerRepository {

    _registrationId: string;

    public moveFigureOnServer(boardState : BoardState, fromCoord, toCoord, callback) {
        
        $.get('/api/board/movefigure?fromPosition=' + fromCoord + '&toPosition=' + toCoord + '&cells=' + boardState.cells + '&turn=' + boardState.turn +'&mustGoFrom=' + boardState.mustGoFrom, callback);
    }

    public intellectStep(boardState : BoardState, callback) {
        $.get('/api/intellect/calculatestep?cells=' + boardState.cells + '&turn=' + boardState.turn +'&mustGoFrom=' + boardState.mustGoFrom, callback);
    }
}