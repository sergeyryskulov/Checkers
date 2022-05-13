class GameDrawer {


    _squareDrawer: SquareDrawer;
    _figureDrawer: FigureDrawer;
    _boardDrawer: BoardDrawer;
    _rulesDrawer: RulesDrawer;

    constructor() {
        this._squareDrawer = new SquareDrawer();
        this._figureDrawer = new FigureDrawer();
        this._boardDrawer = new BoardDrawer();
        this._rulesDrawer = new RulesDrawer();
    }

    public setNewGameClickHandler(callback) {
        $('.button__new').click(callback);

        $('.button__rules').click(()=>this._rulesDrawer.drawRules());

    }

   
    public drawSquares(width) {
        let squaresHtml = this._squareDrawer.getSquaresHtml(width);

        this._boardDrawer.drawBoard(squaresHtml);
    }

    public drawMoving(fromCoord, toCoord, onComplete) {
        this._figureDrawer.drawMoving(fromCoord, toCoord, onComplete);
    }

    public drawFigure(coord, figure) {
        let figuresHtml = this._figureDrawer.getHtml(coord, figure);
        $('#s' + coord).html(figuresHtml);
        this._figureDrawer.setHandlers(coord, figure);
    }

    public setDropFigureOnSquareHandler(dropCallback) {
        this._squareDrawer.setDropFigureOnSquareHandler(dropCallback);
    }
}