var RulesDrawer = /** @class */ (function () {
    function RulesDrawer() {
    }
    RulesDrawer.prototype.drawRules = function () {
        $('.rules__dialog').show();
        $('.rules__close').click(function () {
            $('.rules__dialog').hide();
        });
    };
    return RulesDrawer;
}());
//# sourceMappingURL=rulesDrawer.js.map