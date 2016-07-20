$(document).ready(function () {
    $(".search-btn").click(function (event) {
        $(".searchFormWrapper").slideToggle();
        event.stopPropagation();
    });
    $(window).click(function (e) {
        var $clicked = $(e.target);
        if (!$clicked.hasClass("searchFormWrapper") && !$clicked.parents().hasClass("searchFormWrapper"))
            $(".searchFormWrapper").slideUp();
    });

    $("#liLoginForm").fancybox({
        'hideOnContentClick': true
    });
    $('.nav-tabs a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    })

});