$(function () {
    $("#contactFormButton").click(function () {
        sendEmail();
    });
});

function sendEmail(){
    $.ajax({
        url: "api/home/email",
        method: "POST",
        data: {
            "EmailAdress": $("#contactForm [name=contactEmail]"),
            "Subject": $("#contactForm [name=contactSubject]"),
            "Message": $("#contactForm [name=contactMessage]")
        }
    })
        .done(function (result) {
            console.log(result);
        })
        .fail(function (xhr, status, error) {
            console.log(xhr, status, error);
        });
}