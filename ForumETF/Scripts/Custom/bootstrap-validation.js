$(document).ready(function (event) {

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
            SelectedId: {
                validators: {
                    notEmpty: {
                        message: 'Morate izabrati neku od ponuđenih kategorija!'
                    }
                }
            },
            Content: {
                validators: {
                    notEmpty: {
                        message: 'Polje je obavezno!'
                    },
                }
            }
        }
    });

    //$("#comment-form").bootstrapValidator({
    //    message: 'This value is not valid',
    //    feedbackIcons: {
    //        valid: 'glyphicon glyphicon-ok',
    //        invalid: 'glyphicon glyphicon-remove',
    //        validating: 'glyphicon glyphicon-refresh'
    //    },
    //    fields: {
    //        commentContent: {
    //            validators: {
    //                notEmpty: {
    //                    message: 'Polje je obavezno!'
    //                },
    //            }
    //        }
    //    }
    //});



});