$(function () {
    checkWindowWidth();

    $("#navBar li").click(function () {
        var idToScroll = checkNavbarId(this.id);
        scrollToId(idToScroll);
    });
});

function checkWindowWidth() {
    var width = $(window).width();

    if (width < 700) {
        $("#pageTitleName").text("PA");
    }
}

function checkNavbarId(id) {
    var clickedId = "";

    if (id === "pageTitleName")
        clickedId = "#aboutPage";
    else
        clickedId = "#" + id + "Page";

    return clickedId;
}

function scrollToId(id) {
    $('html, body').animate({
        scrollTop: $(id).offset().top
    }, 1000);
}