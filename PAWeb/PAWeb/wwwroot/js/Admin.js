$(function () {
    getAllProjects();

    $("#projectSubmitButton").click(function () {
        addProject();
    });

    $(".removeProjectButton").on("click", function (e) {
        //e.PreventDefault();
        alert("hello");
    });

});


function getAllProjects() {
    $.ajax({
        url: "api/home/getAllProjects",
        method: "GET"
    })
        .done(function (allProjects) {
            var html = "";
            allProjects.forEach(function (project) {
                html += displayAllProjectsAdmin(project);
            });
            $("#projectsList").html(html);
        })
        .fail(function (xhr, status, error) {
            console.log(xhr, status, error);
        });
}

function displayAllProjectsAdmin(project) {
    var html = '<li>' + project.title + '</li>';
    html += '<button class="removeProjectButton">Remove project</button>'
    return html;
}


function addProject() {
    $.ajax({
        url: 'api/home/addproject',
        method: 'POST',
        data: {
            "Title": $("#addProjectForm [name=projectTitle]").val(),
            "Description": $("#addProjectForm [name=projectDescription]").val(),
            "GithubUrl": $("#addProjectForm [name=gitUrl]").val()
        }
    })
        .done(function (result) {
            console.log("Project added!");
        })
        .fail(function (xhr, status, error) {
            console.log(xhr, status, error);
        });
}