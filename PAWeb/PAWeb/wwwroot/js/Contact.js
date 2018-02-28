$(function () {
    $("#contactFormButton").click(function () {
        sendEmail();
    });

    $("#email2").hide();
});

function sendEmail() {
    $.ajax({
        url: 'api/home/sendemail',
        method: 'POST',
        data: {
            "Sender": $("#contactForm [name=contactEmail]").val(),
            "CheckSpam": $("#contactForm [name=contactEmail2]").val(),
            "Subject": $("#contactForm [name=contactSubject]").val(),
            "Message": $("#contactForm [name=contactMessage]").val(),
            "Phone": $("#contactForm [name=contactPhone]").val()
        }
    })
        .done(function (result) {
            console.log(result);
            location.reload();
        })
        .fail(function (xhr, status, error) {
            console.log(xhr, status, error);
        });
}