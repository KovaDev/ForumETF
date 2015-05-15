$(document).ready(function () {

    tinyMCE.init({
        // General options
        mode: "textareas",
        theme: "modern",
        // Theme options
        theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect",
        theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
        theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
        theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,spellchecker,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,blockquote,pagebreak,|,insertfile,insertimage",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_statusbar_location: "bottom",
        theme_advanced_resizing: true,

        // Example content CSS (should be your site CSS)
        content_css: "css/Site.css"
    });

 
    //var $myForm = $("#comment-form");
    //$myForm.submit(function () {
    //    //$("textarea[name=commentContent]").val("");
        
    //});

    $("#comment-form").bind("ajax:complete", function () {
        $("textarea[name=commentContent]").val("");
    });


    $("#post-wysiwyg").wysiwyg();
    //var commentForm = $("#create-comment-form");

    //$.ajaxSetup({ cache: false });

    $("#posts-table btn").click(function (e) {
        e.preventDefault();
        alert("poruka");
        return false;
    });

    $(document).on("click", "#taster", function (e) {
        e.preventDefault();
        alert("sfsffs");
        return false;
    });

    $(document).on("click", ".edit-btn", function (e) {
        e.preventDefault();
        alert("sfsffs");
        return false;
    });

    var post = null;

    $(document).on("click", ".delete-btn", function (e) {
        e.preventDefault();

        post = $(this).attr("id");
        //alert(post);
        return false;
    });

    $(document).on("click", "#mySubmit", function (e) {
        e.preventDefault();

        $.ajax({
            type: "Post",
            url: "/Post/Delete",
            cache: false,
            async: false,
            //dataType: 'text/html',
            data: { postId: post },
            success: function (data) {
                $("#user-posts-count").html(data.num_of_posts);

                var alert = "<div class=\"col-md-12\" id=\"alert-wrapper\"><div class=\"alert alert-dismissable alert-success\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">×</button><p>Post je uspješno obrisan!</p></div></div>";
                
                $(alert).insertAfter(".panel-body");
            }
        });
        
        $("#deleteModal").modal("hide");
        //return false;
    });


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
        if ($("#markdown").html() === "") {
            $("#answer-submit").prop("disabled", true);
        } else if ($("#markdown").html() !== "") {
            $("#answer-submit").prop("disabled", false);
        }
    });

    $("#answer-submit").bind("click", function () {
        $("#answer-hidden").val($("#markdown").html().replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;'));
    });


    /* Kreiranje posta */
    //var editor = $("#post-rich-editor");

    //$(editor).keyup(function () {
    //    if ($(editor).html() === "") {
    //        $("#answer-submit").prop("disabled", true);
    //    } else if ($(editor).html() !== "") {
    //        $("#answer-submit").prop("disabled", false);
    //    }
    //});

    //$("#answer-submit").bind("click", function () {
    //    $("#answer-hidden").val($("#markdown").html().replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;'));
    //});

    //function bindValueOnHiddenField() {
        
    //}

    //function getRichEditorContent(selector) {
    //    return $(selector).html().replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
    //}

    $("#post-save").bind("click", function () {
        $("#Content").val($("#post-wysiwyg").html().replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;'));
    });

    //$("#createPostForm").submit(function (event) {
    //    //alert("Handler for .submit() called.");
    //    event.preventDefault();
    //});

    //$("#post-create-submit").bind("click", function () {
    //    $("#post-content-hidden").val(getRichEditorContent(editor));
    //});

});


function initializeTinyMCE() {
    
}