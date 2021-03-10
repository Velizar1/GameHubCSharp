"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationhub").build();

//Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;



connection.start().then(function () {
    console.log("connected")
   
}).catch(function (err) {
    return console.error(err.toString());
});
//............................................................

connection.on("ReceiveNotfication", function (obj) {

  
    document.getElementById("counter").innerText = obj.notCount;
    let not = obj.notifications;
    document.getElementById("nott").innerHTML = ""
    if (not.length == 0) {
        
        document.getElementById("nott").innerHTML = document.getElementById("nott").innerHTML.concat(`<p class="ml-2" style="color:white;margin-bottom:0">No notifications found</p>`)
    }
    for (let i = 0; i < not.length; i++) {
       
        document.getElementById("nott").innerHTML = document.getElementById("nott").innerHTML.concat(`<div style="word-wrap: break-word;">
        <p class="badge ml-2" style="color:white;margin-bottom:0px">${getTime((Math.abs(new Date() - new Date(not[i].createdAt))))}</p>
        <a style="text-decoration: none;" href="/game/detail?id=${not[i].gameEventId}"><p class="badge-danger ml-2" style=";border-radius: 3px;
    width:97%;font-size:15px;
    word-wrap:break-word;">${not[i].message}</p ></a ></div >`)

    }
});


connection.on("UpdateNotifications", function (obj) {

    document.getElementById("counter").innerText = 0;
    let not = obj.notifications;
   
    document.getElementById("nott").innerHTML = ""
    if (not.length == 0) {
       
        document.getElementById("nott").innerHTML = document.getElementById("nott").innerHTML.concat(`<p class="ml-2" style="color:white;margin-bottom:0">No notifications found</p>`)
    }
    for (let i = 0; i < not.length; i++) {
        
        document.getElementById("nott").innerHTML = document.getElementById("nott").innerHTML.concat(`<div style="word-wrap: break-word;">
        <p class="badge ml-2" style="color:white;margin-bottom:0px">${getTime((Math.abs(new Date() - new Date(not[i].createdAt))))}</p>
        <a style="text-decoration: none;" href="/game/detail?id=${not[i].gameEventId}"><p class="badge-danger ml-2" style=";border-radius: 3px;
    width:97%;font-size:15px;
    word-wrap:break-word;">${not[i].message}</p ></a ></div >`)

    }
   
});

function getTime(millisec) {
    var seconds = (millisec / 1000).toFixed(0);
    var minutes = Math.floor(seconds / 60);
    var hours = "";
    if (minutes > 59) {
        hours = Math.floor(minutes / 60);
        
        minutes = minutes - (hours * 60);
       
    }
    if (hours != "") {
        return "before " + hours + " hours";
    }
    return "before " + minutes + " min";
}

function updateCount(user) {
    connection.invoke("NotificationCount", user).catch(function (err) {
        return console.error(err.toString());
    });
}

function mafunc() {
  
    $("#form1").on('submit', function (e) {

        $.ajax({
            type: $(this).prop('method'),
            url: $(this).prop('action'),
            data: $(this).serialize()
        }).done(function () {
            connection.invoke("SendNotificationTo", roomid).catch(function (err) {
                return console.error(err.toString());
            });
        });
    });

};



$("#form1").on('submit', function (e) {
    e.preventDefault();
    $.ajax({
        type: $(this).prop('method'),
        url: $(this).prop('action'),
        data: $(this).serialize()
    }).done(function (obj) {
        console.log(obj);
        connection.invoke("SendNotificationTo", roomid).catch(function (err) {
            return console.error(err.toString());

        });

    });
    /* fetch($(this).prop('action'), {
         method: $(this).prop('method'),
         data: $(this).serialize()
 
     }).then(res => {
         
         connection.invoke("SendNotificationTo", roomid).catch(function (err) {
             return console.error(err.toString());
 
         });
         
         return res.json();
         //window.location.replace(res.headers.get('url'));
     }).then(data => console.log(data))*/
});