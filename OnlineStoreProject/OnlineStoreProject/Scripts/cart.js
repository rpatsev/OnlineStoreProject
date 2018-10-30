$(document).ready(function () {
    if (!navigator.cookieEnabled) {
        alert('Please, turn cookies on for convenient work in our website');
    }

    var sumField = $("#total_sum");
    var items = $(".order_item");
    var itemsAmount = $(".items_amount");
    var ordersCookieName = "orders";

    function refreshSelectItems() {
        var orderCookie = JSON.parse(getCookie(ordersCookieName));
        for (var i = 0; i < items.length; i++) {
            var currentOrderItemId = items.eq(i).find(".product_id").val();
            items.eq(i).find(".items_amount").val(orderCookie[currentOrderItemId]);        
        }
    }

    function calculateTotalSum() {
        var sum = 0;
        for (var i = 0; i < items.length; i++) {
            var amount = items.eq(i).find(".items_amount").val();
            var price = items.eq(i).find(".product_price").val();
            sum += price * amount;
        }
        sumField.eq(0)[0].innerHTML = sum.toFixed(2);
    }; 

    for (var i = 0; i < itemsAmount.length; i++) {
        itemsAmount.eq(i).trigger("change");
        itemsAmount.eq(i).change(calculateTotalSum);
        itemsAmount.eq(i).change(setItemsAmountToCookie);
    }

    function setItemsAmountToCookie() {
        console.log($(this));
        var amount = parseInt($(this).val());
        var currentOrderItemId = $(this).parents(".order_item").find(".product_id").val();
        var orderCookie = JSON.parse(getCookie(ordersCookieName));
        orderCookie[currentOrderItemId] = amount;
        document.cookie = ordersCookieName + "=" + JSON.stringify(orderCookie);     
    }

    function getCookie(name) {
        var matches = document.cookie.match(new RegExp(
            "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
        ));
        return matches ? decodeURIComponent(matches[1]) : undefined;
    }

    refreshSelectItems();
    calculateTotalSum();
})