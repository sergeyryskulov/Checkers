class BoardDrawer {

    public setFlipClickHandler(callback) {
        $('.flip').click(callback);
    }

    public setNewGameClickHandler(callback) {
        $('.newGame').click(callback);
    }

    public  drawSquares(isFlipped, width) {
        $('.board').html('');
        let divSquare = '<div  id=s$coord class="square $color"></div>';

        for (let coord = 0; coord < width*width; coord++) {
            $('.board').append(divSquare.replace('$coord', '' + (isFlipped ? width * width-1 - coord : coord)).replace('$color', this.isBlackSquareAt(coord, width) ? 'black' : 'white'));
        }
    }


    public drawFigure(coord, figure) {

        let divFigure = '<div id=f$coord class=figure>$figure</div>';
        $('#s' + coord).html(divFigure.replace('$coord', '' + coord).replace('$figure',
            this.gettChessSymbol(figure)));

        $('.figure').draggable();
    }

    public setDropFigureOnSquareHandler(dropCallback) {
        $('.square').droppable({
            drop: function (event, ui) {
                let fromCoord = ui.draggable.attr('id').substring(1);
                let toCoord = this.id.substring(1);
                dropCallback(fromCoord, toCoord);
            }
        });
    }

    private isBlackSquareAt(coord: number, width : number): boolean {
        return ((coord % width + Math.floor(coord / width)) % 2) !== 0;

    }

    private gettChessSymbol(figure: string): string {
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
}