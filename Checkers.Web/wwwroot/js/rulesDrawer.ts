class RulesDrawer {
    public drawRules() {
        $('.rules__dialog').show();
        $('.rules__cover').show();
        $('.rules__close').click(function () {
            $('.rules__cover').hide();
            $('.rules__dialog').hide();
        });
    }
}