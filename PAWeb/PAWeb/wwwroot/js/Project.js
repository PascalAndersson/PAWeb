$(function () {
    getAllProjects();
});

function getAllProjects() {
    $.ajax({
        url: "api/home/getAllProjects",
        method: "GET"
    })
        .done(function (allProjects) {
            console.log(allProjects);
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
    var image = checkIfImageUrlIsNull(project.imageUrl);

    var html = '<div class="row projectWrapper">';
    html += '<div class="col-md-7">';
    html += '<p class="projectTitle">' + project.title + '.</p>';
    html += '<p class="projectDescription">' + project.description + "</p>";
    html += '</div>';
    html += '<div class="col-md-4 projectImage">' + image + '</div>';
    html += '</div>';

    return html;
}

function checkIfImageUrlIsNull(image) {
    //console.log(image);
    var imageUrl = "";

    if (imageUrl !== null) {
        console.log("img url is not null");
        return imageUrl;
    }
    else {
        console.log("imageUrl is null...");
        imageUrl = "imageUrl is null...";
    }
}