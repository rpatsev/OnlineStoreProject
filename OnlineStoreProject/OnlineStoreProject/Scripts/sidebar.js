$(document).ready(function () {
    var sidebar = $("#sidebar");
    var renderSidebar = function (response) {
        var result = "";
        if (response.length > 0) {
            result += "<ul>";
            for (var i = 0; i < response.length; i++) {
                result += "<a href='/Product/ProductGroup/" + response[i].CategoryId + "'><li class='category_item'>" + response[i].Name + "</li></a>"
            }
            result += "</ul>";
        }
        else {
            result += "<p>No categories set</p>"
        }
        sidebar.eq(0)[0].innerHTML = result;
    }

    function getCategories() {
        $.ajax({
            url: "/Category/DisplayCategories",
            method: "GET",
            dataType: "json",
            success: renderSidebar
        });

    };
    if (window.location.pathname != "/Category/") {
        getCategories();
    }
})