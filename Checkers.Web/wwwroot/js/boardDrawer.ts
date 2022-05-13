class BoardDrawer {

    public drawBoard(squaresHtml : string) {
        let boardWidth = Math.min(document.documentElement.clientWidth, document.documentElement.clientHeight) - 100;

        $('.board__inner').width(boardWidth);
        $('.board__inner').height(boardWidth);

        $('.board__inner').html(squaresHtml);
    }

    
}