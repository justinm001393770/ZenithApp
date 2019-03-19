//NavSearch Scroll BEGIN
var prevScrollpos = window.pageYOffset;
var myHideBar;

//$('.profile-picture-container').mousemove(function (e) {
//    var amountMovedX = (e.pageX * -1 / 6);
//    var amountMovedY = (e.pageY * -1 / 6);
//    $('.profile-picture-container').css('background-position', amountMovedX + 'px ' + amountMovedY + 'px');
//});

$(document).ready(function () {
    $(".game-image-container-x").toggleClass("to_top");
    window.setInterval(function () {
        $(".game-image-container-x").toggleClass("to_top");
    }, 5500);
});

window.onscroll = function () {
    var currentScrollPos = window.pageYOffset;
    if (prevScrollpos > currentScrollPos) {
        document.getElementById("navbar").style.opacity = 1;
        document.getElementById("searchbar").style.opacity = 1;
        $(".sticky-container").stop().animate({ "marginTop": ($(window).scrollTop()) + "px", "marginLeft": ($(window).scrollLeft()) + "px" }, "fast");

    } else {
        document.getElementById("navbar").style.opacity = 0;
        document.getElementById("searchbar").style.opacity = 0;
        $(".sticky-container").stop().animate({ "marginTop": ($(window).scrollTop()-53) + "px", "marginLeft": ($(window).scrollLeft()) + "px" }, "fast");
    }
    prevScrollpos = currentScrollPos;
}
//NavSearch Scroll END

//showBar BEGIN
function showBar() {
    clearTimeout(myHideBar);
    document.getElementById("navbar").style.opacity = 1;
    document.getElementById("searchbar").style.opacity = 1;
}
//showBar END

//hideBar BEGIN
function hideBar() {
    myHideBar = setTimeout(function () {
        var currentScrollPos = window.pageYOffset;
        if (currentScrollPos > window.pageXOffset + 87) {
            document.getElementById("navbar").style.opacity = 0;
            document.getElementById("searchbar").style.opacity = 0;
        }
    }, 1000);
}
//hideBar END

//Scroll to Top BEGIN
$(window).scroll(function () {
    var height = $(window).scrollTop();
    $('#toTop').css("visibility", "visible");
    if (height > 100) {
        $('#toTop').fadeIn();
    } else {
        $('#toTop').fadeOut();
    }
});
$(document).ready(function () {
    $("#toTop").click(function (event) {
        event.preventDefault();
        $("html, body").animate({ scrollTop: 0 }, "slow");
        return false;
    });

});
//Scroll to Top END

function pageLoaded() {
    $('#loadScreen').fadeOut();
    $('#html').css("overflow-y", "scroll");
}

$(document).ready(function () {
    $(this).scrollTop(0);
});

function homeLoaded() {
    $('#loadScreen').fadeOut();
}

function sendMsg(text) {
    var textBox = document.getElementById('message');

    if (textBox.value.trim().length > 0) {

        $.ajax({
            type: "POST",
            url: "/Chat/SendMessage",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ chatText: textBox.value }),
            dataType: "json",
            success: function (data) {
                self.result(data.Message);
            },
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                self.result(errorThrown);
            }
        });

        textBox.value = "";
        var objDiv = document.getElementById("chatbox");
        objDiv.scrollTop = objDiv.scrollHeight;
        setInterval(updateScroll, 1);
    }
}

function updateScroll() {
    var element = document.getElementById("chatbox");
    element.scrollTop = element.scrollHeight;
}