$(document).ready(function () {
  var mouse_is_inside = false;
  $("#mobile_menu").click(function () {
    $(".outplay-leftSideMenu").toggleClass('show');
    $(".overlay").addClass('show');
  });
  $(".overlay").click(function () {
    $(".outplay-leftSideMenu").removeClass('show');
    $(".overlay").removeClass('show');
  });
  $("#profle_menu").click(function () {
    $(".Profile-menu").toggleClass('show');
  });
  $('#profle_menu').hover(function () {
    mouse_is_inside = true;
  }, function () {
    mouse_is_inside = false;
  });

  $("body").mouseup(function () {
    if (!mouse_is_inside) $('.Profile-menu').removeClass('show');
  });
});
