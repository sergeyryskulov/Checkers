class RulesDrawer {
    public drawRules() {
        $('.rules__dialog').show();
        $('.rules__close').click(function() {
            $('.rules__dialog').hide();
        });
    }
}