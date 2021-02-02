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

connection.on("ReceiveNotfication", function (user) {
    console.log("im here");
    document.getElementById("counter").innerText = Number(document.getElementById("counter").innerText)+1;
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    connection.invoke("SendNotificationTo", user).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
