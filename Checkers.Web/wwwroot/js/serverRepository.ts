﻿class BoardState{

    cells: string;

    turn: string;

    mustGoFrom?: number;
}

class ServerRepository {

    _registrationId: string;

    public moveFigureOnServer(boardState : BoardState, fromCoord, toCoord, callback) {
        
        $.post('/api/board/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord + '&cells=' + boardState.cells + '&turn=' + boardState.turn +'&mustGoFrom' + boardState.mustGoFrom, callback);
    }

    public intellectStep(boardState : BoardState, callback) {
        $.post('/api/intellect/calculatestep?cells=' + boardState.cells + '&turn=' + boardState.turn +'&mustGoFrom' + boardState.mustGoFrom, callback);
    }
}