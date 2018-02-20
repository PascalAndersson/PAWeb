$(function () {
    $("#navBar li").click(function () {
        var idToScroll = checkNavbarId(this.id);
        scrollToId(idToScroll);
    });
});

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