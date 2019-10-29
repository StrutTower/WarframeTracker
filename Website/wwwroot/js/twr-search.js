$(document).ready(function () {

    var searcher = $('.twr-searcher');

    var timeout;
    searcher.on('keyup', function (e) {
        if (timeout !== undefined) {
            clearTimeout(timeout);
        }
        timeout = setTimeout(function () { filter(searcher); }, 500);
    });

    $('.twr-search-clear').on('click', function (e) {
        searcher.val('');
        filter(searcher);
    });

    function filter(input) {
        var value = input.val();
        var min = input.data('min-length');
        var targetSelector = input.data('target');

        var targets = $(targetSelector);

        if (value.length === 0) {
            targets.show();
            return;
        }

        if (min !== undefined) {
            var minInt = parseInt(min);
            if (value.length < minInt) {
                return;
            }
        }


        targets.each(function () {
            var element = $(this);
            if (element.data('twr-search').toLowerCase().indexOf(value.toLowerCase()) > -1) {
                element.show();
            } else {
                element.hide();
            }
        });
    }
});