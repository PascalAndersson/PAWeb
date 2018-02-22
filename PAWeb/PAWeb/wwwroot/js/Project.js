$(function () {
    getAllProjects();
});

function getAllProjects() {
    $.ajax({
        url: "api/home/getAllProjects",
        method: "GET"
    })
        .done(function (allProjects) {
            var html = "";
            allProjects.forEach(function (project) {
                html += displayAllProjects(project);
            });
            $("#projectsContainer").html(html);
        })
        .fail(function (xhr, status, error) {
            console.log(xhr, status, error);
        });
}

function displayAllProjects(project) {
    //console.log(project.imageUrl);
    var image = checkIfImageUrlIsNull(project.imageUrl);

    var html = '<div class="row projectWrapper">';
    html += '<div class="col-md-7">';
    html += '<p class="projectTitle">' + project.title + '.</p>';
    html += '<p class="projectDescription">' + project.description + "</p>";
    html += '</div>';
    html += '<div class="col-md-4">';
    html += '<img class="projectImage" src="img/project_images/' + image + '" alt="Fuck.">';
    html += '</div >';
    html += '</div>';

    return html;
}

function checkIfImageUrlIsNull(image) {
    console.log(image);
    var imageUrl = "";

    if (image === null) {
        imageUrl = "default_img.png";
    }
    else {
        imageUrl = image;
    }
    return imageUrl;
}