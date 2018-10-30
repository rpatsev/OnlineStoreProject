$(document).ready(function () {



    var markField = $("#vote_badge");
    var prodid = $(".product_img_wrapper").attr("data-attr");
    function GetAveragePoints() {
        $.ajax({
            type: "GET",
            url: "/Feedback/GetProductAverageMark/"+prodid,
            success: function (response) {
                if (response > 0) {
                    markField.append("<div class='mark_badge'>" + (1 * response).toFixed(1) + "</div>");
                }
            },
            error: function () { console.log("Error occured"); }
        })
    }

    GetAveragePoints();
})