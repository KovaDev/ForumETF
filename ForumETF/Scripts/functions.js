
$(document).ready(function () {

    $("#markdown").wysiwyg();

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