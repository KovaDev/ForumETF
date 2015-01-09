$(document).ready(function () {

    $("#markdown").wysiwyg();
    //var commentForm = $("#create-comment-form");

    //$.ajaxSetup({ cache: false });

    window.setTimeout(function () {
        $("#alert_message").fadeTo(500, 0).slideUp(500, function () {
            $(this).remove();
        });
    }, 3000);

    $("#new-comment-link").click(function (event) {

        event.preventDefault();
        
        //var content = $("#commentTextarea").text();

        $("#comment-form-wrapper").slideToggle("fast");
        
        //if (commentForm.css("display") == "none") {
        //    commentForm.slideToggle("fast");
        //} else {
        //    commentForm.slideToggle("fast");
        //}
    });

    $("#markdown").keyup(function () {
        if ($("#markdown").html() == "") {
            $("#answer-submit").prop("disabled", true)
        } else if ($("#markdown").html() != "") {
            $("#answer-submit").prop("disabled", false)
        }
    });

    $("#answer-submit").bind("click", function () {
        $("#answer-hidden").val($("#markdown").html().replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;'));
    });


    //$("#createPostForm").bootstrapValidator({
    //    message: 'This value is not valid',
    //    feedbackIcons: {
    //        valid: 'glyphicon glyphicon-ok',
    //        invalid: 'glyphicon glyphicon-remove',
    //        validating: 'glyphicon glyphicon-refresh'
    //    },
    //    fields: {
    //        Title: {
    //            message: 'The username is not valid',
    //            validators: {
    //                notEmpty: {
    //                    message: 'Polje je obavezno!'
    //                },
    //            }
    //        },
    //        SelectedId: {
    //            validators: {
    //                notEmpty: {
    //                    message: 'Morate izabrati neku od ponuđenih kategorija!'
    //                }
    //            }
    //        },
    //        Content: {
    //            validators: {
    //                notEmpty: {
    //                    message: 'Polje je obavezno!'
    //                },
    //            }
    //        }
    //    }

    //});

});