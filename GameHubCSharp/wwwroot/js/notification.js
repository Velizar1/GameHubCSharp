"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

//Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;



connection.start().then(function () {
    console.log("connected")
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});
//............................................................

connection.on("ReceiveNotfication", function (notifications) {
    console.log(notifications.length);
    console.log(notifications);
    document.getElementById("counter").innerText = notifications.length;
    for (var i = 0; i < notifications.length; i++) {
        document.getElementById("notification-div").innerHtml += `<a class="dropdown-item waves-effect waves-light" href="GameEven/GameEventDetail?id=${notifications[i].id}"><span class="badge badge-danger ml-2">${notifications[i].message}</span></a>`
    }
});

 function mafunc() {
    connection.invoke("SendNotificationTo", roomid).catch(function (err) {
        return console.error(err.toString());
    });
};
