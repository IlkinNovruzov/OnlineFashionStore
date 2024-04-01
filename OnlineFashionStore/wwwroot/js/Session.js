// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
//const buttons = document.querySelectorAll('.js-add-cart');
//buttons.forEach(button => {
//    button.addEventListener('click', () => {
//        alert('clicked.');
//    });
//});

//$('.pc__category').each(function () {
//    $(this).click(function () {
//        var productName = $(this).text();
//        $.post('ShoppingCart/AddToCart', { productName: productName }, function () {
//            alert('Product added to cart' + productName);
//        });
//    });
//});
//$(document).ready(function () {
//    $(".pc__category").each(function () {
//        $(this).click(function () {
//            $.ajax({
//                url: '/ShoppingCart/AddToSession', // AJAX isteğinin gideceği adres
//                type: 'POST', // POST isteği yapılacak
//                success: function (response) {
//                    alert('Session değeri başarıyla eklendi:', response);
//                },
//                error: function () {
//                    alert('İsteğin işlenmesi sırasında bir hata .');
//                }
//            });
//        });
//    });
//});
$(document).ready(function () {
    $(".test").each(function () {
        $(this).click(function () {
            $.ajax({
                url: '/Shop/AddBrand',
                type: 'POST',
                data: { Bname="Wishon" },
                success: function (response) {
                    alert('başarıyla tamamlandı:', response);
                },
                error: function () {
                    alert('İşlem sırasında bir hata oluştu:');
                }
            });


        });
    });
});
