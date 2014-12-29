
$(document).ready(function () {

    $("#markdown").wysiwyg();
    var commentForm = $("#create-comment-form");

    $("#new-comment-link").click(function (event) {

        event.preventDefault();
        
        var content = $("#comment-textarea").text();

        $("#create-comment-form").slideToggle("fast");
        
        //if (commentForm.css("display") == "none") {
        //    commentForm.slideToggle("fast");
        //} else {
        //    commentForm.slideToggle("fast");
        //}
    });


    //$(commentForm).submit(function (e) {
    //    $.ajax({
    //        url: '@Url.Action("Create", "Comment")',
    //        type: "Post",
    //        cache: false,
    //        async: true,
    //        dataType: 'json',
    //        data: { content: content },
    //        success: function (data) {
    //            alert(data);
    //        }
    //    });
    //    e.preventDefault();
    //    e.unbind();
    //});

    //$(commentForm).submit();




    $("#createPostForm").bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            Title: {
                message: 'The username is not valid',
                validators: {
                    notEmpty: {
                        message: 'Polje je obavezno!'
                    },
                }
            },
            Content: {
                validators: {
                    notEmpty: {
                        message: 'The email is required and cannot be empty'
                    },
                }
            }
        }

    });

});