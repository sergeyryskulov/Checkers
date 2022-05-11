class FigureDrawer {

    public drawMoving(fromCoord : number, toCoord : number, onComplete) {

        $('#f' + fromCoord).css('position', 'relative');
        $('#f' + fromCoord).css('z-index', 100);

        let leftShift = $('#s' + toCoord).position().left - $('#s' + fromCoord).position().left;
        let topShift = $('#s' + toCoord).position().top - $('#s' + fromCoord).position().top;

        $('#f' + fromCoord).animate({
                left: leftShift,
                top: topShift,
            },
            500,
            onComplete);

    }

    public getHtml(coord : number, figure : string) {

        return `<div id=f${coord} class="figure figure_${figure}"></div>`;

    }

    public setHandlers(coord: number, figure: string) {

        if (figure == 'P' || figure == 'Q') {
            $('#f' + coord).mousedown(function () {
                $(this).css('cursor', 'url(/images/cursorGrab.cur), move');
            }).mouseup(function () {
                $(this).css('cursor', 'pointer');
            }).mouseleave(function () {
                $(this).css('cursor', 'pointer');
            }).draggable({
                start: function () {
                    $(this).css('cursor', 'url(/images/cursorGrab.cur), move');
                }
            });
        }
    }
}