var $;

class Game {

    private _serverRepository: ServerRepository;

    private _gameDrawer: GameDrawer;

    private _figuresCache : Array<string>;

    private _oldBoardState: BoardState;

    initGame() {
        this._serverRepository = new ServerRepository();

        this._gameDrawer = new GameDrawer();

        let defaultBoardState = new BoardState();
        defaultBoardState.cells = "1p1p1p1pp1p1p1p11p1p1p1p1111111111111111P1P1P1P11P1P1P1PP1P1P1P1";
        defaultBoardState.turn = 'w';
        defaultBoardState.mustGoFrom = null;

        if (window.location.href.indexOf('cells=') !== -1) {
            defaultBoardState.cells = window.location.href.split('cells=')[1].split('&')[0];
        }
        if (window.location.href.indexOf('turn=') !== -1) {
            defaultBoardState.turn = window.location.href.split('turn=')[1].split('&')[0];
        }

        this._gameDrawer.setNewGameClickHandler(() => this.showFiguresOnBoard(defaultBoardState));

        this.initBoard(defaultBoardState);       
    }

    private initBoard(defaultBoardState : BoardState) {

        this._figuresCache = new Array(defaultBoardState.cells.length);
        let lineSquareCount = Math.sqrt(this._figuresCache.length);

        this._gameDrawer.drawSquares(lineSquareCount);
        this._gameDrawer.setDropFigureOnSquareHandler((fromCoord, toCoord) => {
            this.moveFigureOnBoard(fromCoord, toCoord);
            this._serverRepository.moveFigureOnServer(this._oldBoardState, fromCoord, toCoord, (newBoardState : BoardState) => this.showFiguresOnBoard(newBoardState));
        });
        this.showFiguresOnBoard(defaultBoardState);
        
    }

    private moveFigureOnBoard(fromCoord, toCoord) {
        let figure = this._figuresCache[fromCoord];
        this.showFigureAt(fromCoord, '1');
        this.showFigureAt(toCoord, figure);
    }
   
    private showFiguresOnBoard(boardState : BoardState) {

        this._oldBoardState = boardState;

        console.log(boardState);
        
        for (let coord = 0; coord < boardState.cells.length; coord++) {
            this.showFigureAt(coord, boardState.cells[coord]);
        }

        if (boardState.turn === 'b') {
            this._serverRepository.intellectStep(this._oldBoardState, (newsBoardState) => this.intellectStepCalculated(newsBoardState));
        }
    }

    private intellectStepCalculated(newBoardState: BoardState) {

        let fromFigureCoord = -1;
        let toFigureCoord = -1;

        for (let coord = 0; coord < newBoardState.cells.length; coord++) {
            if (
                newBoardState.cells[coord] == '1' &&
                (this._figuresCache[coord] == 'p' || this._figuresCache[coord] == 'q')) {
                fromFigureCoord = coord;
            }
            if (
                (newBoardState.cells[coord] == 'p' || newBoardState.cells[coord] == 'q') && (this._figuresCache[coord] == '1')) {
                toFigureCoord = coord;
            }

        }

        if (fromFigureCoord !== -1 && toFigureCoord !== -1) {
            this._gameDrawer.drawMoving(fromFigureCoord, toFigureCoord, () => this.showFiguresOnBoard(newBoardState));
        } else {
            this.showFiguresOnBoard(newBoardState);
        }

    }

    private showFigureAt(coord: number, figure: string) {
        if (this._figuresCache[coord] === figure) {
            return;
        }
        this._figuresCache[coord] = figure;
        this._gameDrawer.drawFigure(coord, figure);
    }
}