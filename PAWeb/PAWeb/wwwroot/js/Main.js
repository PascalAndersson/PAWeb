
$(function () {
    $("#navBar li").click(function () {
        var idToScroll = checkNavbarId(this.id);
        scrollToId(idToScroll);
    });
});

function checkNavbarId(id) {
    var clickedId = "";

    if (id == "about" || id == "pageTitleName")
        clickedId = "#aboutPage";
    else if (id == "projects")
        clickedId = "#projectsPage";
    else if (id == "contact")
        clickedId = "#contactPage";
    return clickedId;
}

function scrollToId(id) {
    $('html, body').animate({
        scrollTop: $(id).offset().top
    }, 1000);
}