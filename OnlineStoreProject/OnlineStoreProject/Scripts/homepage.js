$(document).ready(function () {
    var bestsellers = $("#bestsellers");
    var topScored = $("#topScored");
    var bestsellersCarousel = bestsellers.children(".carousel");
    var topScoredCarousel = topScored.children(".carousel");

    var renderProductsByPoints = function (response) {
        var result = "";
        result += renderCarousel(response);
        if (result) { topScored.find("h3").removeClass('hidden'); }
        topScoredCarousel.eq(0)[0].innerHTML = result;
    };

    var renderProductsByPurchase = function (response) {
        var result = "";
        result += renderCarousel(response);
        if (result) { bestsellers.find("h3").removeClass('hidden'); }
        bestsellersCarousel.eq(0)[0].innerHTML = result;
        sliderInit();
    }

    function renderCarousel(data) {
        var output = "";
        if (data.length == 0) {
            output += "<p>No products yet</p>";
        } else {
            for (var i = 0; i < data.length; i++) {
                p = data[i];
                output += "<div class='list_item'><a href='/Product/Details/"+p.ProductId+"'>";
                output += "<p class='product_name'>" + p.Name + "</p>";
                output += "<img src='" + p.ImagePath.substring(1) + "' alt='" + p.Name + "'>";
                output += "<p class='product_price'>Price: $" + p.Price + "</p>";
                output += "<a href='/OrderItem/Create/" + p.ProductId + "'><div class='btn btn-success btn-md purchase_button'>";
                output += "<i class='glyphicon glyphicon-shopping-cart'></i>Purchase</div></a>";
                output += "</a></div>";
            }
        }
        return output;
    }

    function getProductsByPoints() {
        $.ajax({
            method: "GET",
            url: "/Product/GetProductsInOrderByPoints",
            contentType: "json",
            success: renderProductsByPoints,
            error: displayError
        })
    }

    function getProductsByPurchase() {
        $.ajax({
            method: "GET",
            url: "/Product/GetProductsInOrderByPurchase",
            contentType: "json",
            success: renderProductsByPurchase,
            error: displayError
        })
    }


    function displayError() {
        alert('Error occured!');
    }

        function sliderInit() {
        var carouselFields = $(".carousel");
        for (var i = 0; i < carouselFields.length; i++) {
            carouselFields.eq(i).slick({
                autoplay: true,
                slidesToShow: 3,
                arrows: true,
                autoplaySpeed: 2000,
                responsive: [
                    {
                        breakpoint: 992,
                        settings: {
                            arrows: true,
                            centerMode: true,
                            centerPadding: '40px',
                            slidesToShow: 1
                        }
                    },
                    {
                        breakpoint: 768,
                        settings: {
                            arrows: false,
                            centerMode: true,
                            centerPadding: '40px',
                            slidesToShow: 1
                        }
                    }
                ]
            });
        }
    }

    getProductsByPoints();
    getProductsByPurchase();
    
   
})