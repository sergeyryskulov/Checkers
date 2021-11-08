var $;

class Board {

    
    private map;
    private isDragging = false;
    private isFlipped = false;
    
    initBoard() {
        this.start();

        setInterval(() => this.getFiguresServer(), 1000);
        //this.showFigures("rnbqkbnrpppppppp11111111111111111111111111111111PPPPPPPPRNBQKBNR");
        //this.setDraggable();
        $('.newGame').click(() => this.newGameServer());

        $('.flip').click(() => this.flipBoard());

    }

    private start() {


        this.map = new Array(64);
        this.addSquares();

        this.getFiguresServer();
    }


    private flipBoard() {
        this.isFlipped = !this.isFlipped;
        this.start();
    }

    

    private newGameServer() {
        $.post('/api/newGame', (data) => this.showFigures(data));
    }

    private getFiguresServer() {
        if (this.isDragging) {
            return;
        }
        $.post('/api/getfigures', (data)=> this.showFigures(data));
    }

    private setDraggable() {
        let that = this;
        $('.figure').draggable({
                start: function() {
                    that.isDragging = true;
                }
            }

            );
    }

    private setDroppable() {
        let that = this;
        $('.square').droppable({
            drop: function(event, ui) {
                let fromCoord = ui.draggable.attr('id').substring(1);
                let toCoord = this.id.substring(1);
                that.moveFigure(fromCoord, toCoord);
                that.moveFigureServer(fromCoord, toCoord);
                that.isDragging = false;
            }
        });
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
    private addSquares() {
        $('.board').html('');
        let divSquare = '<div  id=s$coord class="square $color"></div>';
        
        for (let coord = 0; coord < 64; coord++) {
            $('.board').append(divSquare.replace('$coord', '' + (this.isFlipped? 63 - coord : coord)).replace('$color', this.isBlackSquareAt(coord)? 'black' : 'white'));
        }
        this.setDroppable();
    }

    private isBlackSquareAt(coord: number): boolean {
        return ((coord % 8 + Math.floor(coord / 8)) % 2) !== 0;

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

    private moveFigureServer(fromCoord, toCoord) {

        $.post('/api/movefigure?fromCoord=' + fromCoord + '&toCoord=' + toCoord, (data) => this.showFigures(data));
    }
}