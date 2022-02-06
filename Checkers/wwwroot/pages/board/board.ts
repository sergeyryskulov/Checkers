var $;

class Board {

    private serverApi: ServerApi;

    private boardDrawer: BoardDrawer;

    private figuresCache : Array<string>;

    private isFlipped = false;

    initBoard() {
        this.serverApi = new ServerApi();

        this.boardDrawer = new BoardDrawer();

        let position = '';
        if (window.location.href.split('?pos=').length === 2) {
            position = window.location.href.split('?pos=')[1];
        }

        this.serverApi.registerOnServer(position, () => {
            this.boardDrawer.setFlipClickHandler(() => this.flipBoard());

            this.boardDrawer.setNewGameClickHandler(
                () => this.serverApi.clearGameOnServer(
                    (clearedFigures) => this.showFiguresOnBoard(clearedFigures)));

            this.showBoard();
        });
    }

    private showBoard() {


        this.serverApi.getFiguresFromServer((data) => {
            this.figuresCache = new Array(data.length - 1);
            let lineSquareCount = Math.sqrt(this.figuresCache.length);
            ;
            let boardWidth = Math.min(document.documentElement.clientWidth, document.documentElement.clientHeight) - 60;
                                   
            $('.board').width(boardWidth);
            $('.board').height(boardWidth);
            this.boardDrawer.drawSquares(this.isFlipped, lineSquareCount);
            this.boardDrawer.setDropFigureOnSquareHandler((fromCoord, toCoord) => {
                this.moveFigureOnBoard(fromCoord, toCoord);
                this.serverApi.moveFigureOnServer(fromCoord, toCoord, (data) => this.showFiguresOnBoard(data));
            });
            this.showFiguresOnBoard(data);
        });
    }

    private flipBoard() {
        this.isFlipped = !this.isFlipped;
        this.showBoard();
    }

    private moveFigureOnBoard(fromCoord, toCoord) {
        let figure = this.figuresCache[fromCoord];
        this.showFigureAt(fromCoord, '1');
        this.showFigureAt(toCoord, figure);
    }

    private showFiguresOnBoard(boardState) {
        var figuresLength = boardState.length - 1;
        if (boardState[boardState.length - 1] !== 'w' &&
            boardState[boardState.length - 1] !== 'W' &&
            boardState[boardState.length - 1] !== 'b' &&
            boardState[boardState.length - 1] !== 'B') {

            figuresLength = Math.max(boardState.indexOf('w'), boardState.indexOf('W'), boardState.indexOf('b'), boardState.indexOf('B'));
        }

        for (let coord = 0; coord < figuresLength; coord++) {
            this.showFigureAt(coord, boardState[coord]);
        }

        $('.state').text(boardState);

        if (boardState[figuresLength]=== 'w') {
            $('.turn').text('Ход белых');
        } if (boardState[figuresLength] === 'W') {
            $('.turn').text('Белые выиграли!');
            
        } else if (boardState[figuresLength] === 'b'){
            $('.turn').text('Ход черных');
            this.serverApi.intellectStep((boardState) => this.showFiguresOnBoard(boardState));
            
        } else if (boardState[figuresLength] === 'B') {
            $('.turn').text('Черные выиграли!');
        }

    }

    private showFigureAt(coord: number, figure: string) {
        if (this.figuresCache[coord] === figure) {
            return;
        }
        this.figuresCache[coord] = figure;
        this.boardDrawer.drawFigure(coord, figure);
    }
}