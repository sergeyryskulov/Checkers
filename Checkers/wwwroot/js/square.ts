﻿class Square {
    public getSquaresHtml(width : number ) {
        let result = '';
        for (let coord = 0; coord < width * width; coord++) {
            let currentSquareColor = this.isBlackSquareAt(coord, width) ? 'black' : 'white';
            let currentSquareHtml = `<div id=s${coord} class="square square_${currentSquareColor}" ></div>`;

            result += currentSquareHtml;
        }

        return result;
    }

    private isBlackSquareAt(coord: number, width: number): boolean {
        return ((coord % width + Math.floor(coord / width)) % 2) !== 0;

    }
}