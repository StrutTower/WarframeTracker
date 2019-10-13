$(document).ready(function () {

    $('.open-item-modal').on('click', function (e) {
        e.preventDefault();

        $.ajax({
            url: $(this).data('url'),
            cache: false,
            method: 'GET',
            success: function (response) {
                $('#item-form-modal-body').html(response);
                $('#item-form-modal').modal('show');
            },
            error: function (response) {
                notifySimple(response.responseJSON.Message, 'error');
                console.error('Failed to get modal view.<br/>' + response.responseJSON.Message, 'danger');
            }
        });
    });

    $('#item-form-modal').on('submit', '#item-acquisition-form', function (e) {
        e.preventDefault();
        $.ajax({
            url: $(this).attr('action'),
            cache: false,
            method: 'POST',
            data: $(this).serialize(),
            success: function (response) {
                Lobibox.notify('success', {
                    msg: 'Updated',
                    size: 'mini',
                    sound: false,
                    delay: 4000,
                    icon: false
                });
                $('#item-form-modal').modal('hide');
                var container = $('.codex-image-container[data-item-name="' + response.itemName + '"]');
                var symbol = container.find('.acquisition-symbol');
                symbol.html(response.view);
            },
            error: function (response) {
                notifySimple(response.responseJSON.Message, 'error');
                console.error('Failed to upate');
            }
        });
    });

    $('.open-item-details-modal').on('click', function (e) {
        e.preventDefault();

        $.ajax({
            url: $(this).data('url'),
            cache: false,
            method: 'GET',
            success: function (response) {
                $('#item-details-modal-body').html(response);
                $('#item-details-modal').modal('show');
            },
            error: function (response) {
                notifySimple(response.responseJSON.Message, 'error');
                console.error('Failed to get modal view.<br/>' + response.responseJSON.Message);
            }
        });
    });
});