$(document).ready(function () {
    $('.sidebar-toggler').on('click', function (e) {
        e.preventDefault();
        $('#left-sidebar').toggleClass('show');
    });

    $('.sidebar-underlay').on('click', function (e) {
        e.preventDefault();
        $('#left-sidebar').removeClass('show');
    })
});