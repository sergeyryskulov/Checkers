class BoardDrawer {

    public setNewGameClickHandler(callback) {
        $('.button__new').click(callback);
    }

    public drawSquares(width) {
        let boardWidth = Math.min(document.documentElement.clientWidth, document.documentElement.clientHeight) - 100;

        $('.board__inner').width(boardWidth);
        $('.board__inner').height(boardWidth);
        $('.board__inner').html(new Square().getSquaresHtml(width));

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

        let divFigure = `<div id=f${coord} class="figure figure_${figure}"></div>`;

        $('#s' + coord).html(divFigure);

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

   
}