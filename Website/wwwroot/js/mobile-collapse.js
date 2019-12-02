$(document).ready(function () {

    $('.mobile-collapse .mobile-collapse-header').on('click', function (e) {
        var container = $(this).closest('.mobile-collapse');
        var groupContainer = container.closest('.mobile-collapse-group');
        if (container.hasClass('show')) {
            container.removeClass('show');
        } else {
            if (groupContainer.length) {
                groupContainer.find('.mobile-collapse').removeClass('show');
            }
            container.addClass('show');
        }
    });

});