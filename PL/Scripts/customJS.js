$(document).ready(function () {

    $('button#nav_button_login').click(function () {
        location.href = window.location.origin + '/Account/Login';
    });
    $('button#nav_button_register').click(function () {
        location.href = window.location.origin + "/Account/Register";
    });
    $('button#add').click(function () {
        location.href = window.location.origin + "/Friend/AddToFriend/" + $(this).val();
    });
    $('button#remove').click(function () {
        location.href = window.location.origin + "/Friend/RemoveFriend/" + $(this).val();
    });

    $("button#newMessage").click(function () {
        location.href = window.location.origin + "/Message/GetMessages/" + $(this).val();
    });

    $("#messageSubmit").bind('click',function(){   
        setTimeout("$('textarea').val('')",100);
        $("#text").focus();
    });

    $(".block_message").click(function () {
        location.href = window.location.origin + "/Message/BlockMessage/" + $(this).val();
    });
});



