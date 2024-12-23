$(document).ready(function () {
    $("#userPrifleBtn").on("click", function () {
        $.ajax({
            url: "/Home/UserPrfile",
            type: "Get",

            success: function (response) {
                $("#PartialBody").html(response);

            }
        });
    });


});