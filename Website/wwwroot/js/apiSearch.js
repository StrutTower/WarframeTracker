$(document).ready(function (e) {

    var apiSearcher = $('.api-searcher');
    if (apiSearcher.length) {
        var filter = apiSearcher.data('api-filter');
        if (filter === undefined || filter.length < 1) {
            filter = 'All';
        }
        var itemArray = [];
        $.getJSON('https://raw.githubusercontent.com/WFCD/warframe-items/development/data/json/' + filter + '.json', function (data) {
            $.each(data, function (i, v) {
                itemArray.push({
                    id: v.uniqueName,
                    text: v.name
                });
            });

            apiSearcher.select2({
                width: '100%',
                data: itemArray,
                templateResult: apiSearchFormatter
            });
        });

        
    }

    function apiSearchFormatter(item) {
        if (!item.id) {
            return item.text;
        }

        var $item = $('<div><div>' + item.text + '</div><div class="small text-muted">' + item.id + '</div>');

        return $item;
    }
});