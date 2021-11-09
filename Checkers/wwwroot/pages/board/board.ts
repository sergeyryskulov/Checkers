var $;

class Board {

    
    private map;
    private isFlipped = false;

    private boardRepository: BoardRepository;
    private boardDrawer : BoardDrawer;

    initBoard() {
        this.boardRepository = new BoardRepository();
        this.boardDrawer = new BoardDrawer();

        this.boardRepository.register((data) => {

            this.startGame();

            this.boardDrawer.setFlipHandler(() => this.flipBoard());

            this.boardDrawer.setNewGameHabdler(() => this.boardRepository.newGame((data) => this.showFigures(data)));

        });
    }


    private startGame() {

        this.map = new Array(64);

        this.boardDrawer.addSquares(this.isFlipped, (fromCoord, toCoord) => {
            this.moveFigure(fromCoord, toCoord);
            this.boardRepository.moveFigureServer(fromCoord, toCoord, (data) => this.showFigures(data));

        });

        this.boardRepository.getFigures((data) => this.showFigures(data));
    }


    private flipBoard() {
        this.isFlipped = !this.isFlipped;
        this.startGame();
    }

    private setDraggable() {
        $('.figure').draggable({
                start: function() {
                   // that.isDragging = true;
                }
            }
        );
    }

   

    private moveFigure(fromCoord, toCoord) {
        let figure = this.map[fromCoord];
        this.showFigureAt(fromCoord, '1');
        this.showFigureAt(toCoord, figure);
    }
    private showFigures(figures) {
        
        for (let coord = 0; coord < 64; coord++) {
            this.showFigureAt(coord, figures[coord]);
        }
    }


    private  gettChessSymbol(figure : string)  :string{
        switch (figure) {
            case 'K': return '&#9812;';
            case 'Q': return '&#9813;';
            case 'R': return '&#9814;';
            case 'B': return '&#9815;';
            case 'N': return '&#9816;';
            case 'P': return '&#9817;';
            case 'k': return '&#9818;';
            case 'q': return '&#9819;';
            case 'r': return '&#9820;';
            case 'b': return '&#9821;';
            case 'n': return '&#9822;';
            case 'p': return '&#9823;';
            case 'k': return '&#9824;';
            default:
        }
        return '';
    }
    private showFigureAt(coord: number, figure: string) {
        if (this.map[coord] === figure) {
            return;
        }
        this.map[coord] = figure;
        let divFigure = '<div id=f$coord class=figure>$figure</div>';
        $('#s' + coord).html(divFigure.replace('$coord', '' + coord).replace('$figure',
            this.gettChessSymbol(figure)));

        this.setDraggable();
    }

   
}