$(".add-to-cart").click(function(e) {
    sweetAlert("Book has been added to your cart!", "", "success")
});
$(".remove-from-cart").click(function(e) {
    sweetAlert("Book has been removed from your cart!", "", "success")
});
$(".remove-from-wishlist").click(function(e) {
    sweetAlert("Book has been removed from your wishlist!", "", "success")
});
$(".add-to-wishlist").click(function(e) {
    sweetAlert("Book has been added to your wishlist!", "", "success")
});
$(".add-to-favorite").click(function(e) {
    sweetAlert("Book has been set as your favorite!", "", "success")
});
$(".confirm-order").click(function(e) {
    sweetAlert("You order is complete!", "", "success")
});
$(".ship-order").click(function(e) {
    sweetAlert("Order has been shipped!", "", "success")
});