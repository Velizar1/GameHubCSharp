﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $("#back-to-top").fadeIn();
        } else {
            $("#back-to-top").fadeOut();
        }
    });
    // scroll body to 0px on click
    $("#back-to-top").click(function () {
        $("body,html").animate(
            {
                scrollTop: 0
            },
            400
        );
        return false;
    });
});

function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}

function openUserMenu(){
    
    document.getElementById("userSidenav").style.width = "270px";
    var nodes = document.getElementById('userSidenav').getElementsByTagName("div");
    for (var i = 0; i < nodes.length; i++) {
        nodes[i].style.width = "250px";
    }
}
function closeUserMenu() {
    document.getElementById("userSidenav").style.width = "0";
}