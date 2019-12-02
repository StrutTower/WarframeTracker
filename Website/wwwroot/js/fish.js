$(document).ready(function () {

    $('#fish-biome-filter').on('change', function (e) {
        var value = $(this).val();
        if (value === undefined || value.length < 1) {
            $('.fish-details-container').show();
        } else {
            $('.fish-details-container').hide();
            $('.fish-details-container[data-biome*="' + value + '"]').show();
        }
    });
});