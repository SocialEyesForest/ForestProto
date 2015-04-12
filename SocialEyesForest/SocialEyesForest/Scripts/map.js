$(function () {

    alert("lol");

    var $window = $(window),
        $body = $('body')

    // Menu.
    var $menu = $('#menu'),
        $menuClose = $('<a class="close">').appendTo($menu),
        $menuToggle = $('.menuToggle');

        // Move to end of body.
        $menu.appendTo($body);

        // Close.
        $menuClose
            .on('click touchend', function (event) {
                event.preventDefault();
                event.stopPropagation();

                $body.removeClass('is-menu-visible');
            });

        // Toggle.
        $menuToggle
            .on('click touchend', function (event) {
                event.preventDefault();
                event.stopPropagation();

                $body.toggleClass('is-menu-visible');
            });

        // Wrapper.
        $body
            .on('click touchend', function (event) {
                if ($body.hasClass('is-menu-visible')) {
                    event.preventDefault();
                    event.stopPropagation();

                    $body.removeClass('is-menu-visible');
                }
            });
});