class BoardDrawer {

    public setNewGameClickHandler(callback) {
        $('.button__new').click(callback);
    }

    public drawSquares(width) {
        let boardWidth = Math.min(document.documentElement.clientWidth, document.documentElement.clientHeight) - 100;

        $('.board__inner').width(boardWidth);
        $('.board__inner').height(boardWidth);

        $('.board__inner').html('');
        let divSquare = '<div  id=s$coord class="square $color"></div>';

        for (let coord = 0; coord < width*width; coord++) {
            $('.board__inner').append(divSquare.replace('$coord', '' + coord).replace('$color', this.isBlackSquareAt(coord, width) ? 'black' : 'white'));
        }
    }

    public drawMoving(fromCoord, toCoord, onComplete) {

        $('#f' + fromCoord).css('position', 'relative');
        $('#f' + fromCoord).css('z-index', 100);

        let leftShift = $('#s' + toCoord).position().left - $('#s' + fromCoord).position().left;
        let topShift = $('#s' + toCoord).position().top - $('#s' + fromCoord).position().top;

        $('#f' + fromCoord).animate({
            left: leftShift,
            top: topShift,
        }, 500,
            onComplete);
        
    }

    public drawFigure(coord, figure) {

        let divFigure = '<div id=f$coord class="figure figure_$figure"></div>';
        $('#s' + coord).html(divFigure.replace('$coord', '' + coord).replace('$figure', figure));

        if (figure == 'P' || figure == 'Q') {
            $('#s' + coord +' .figure').mousedown(function() {
                $(this).css('cursor', 'url(/pages/board/grab.cur), move');
            }).mouseup(function() {
                $(this).css('cursor', 'pointer');
            }).mouseleave(function() {
                $(this).css('cursor', 'pointer');
            }).draggable({
                start: function() {
                    $(this).css('cursor', 'url(/pages/board/grab.cur), move');
                }
            });
        }

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
}