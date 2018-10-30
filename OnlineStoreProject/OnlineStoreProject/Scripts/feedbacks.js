$(document).ready(function () {
    var currentUserId;
    getCurrentUserId();
    var unauthorizedUser = "Unknown user";
    var feedbacksField = $("#feedbacks_list");
    var maxPointsValue = 5;
    var minPointsValue = 1;
    var defaultPointValue = 4;
    var renderRatingPluginDelay = 50;
    var renderFeedbacks = function (response) {
        var result = "";
        if (response.length > 0) {
            result += "<div>";
            for (var i = 0; i < response.length; i++) {
                var panelClass = (currentUserId === response[i].UserId) ? "success" : "warning";
                result += "<div class='feedback panel panel-" + panelClass + "' data-attr='" + response[i].FeedbackId + "'>";
                result += "<div class ='panel panel-heading'>";
                result += ((response[i].Username != null) ? response[i].Username : unauthorizedUser) + "'s feedback";

                var controls = "";
                controls += "<div class = 'controls'>";
                controls += "<div class = 'btn edit_comment'><i class='glyphicon glyphicon-pencil'></i></div>";
                controls += "<div class = 'btn remove_comment'><i class='glyphicon glyphicon-remove'></i></div>";
                controls += "</div>";

                if (currentUserId === response[i].UserId) { result += controls; }
     
                result += "</div>";
                result += "<div class='panel panel-body'>";
                result += "<div class='editSection hidden'></div>";
                result += "<div class='feedbackText'>";
                result += (response[i].Text != null) ? response[i].Text : "";
                result += "</div>";
                result += "<div class='points'>";
                result += response[i].Points;
                result += "<span class='maxPoints'>/ " + maxPointsValue +"</span>";
                result += "<div class='rating'>";
                result += "<input type='hidden' name='val' value='" + response[i].Points + "'/>";
                result += "</div>";
                result += "<input type='hidden' id='user_id' name='user_id' value='" + response[i].UserId + "'/>"
                result += "</div></div></div>";
            }
            result += "</div>";           
        }
        else {
            result += "<em>You'll be the first visitor who leaves the comment here!</em>";
        }
        feedbacksField.eq(0)[0].innerHTML = result;
        setTimeout(renderRatings, renderRatingPluginDelay);
    }

    function getFeedbacks() {
        var prodid = $("#feedbacks").attr("data-attr");
        $.ajax({
            method: "GET",
            url: "/Feedback/GetByProduct/" + prodid,
            dataType: "json",
            success: renderFeedbacks,
            error: displayError
        });

    }

    function getCurrentUserId() {
        $.ajax({
            method: "GET",
            url: "/Account/GetCurrentUserId",
            success: function (response) {
                console.log("User ID received");
                currentUserId = response;
            }
        })
    }

    function getFeetbackData() {
        return feedbackData = {
            ProductId: $("#feedbacks").attr("data-attr"),
            text: $("#feedback_form").find("textarea").val(),
            points: $(".pointsSlider").slider("option", "value"),
        };
    }
    
    function postFeedback() {
        var feedbackData = getFeetbackData();
        $(this).parents("#feedback_form").find("textarea").val("");
        $.ajax({
            method: "POST",
            url: "/Feedback/Post",
            data: feedbackData,
            success: function () {
                alert("Successfully sent!");
                getFeedbacks();
                setTimeout(renderRatings, renderRatingPluginDelay);
            },
            error: displayError
        });
    }

    function openEditingSection() {
        var editField = $(this).parents(".feedback").find(".editSection");
        var textToEdit = editField.parents(".feedback").find(".feedbackText").eq(0)[0].innerHTML;
        var output = "<textarea>" + textToEdit + "</textarea>";
        output += "<div class='btn btn-info submitEditingComment'>";
        output += "<i class='glyphicon glyphicon-send'></i>Edit</div>";
        editField.eq(0)[0].innerHTML = output;
        editField.toggleClass("hidden");
    }

    function editFeedback() {
        var feedback = {
            FeedbackId: $(this).parents(".feedback").attr("data-attr"),
            Text: $(this).parents(".feedback").find(".editSection textarea").val(),
        }
        $.ajax({
            method: "POST",
            url: "/Feedback/Edit",
            data: feedback,
            success: function () {
                console.log(feedback.Text);
                alert('Successfully edited');
                getFeedbacks();
                setTimeout(renderRatings, renderRatingPluginDelay);
            },
            error: displayError
        })
    }

    function deleteFeedback() {
        var feedbackId = $(this).parents(".feedback").attr("data-attr");
        $.ajax({
            method: "POST",
            url: "/Feedback/Delete/" + feedbackId,
            success: function () {
                console.log("deleted");
                getFeedbacks();
                setTimeout(renderRatings, renderRatingPluginDelay);
            },
            error: displayError
        })
    }


    $("#sendFeedback").on("click", postFeedback);
    feedbacksField.on("click", ".edit_comment", openEditingSection);
    feedbacksField.on("click", ".submitEditingComment", editFeedback);
    feedbacksField.on("click", ".remove_comment", deleteFeedback);

    function displayError() {
        alert("Error occured");
    }

    function renderRatings() {
        var ratingField = $(".rating");
        for (var i = 0; i < ratingField.length; i++) {
            ratingField.eq(i).rating({
                readOnly: true,
                width: 20,
            });
        }
    }
    setTimeout(renderRatings, renderRatingPluginDelay);

    getFeedbacks();
    
    
    var handle = $("#custom-handle");
    $(".pointsSlider").slider({
        max: maxPointsValue,
        min: minPointsValue,
        value: defaultPointValue,
        create: function () {
            handle.text($(this).slider("value"));
        },
        slide: function (event,ui) {
            handle.text(ui.value);
        }
    });
})