$(document).ready(function () {

    var codexDropdown = $('#codex-filter-dropdown');

    if (codexDropdown.length) {
        var section = codexDropdown.data('codex-section');
        var filter = sessionStorage.getItem('codexFilter' + section);
        codexDropdown.val(filter);
        filterChange(filter);

        $('#codex-filter-dropdown').on('change', function (e) {
            var value = $(this).val();
            sessionStorage.setItem('codexFilter' + section, value);
            filterChange(value);
        });
    }


    function filterChange(filterType) {
        var hideSelector;
        var showSelector = '.codex-item-container';
        switch (filterType) {
            case 'showMastered':
                showSelector = '.codex-item-container[data-mastered="True"]';
                hideSelector = '.codex-item-container[data-mastered="False"]';
                break;
            case 'showNotMastered':
                showSelector = '.codex-item-container[data-mastered="False"]';
                hideSelector = '.codex-item-container[data-mastered="True"]';
                break;
            case 'showAcquired':
                showSelector = '.codex-item-container[data-acquired="True"]';
                hideSelector = '.codex-item-container[data-acquired="False"]';
                break;
            case 'showNotAcquired':
                showSelector = '.codex-item-container[data-acquired="False"]';
                hideSelector = '.codex-item-container[data-acquired="True"]';
                break;
            case 'showVaulted':
                showSelector = '.codex-item-container[data-vaulted="True"]';
                hideSelector = '.codex-item-container[data-vaulted="False"]';
                break;
            case 'showNotVaulted':
                showSelector = '.codex-item-container[data-vaulted="False"]';
                hideSelector = '.codex-item-container[data-vaulted="True"]';
                break;
        }
        $('#codex-filter-text-group').hide();

        $(showSelector).show();
        $(hideSelector).hide();
    }
});