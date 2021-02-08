"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationhub").build();

//Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;



connection.start().then(function () {
    console.log("connected")
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});
//............................................................

connection.on("ReceiveNotfication", function (obj) {

    //console.log(obj)
    document.getElementById("counter").innerText = obj.notCount;
    let not = obj.notifications;
    console.log(not)
    console.log(not.length)
    document.getElementById("nott").innerHTML=""
    for (let i = 0; i < not.length; i++)
    {
        console.log("in loop")
        document.getElementById("nott").innerHTML = document.getElementById("nott").innerHTML.concat(`<a class="dropdown-item waves-effect waves-light" href="/game/detail?id=${not[i].gameEventId}"><span class="badge badge-danger ml-2">${not[i].message}</span></a>`)
  
    }
    console.log("here")
});


connection.on("UpdateNotifications", function (obj) {

    //console.log(obj)
    document.getElementById("counter").innerText = 0;
    let not = obj.notifications;
    console.log(not)
    console.log(not.length)
    document.getElementById("nott").innerHTML = ""
    for (let i = 0; i < not.length; i++) {
        console.log("in loop")
        document.getElementById("nott").innerHTML = document.getElementById("nott").innerHTML.concat(`<a class="dropdown-item waves-effect waves-light" href="/game/detail?id=${not[i].gameEventId}"><span class="badge badge-danger ml-2">${not[i].message}</span></a>`)
        
    }
    console.log("here")
});
function updateCount(user) {
    connection.invoke("NotificationCount", user).catch(function (err) {
        return console.error(err.toString());
    });
}

function mafunc() {
    //$('#form1').submit(function () {
    //    this.submit();
    //     connection.invoke("SendNotificationTo", roomid).catch(function (err) {
    //    return console.error(err.toString());
    //});
    //    return false;
    //});

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