var $;

class Board {

    private serverApi: ServerApi;

    private boardDrawer: BoardDrawer;

    private figuresCache : Array<string>;

    initBoard() {
        this.serverApi = new ServerApi();

        this.boardDrawer = new BoardDrawer();

        let position = '';
        if (window.location.href.split('?pos=').length === 2) {
            position = window.location.href.split('?pos=')[1];
        }

        this.serverApi.registerOnServer(position, () => {

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
            let boardWidth = Math.min(document.documentElement.clientWidth, document.documentElement.clientHeight) - 100;
                                   
            $('.board').width(boardWidth);
            $('.board').height(boardWidth);
            this.boardDrawer.drawSquares(lineSquareCount);
            this.boardDrawer.setDropFigureOnSquareHandler((fromCoord, toCoord) => {
                this.moveFigureOnBoard(fromCoord, toCoord);
                this.serverApi.moveFigureOnServer(fromCoord, toCoord, (data) => this.showFiguresOnBoard(data));
            });
            this.showFiguresOnBoard(data);
        });
    }

    private moveFigureOnBoard(fromCoord, toCoord) {
        let figure = this.figuresCache[fromCoord];
        this.showFigureAt(fromCoord, '1');
        this.showFigureAt(toCoord, figure);
    }

    private getFiguresLength(boardState: string) {
        var figuresLength = boardState.length - 1;
        if (boardState[boardState.length - 1] !== 'w' &&
            boardState[boardState.length - 1] !== 'W' &&
            boardState[boardState.length - 1] !== 'b' &&
            boardState[boardState.length - 1] !== 'B') {

            figuresLength = Math.max(boardState.indexOf('w'), boardState.indexOf('W'), boardState.indexOf('b'), boardState.indexOf('B'));
        }
        return figuresLength;
    }

    private showFiguresOnBoard(boardState) {
        console.log(boardState);

        var figuresLength = this.getFiguresLength(boardState);

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
            this.serverApi.intellectStep((newsBoardState) =>  this.intellectStepCalculated(newsBoardState));
            
        } else if (boardState[figuresLength] === 'B') {
            $('.turn').text('Черные выиграли!');
        }

    }

    private intellectStepCalculated(newBoardState: string) {

        let fromFigureCoord = -1;
        let toFigureCoord = -1;

        for (let coord = 0; coord < this.getFiguresLength(newBoardState); coord++) {
            if (
                newBoardState[coord] == '1' &&
                    (this.figuresCache[coord] == 'p' || this.figuresCache[coord] == 'q')) {
                fromFigureCoord = coord;
            }
            if (
                (newBoardState[coord] == 'p' || newBoardState[coord] == 'q') && (this.figuresCache[coord] == '1')) {
                toFigureCoord = coord;
            }

        }

        if (fromFigureCoord !== -1 && toFigureCoord !== -1) {
            this.boardDrawer.drawMoving(fromFigureCoord, toFigureCoord, () => this.showFiguresOnBoard(newBoardState));
        } else {
            this.showFiguresOnBoard(newBoardState);
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