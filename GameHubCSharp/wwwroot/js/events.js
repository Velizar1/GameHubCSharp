"use strict";

//...........................................................

connection.on("UpdateEventList", function (obj) {
    var x = obj.gameEvents;
    var container = document.getElementById("container");
    container.innerHTML = "";
    for (var i = 0; i < x.length; i++) {

        var element = `<div class=" d-inline-block text-left mt-5 container-style" id="event-div" style="width: 18rem;">
            <img src="${x[i].imageUrl}"
                 class="card-img-top" style="height: 200px;" alt="${x[i].imageUrl}"; title="${x[i].imageUrl}">
            <div class="card-body">
                <h5 class="card-title">Owner's nick : ${x[i].ownerName} </h5>
                <p class="card-text">Devision : ${x[i].devision}</p>
                <p  class="card-text">Players needed : ${x[i].takenPlaces}</p>
                <p  class="card-text">Starts on : ${x[i].startDate}</p>
                <a href="/game/detail/?id=${x[i].id}" class="btn btn-primary border-0 bg-dark btn-lg">Details</a>
            </div>
        </div>`
        //console.log(x[i].id);
        container.innerHTML += element;
    }
});


$("#event-add").on('submit', function (e) {
    e.preventDefault();
    $.ajax({
        type: $(this).prop('method'),
        url: $(this).prop('action'),
        data: $(this).serialize()
    }).done(function () {
        connection.invoke("UpdateEvents").catch(function (err) {
            return console.error(err.toString());

        });
        window.location.replace("/");
    });
});