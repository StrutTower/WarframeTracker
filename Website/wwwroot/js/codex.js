$(document).ready(function () {

    $('#codex-filter-dropdown').on('change', function (e) {
        var value = $(this).val();
        var option = $(this).find(':selected');
        if (value === 'all') {
            $('.codex-item-container').show();
        } else {
            var showSelector = '.codex-item-container[data-' + value + '="' + option.data('show') + '"]';
            var hideSelector = '.codex-item-container[data-' + value + '="' + option.data('hide') + '"]';
            $(showSelector).show();
            $(hideSelector).hide();
        }
    });
});