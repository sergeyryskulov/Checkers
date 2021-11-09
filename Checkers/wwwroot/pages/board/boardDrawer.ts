class BoardDrawer {

    public setFlipHandler(callback) {
        $('.flip').click(callback);
    }

    public setNewGameHabdler(callback) {
        $('.newGame').click(callback);
    }

    public  addSquares(isFlipped, dropCallback) {
        $('.board').html('');
        let divSquare = '<div  id=s$coord class="square $color"></div>';

        for (let coord = 0; coord < 64; coord++) {
            $('.board').append(divSquare.replace('$coord', '' + (isFlipped ? 63 - coord : coord)).replace('$color', this.isBlackSquareAt(coord) ? 'black' : 'white'));
        }
        this.setDroppable(dropCallback);
    }

    private isBlackSquareAt(coord: number): boolean {
        return ((coord % 8 + Math.floor(coord / 8)) % 2) !== 0;

    }

    private setDroppable(dropCallback) {
        let that = this;
        $('.square').droppable({
            drop: function (event, ui) {
                let fromCoord = ui.draggable.attr('id').substring(1);
                let toCoord = this.id.substring(1);

                dropCallback(fromCoord, toCoord);

             //   that.moveFigure(fromCoord, toCoord);
               // that.boardRepository.moveFigureServer(fromCoord, toCoord, (data) => that.showFigures(data));
             
            }
        });
    }
}