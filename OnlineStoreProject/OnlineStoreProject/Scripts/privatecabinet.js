$(document).ready(function () {
    var personalFeedbacksField = $("#client_feedbacks");
    var personalOrdersField = $("#client_orders");

    var renderPersonalOrdersSection = function (response) {
        var output = "<h3 class='text-center'>Orders</h3>";
        if (response.length < 1) {
            output += "<p>No orders made yet. <a href='/'>Let's go shopping!!!</a></p>";
        } else {
            response.map(function (i) {
                output += "<div class='panel panel-success user_order_item' data-attr='" + i.OrderId + "'>";
                output += "<div class='panel-heading'>Order#" + i.OrderId + "<div class='btn btn-warning pull-right order_details_button'>More</div></div>";
                output += "<div class='clearfix'></div>";
                output += "<div class='panel-body'>";
                output += "<p>Sum: $" + i.Sum + "</p>";
                output += "<p>Order Date: " + new Date(parseInt(i.CreatedAt.substring(6))).toLocaleDateString() + "</p>";
                output += "<div class='order_details'></div>";
                output += "</div>";
                output += "</div>";
            });
        }        
        personalOrdersField.eq(0)[0].innerHTML = output;
    }

    var renderPersonalFeedbacksSection = function (response) {
        var output = "<h3 class='text-center'>Feedbacks</h3>";
        if (response.length < 1) {
            output += "<p>No feedbacks left yet</p>";
        } else {
            response.map(function (i) {
                output += "<div class='panel panel-success'>";
                output += "<div class='panel-heading'>";
                output += "<a href ='/Product/Details/" + i.Product.ProductId + "'>" + i.Product.Name + "</a>";
                output += "<p class='pull-right'>"+ renderRating(i.Points) +"</p>";
                output += "</div>";
                output += "<div class='panel-body'>";
                output += "<div class='row'><div class='col-xs-3'>";
                output += "<img class='feedback_product_img' src ='" + i.Product.ImagePath.substring(1) + "' alt = '" + i.Product.Name + "'/>";
                output += "</div>";
                output += "<div class='col-xs-9'>";
                output += "<p>" + i.Text + "</p>";
                output += "<p>Date: " + new Date(parseInt(i.CreatedAt.substring(6))).toLocaleDateString() + "</p>";
                output += "</div></div></div>";
                output += "</div>";
            })
        }

        personalFeedbacksField.eq(0)[0].innerHTML = output;
    }

    var renderOrderDetails = function (response) {
        var output = "";
        for (var i = 0; i < response.length; i++) {
            var r = response[i];
            output += "<div class='row'><div class='col-xs-3'>";
            output += "<img  class='order_product_img' src='" + response[i].Product.ImagePath.substring(1) + "' alt='" + response[i].Product.Name + "' />";
            output += "</div><div class='col-xs-9'>";
            output += "<p class=user_order_item_name'>Product: <a href = '/Product/Details/" + r.Product.ProductId + "'>" + r.Product.Name + "</a></p>";
            output += "<div class ='sum_details'>";
            output += "<span>Sum: $" + (response[i].Sum / response[i].Amount) + "</span>";
            output += "<span> &times; " + response[i].Amount + "</span>";
            output += "<span>= $" + response[i].Sum + "</span>";
            output += "</div></div></div>";
            output += "<hr/>";
        }
        var currentOrderDetailsField = $("#client_orders").find(".user_order_item[data-attr = '" + response[0].OrderId + "']").find(".order_details");
        currentOrderDetailsField.eq(0)[0].innerHTML = output;
        currentOrderDetailsField.slideToggle();
    }

    function renderRating(points) {
        var maxPoints = 5;
        var result = "<ul class='user_rating'>";
        var pointsFilled = Math.floor(points);
        for (var i = 0; i < pointsFilled; i++) {
            result += "</li><i class='glyphicon glyphicon-star'></i></li>";
        }
        for (var i = 0; i < (maxPoints - pointsFilled); i++) {
            result += "</li><i class='glyphicon glyphicon-star-empty'></i></li>";
        }
        result += "</ul>";
        return result;
    }

    function getUserFeedbacks() {
        $.ajax({
            method: "GET",
            url: "/PrivateCabinet/GetFeedbacksByUser",
            contentType: "application/json",
            success: renderPersonalFeedbacksSection,
            error: displayError
        })
    }

    function getUserOrders() {
        $.ajax({
            method: "GET",
            url: "/PrivateCabinet/GetOrdersByUser",
            contentType: "application/json",
            success: renderPersonalOrdersSection,
            error: displayError
        })
    }

    function showOrderDetails() {
        var orderId = $(this).parents(".user_order_item").attr("data-attr");
        $(this).toggleClass('clicked');
        $(this).eq(0)[0].innerHTML = ($(this).hasClass('clicked')) ? "Less" : "More";
        $.ajax({
            method: "GET",
            url: "/Admin/Order/Details/" + orderId,
            dataType: "json",
            success: renderOrderDetails,
            error: displayError
        })  
    }

    personalOrdersField.on("click", ".order_details_button", showOrderDetails)

    getUserFeedbacks();
    getUserOrders();

    function displayError() {
        alert("Error occured!");
    }
})