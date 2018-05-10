// Write your JavaScript code.
$(".add-to-cart").click(function(e) {
    sweetAlert("Book has been added to your cart!", "", "success")
});
$(".remove-from-cart").click(function(e) {
    sweetAlert("Book has been removed from your cart!", "", "success")
});
$(".remove-from-wishlist").click(function(e) {
    sweetAlert("Book has been removed from your wishlist!", "", "success")
});