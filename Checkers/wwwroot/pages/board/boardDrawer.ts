class BoardDrawer {

    public setFlipClickHandler(callback) {
        $('.flip').click(callback);
    }

    public setNewGameClickHandler(callback) {
        $('.newGame').click(callback);
    }

    public drawSquares(width) {
        $('.board').html('');
        let divSquare = '<div  id=s$coord class="square $color"></div>';

        for (let coord = 0; coord < width*width; coord++) {
            $('.board').append(divSquare.replace('$coord', '' + coord).replace('$color', this.isBlackSquareAt(coord, width) ? 'black' : 'white'));
        }
    }

    public drawMoving(fromCoord, toCoord, onComplete) {

        $('#f' + fromCoord).css('position', 'relative');

        let leftShift = $('#s' + toCoord).position().left - $('#s' + fromCoord).position().left;
        let topShift = $('#s' + toCoord).position().top - $('#s' + fromCoord).position().top;

        $('#f' + fromCoord).animate({
            left: leftShift,
            top: topShift,
        }, 300,
            onComplete);
        
    }

    public drawFigure(coord, figure) {

        let divFigure = '<div id=f$coord class="figure figure_$figure"></div>';
        $('#s' + coord).html(divFigure.replace('$coord', '' + coord).replace('$figure', figure));

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
        case 'Q': return '&#9813;';
        case 'P': return '&#9817;';
        case 'p': return '&#9823;';
        
        default:
        }
        return '';
    }
}