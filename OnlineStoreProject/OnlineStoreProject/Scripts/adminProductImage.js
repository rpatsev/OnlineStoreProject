$(document).ready(function () {
    var productSelectBox = $("#productId");
    var imageField = $("#product_img");
    productSelectBox.trigger("change");
    productSelectBox.change(changePhoto);
    changePhoto();

    function changePhoto() {
        var prodid = productSelectBox.val();
        $.ajax({
            type: "GET",
            url: "/Admin/Product/GetProductData/" + prodid,
            dataType: "json",
            success: function (response) {
                if (response.ImagePath != null) {
                    var imgLocation = response.ImagePath.substring(1);
                    imageField.eq(0)[0].innerHTML = "<img src='" + imgLocation + "'>";
                } else {
                    imageField.eq(0)[0].innerHTML = "<img src='/Files/notfound.png'>";
                }
            }
        })
    };

    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $("#image_upload_preview").attr("src", e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $("#inputImageField").change(function () {
        readURL(this);
    });
})
