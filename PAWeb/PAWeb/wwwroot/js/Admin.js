var allProjects = [];

$(function () {
    getAllProjects();

    // JQuery OnClick functionality for calling AJAX functions

    $("#projectSubmitButton").click(function () {
        addProject();
    });

    $(document).on("click", ".projectButton", function (e) {
        e.preventDefault();

        var buttonText = $(this).text();
        var projectId = $(this).val();

        if (buttonText === "Remove")
            removeProject(projectId);
        else
            displayProjectToEdit(projectId);
    });

    $(document).on("click", "#editFormSubmitButton", function (e) {
        e.preventDefault();

        var projectId = $(this).val();

        editProject(projectId);
    });

    $(document).on("click", "#saveImageBtn", function (e) {
        var id = $(this).attr('value');

        var data = new FormData();
        var files = $("#projectImage").get(0).files;
        var fileName = id + ".jpg";

        data.append('image', files[0], fileName);
        editProfileImage(data);
    });

});

// AJAX Call functions to server.

function editProfileImage(data) {
    alert(data.fileName);
    $.ajax({
        url: "api/admin/image",
        method: "PUT",
        contentType: false,
        processData: false,
        data: data
    })
        .done(function (image) {
            console.log(image);
            //imageButtonAnimation("fileUploadSuccessfull", "fa-check");
        })
        .fail(function (xhr, status, error) {
            //imageButtonAnimation("fileUploadFail", "fa-times");
            console.log(xhr, status, error);
        });
}

function getAllProjects() {
    $.ajax({
        url: "api/home/getAllProjects",
        method: "GET"
    })
        .done(function (allProjectsFromDb) {
            console.log(allProjectsFromDb);
            var html = "";
            allProjectsFromDb.forEach(function (project) {
                html += displayAllProjectsAdmin(project);
            });
            $("#projectsList").html(html);
            allProjects = allProjectsFromDb;
        })
        .fail(function (xhr, status, error) {
            console.log(xhr, status, error);
        });
}

function addProject() {
    $.ajax({
        url: 'api/admin/addproject',
        method: 'POST',
        data: {
            "Title": $("#addProjectForm [name=projectTitle]").val(),
            "Description": $("#addProjectForm [name=projectDescription]").val(),
            "GithubUrl": $("#addProjectForm [name=gitUrl]").val()
            //"ImageUrl": $("#addProjectForm [name=projectImageUrl]").val()
        }
    })
        .done(function (result) {
            console.log("Project added!");
        })
        .fail(function (xhr, status, error) {
            console.log(xhr, status, error);
        });
}

function removeProject(id) {
    $.ajax({
        url: 'api/admin/removeproject',
        method: 'DELETE',
        data: { id }
    })
        .done(function (result) {
            location.reload(true);
            console.log(result);
        })
        .fail(function (xhr, status, error) {
            console.log(xhr, status, error);
        });
}

function editProject(id) {
    $.ajax({
        url: 'api/admin/editproject',
        method: 'PUT',
        data: {
            id: id,
            "Title": $("#editForm [name=projectTitle]").val(),
            "Description": $("#editForm [name=projectDescription]").val(),
            "GithubUrl": $("#editForm [name=gitUrl]").val()
        }
    })
        .done(function (result) {
            location.reload(true);
            console.log(result);
        })
        .fail(function (xhr, status, error) {
            console.log(xhr, status, error);
        });
}

// Display functions for Admin page.

function displayAllProjectsAdmin(project) {
    var html = '<br /><li>' + project.title + '</li>';
    html += '<button class="projectButton" value="' + project.id + '">Remove</button>';
    html += '<button class="projectButton" value="' + project.id + '">Edit</button><br />';
    console.log(project.id);
    return html;
}

function displayProjectToEdit(id) {
    var projectToEdit = getProjectById(id);

    var html = '<h3>Edit project</h3><br/>';
    html += '<form>';
    html += '<input type="text" name="projectTitle" placeholder="Project title" /><br/><br/>';
    html += '<textarea name="projectDescription" placeholder="Description"></textarea><br/><br/>';
    html += '<input type="file" value="Upload Image" id="projectImage"><br/><br/>';
    html += '<a href="#" id="saveImageBtn" value="' + projectToEdit.id + '">Save image</a><br/><br/>';
    html += '<input type="text" name="gitUrl" placeholder="Git Url"/><br/><br/>';
    html += '<button type="submit" id="editFormSubmitButton" value="' + projectToEdit.id + '">Save Changes</button><br/><br/>';
    html += '</form>';

    $("#editForm").html(html);

    populateEditForm(projectToEdit);
}

function populateEditForm(project) {
    alert("hello" + project.title);
    $("#editForm [name=projectTitle]").val(project.title);
    $("#editForm [name=projectDescription]").val(project.description);
    $("#editForm [name=gitUrl]").val(project.gitUrl);
}

// Other class functionality

function getProjectById(id) {
    var project = $.grep(allProjects, function (e) { return e.id == id; });
    return project[0];
}