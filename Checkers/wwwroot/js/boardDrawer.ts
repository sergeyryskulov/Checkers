class BoardDrawer {

    public setNewGameClickHandler(callback) {
        $('.button__new').click(callback);
    }

    public drawSquares(width) {
        let boardWidth = Math.min(document.documentElement.clientWidth, document.documentElement.clientHeight) - 100;

        $('.board__inner').width(boardWidth);
        $('.board__inner').height(boardWidth);
        $('.board__inner').html(new Square().getSquaresHtml(width));

    }

    public drawMoving(fromCoord, toCoord, onComplete) {
        new Figure().drawMoving(fromCoord, toCoord, onComplete);
    }

    public drawFigure(coord, figure) {

        $('#s' + coord).html(new Figure().getHtml(coord, figure));
        new Figure().setHandlers(coord, figure);

    }

    public setDropFigureOnSquareHandler(dropCallback) {
       new Square().setDropFigureOnSquareHandler(dropCallback);
    }
}