$(document).ready(function () {
    var ordersCookieName = "orders";
    var ordersCookie = getCookie(ordersCookieName)
    if (ordersCookie != null) {
        var ordersJson = JSON.parse(ordersCookie);
        var ordersCount = length(ordersJson);
        if (ordersCount > 0){
            $("#cartMenuItem").append("<span class='badge'>" +ordersCount +"</span>")
        }
    }

    function getCookie(name) {
        var matches = document.cookie.match(new RegExp(
            "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
        ));
        return matches ? decodeURIComponent(matches[1]) : undefined;
    }

    function length(obj) {
        return Object.keys(obj).length;
    }

})