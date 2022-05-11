var $;

class Game {

    private _serverRepository: ServerRepository;

    private _gameDrawer: GameDrawer;

    private _figuresCache : Array<string>;

    initGame() {
        this._serverRepository = new ServerRepository();

        this._gameDrawer = new GameDrawer();

        let position = '';
        if (window.location.href.split('?pos=').length === 2) {
            position = window.location.href.split('?pos=')[1];
        }

        this._serverRepository.registerOnServer(position, () => {

            this._gameDrawer.setNewGameClickHandler(
                () => this._serverRepository.clearGameOnServer(
                    (clearedFigures) => this.showFiguresOnBoard(clearedFigures)));

            this.showBoard();
        });
    }

    private showBoard() {


        this._serverRepository.getFiguresFromServer((data) => {
            this._figuresCache = new Array(data.length - 1);
            let lineSquareCount = Math.sqrt(this._figuresCache.length);

            this._gameDrawer.drawSquares(lineSquareCount);
            this._gameDrawer.setDropFigureOnSquareHandler((fromCoord, toCoord) => {
                this.moveFigureOnBoard(fromCoord, toCoord);
                this._serverRepository.moveFigureOnServer(fromCoord, toCoord, (data) => this.showFiguresOnBoard(data));
            });
            this.showFiguresOnBoard(data);
        });
    }

    private moveFigureOnBoard(fromCoord, toCoord) {
        let figure = this._figuresCache[fromCoord];
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

        if (boardState[figuresLength] === 'b') {
            this._serverRepository.intellectStep((newsBoardState) => this.intellectStepCalculated(newsBoardState));
        }
    }

    private intellectStepCalculated(newBoardState: string) {

        let fromFigureCoord = -1;
        let toFigureCoord = -1;

        for (let coord = 0; coord < this.getFiguresLength(newBoardState); coord++) {
            if (
                newBoardState[coord] == '1' &&
                (this._figuresCache[coord] == 'p' || this._figuresCache[coord] == 'q')) {
                fromFigureCoord = coord;
            }
            if (
                (newBoardState[coord] == 'p' || newBoardState[coord] == 'q') && (this._figuresCache[coord] == '1')) {
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