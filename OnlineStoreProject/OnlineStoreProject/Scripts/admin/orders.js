$(document).ready(function () {
    var orderDetailsField = ".order_details";


    $(".order").on('click', showOrderDetails);

    var renderOrderDetails = function (response) {
        var output = "";
        for (var i = 0; i < response.length; i++) {
            var r = response[i];
            output += "<div class='row'><div class='col-xs-3'>";
            output += "<img  class='product_img' src='" + response[i].Product.ImagePath.substring(1) + "' alt='" + response[i].Product.Name + "' />";
            output += "</div><div class='col-xs-9'>";
            output += "<p class='product_name'>Product: <a href = '/Product/Details/" + r.Product.ProductId + "'>" + r.Product.Name + "</a></p>";
            output += "<div class ='sum_details'>";
            output += "<span>Sum: $" + (response[i].Sum / response[i].Amount) + "</span>";
            output += "<span> &times; " + response[i].Amount + "</span>";
            output += "<span>= $" + response[i].Sum + "</span>";
            output += "</div></div></div>";
            output += "<hr/>";
        }
        var currentOrderDetailsField = $("#orders_list").find(".order[data-attr = '" + response[0].OrderId + "']").find(orderDetailsField);
        currentOrderDetailsField.eq(0)[0].innerHTML = output;
        currentOrderDetailsField.slideToggle();
    }

    function showOrderDetails() {
        var prodid = $(this).find(".order_id").val();
        $.ajax({
            method: "GET",
            url: "/Admin/Order/Details/" + prodid,
            dataType: "json",
            success: renderOrderDetails,
            error: displayError
        })  
    }

    function deleteOrder(id) {
        var _order = {
            OrderId: id,
        };
        console.log($(this));
        $.ajax({
            method: "POST",
            url: "/Admin/Order/Remove/",
            data: _order,
            success: function () {
                alert('order deleted')
            },
            error: displayError
        })
    }

    $(".delete_order").on('click', function (e) {
        e.stopPropagation();

        var confirmMessage = 'Order #' + $(this).parents(".order").attr("data-attr") + ' will be removed forever!!!'
        var orderId = $(this).parents(".order").attr("data-attr");
        if (confirm(confirmMessage)) {
            deleteOrder(orderId);
            location.reload();
        }
    })

    function displayError() {
        alert('Error occured');
    }
})