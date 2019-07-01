$(document).ready(function () {

    $('.item-category-select2-list').select2({
        width: '100%'
    });

    $('.item-category-select2-list').on('select2:select', function (e) {
        var data = e.params.data;

        $(this).val('').trigger('change');

        var existing = $('[data-item-category-id="' + data.id + '"]');
        if (existing.length) {
            alert('Already selected');
            return;
        }

        var template = $('#item-category-template');
        var tbody = $('.item-category-table tbody');

        var index = parseInt(tbody.find('tr').last().data('index'));
        if (isNaN(index)) {
            index = 0;
        } else {
            index++;
        }

        var html = template.html();
        html = html.replace(/\[0\]/g, '[' + index + ']').replace(/_0__/g, '_' + index + '__');

        var $tr = $(html).first();
        $tr.find('.item-category-hidden-input').val(data.id);
        $tr.find('.item-category-name').text(data.text);
        $tr.attr('data-index', index).attr('data-item-category-id', data.id);

        tbody.append($tr);
    });

    $('.item-category-table').on('click', '.remove-item-category', function (e) {
        $(this).closest('tr').remove();
    });


    $('.warframe-item-searcher').select2({
        width: '100%',
        allowClear: true,
        placeholder: 'Search for an item.',
        minimumInputLength: 3,
        templateResult: formatWarframeItemResult,
        ajax: {
            url: $('.warframe-item-searcher').data('url'),
            dataType: 'JSON',
            delay: 500,
            data: function (params) {
                var query = {
                    q: params.term,
                    page: params.page || 1
                };
                return query;
            }
        }
    });

    function formatWarframeItemResult(item) {
        if (!item.id) {
            return item.text;
        }
        var html = '<div>';
        html += '<div>' + item.text + '</div>';
        html += '<div class="text-faded">' + item.id + '</div>';
        html += '</div>';

        var $item = $(html);
        return $item;
    }
});