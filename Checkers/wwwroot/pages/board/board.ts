var $;

class Board {

    private serverApi: ServerApi;

    private boardDrawer: BoardDrawer;

    private figuresCache : Array<string>;

    private isFlipped = false;

    initBoard() {
        this.serverApi = new ServerApi();

        this.boardDrawer = new BoardDrawer();

        this.serverApi.registerOnServer(() => {
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
            let width = Math.sqrt(this.figuresCache.length);
            $('.board').width(width * 80);
            $('.board').height(width * 80);
            this.boardDrawer.drawSquares(this.isFlipped, width);
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