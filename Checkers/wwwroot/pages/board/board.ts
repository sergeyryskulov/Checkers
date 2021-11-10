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

        this.figuresCache = new Array(64);

        this.boardDrawer.drawSquares(this.isFlipped);

        this.boardDrawer.setDropFigureOnSquareHandler((fromCoord, toCoord) => {
            this.moveFigureOnBoard(fromCoord, toCoord);
            this.serverApi.moveFigureOnServer(fromCoord, toCoord, (data) => this.showFiguresOnBoard(data));
        });

        this.serverApi.getFiguresFromServer((data) => this.showFiguresOnBoard(data));
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

    private showFiguresOnBoard(figures) {
        for (let coord = 0; coord < 64; coord++) {
            this.showFigureAt(coord, figures[coord]);
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