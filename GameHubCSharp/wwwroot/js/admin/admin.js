

function searchedElement() {
    let type = document.getElementById("category").value;
    let id = document.getElementById("searchInput").value;

    fetch(`https://localhost:44348/findToDelete?id=${id}&type=${type}`)
        .then(response => response.json())
        .then(data => {
            console.log(data)
            let url = `/Admin/DeletePost/${data.id}?deleteType=${type}`;
            document.getElementById("searched").innerHTML = "<table class='table table - dark container mt - 4' >" +
                "<thead>" +
                "< tr >" +
                " <th scope='col'>id</th>" +
                " <th scope='col'>function</th>" +
                " </tr >" +
                "</thead >" +
                "< tbody > " +
                "<tr>" +
                " <th scope='row'></th>" +
                " <td>data.id</td>" +
                ` <td><button id='logout-button' class='bg-danger text-white p-2' onclick='location.href =${url} '>Delete</button></td>` +
                " </tr> </tbody></table>"
        });
}

function displyFilter(val) {
    console.log(val)
    if (val === "User") {
        document.getElementById("g").style.display = "none";
        document.getElementById("u").style.display = "table";
        document.getElementById("ge").style.display = "none";
        document.getElementById("c").style.display = "none";
        document.getElementById("p").style.display = "none";

    }

    if (val === "GameEvent") {
        document.getElementById("g").style.display = "none";
        document.getElementById("ge").style.display = "table";
        document.getElementById("u").style.display = "none";
        document.getElementById("c").style.display = "none";
        document.getElementById("p").style.display = "none";
    }

    if (val === "Game") {
        document.getElementById("g").style.display = "table";
        document.getElementById("ge").style.display = "none";
        document.getElementById("u").style.display = "none";
        document.getElementById("c").style.display = "none";
        document.getElementById("p").style.display = "none";
    }
    if (val === "Category") {
        document.getElementById("g").style.display = "none";
        document.getElementById("ge").style.display = "none";
        document.getElementById("u").style.display = "none";
        document.getElementById("c").style.display = "table";
        document.getElementById("p").style.display = "none";
    }
    if (val === "Post") {
        document.getElementById("g").style.display = "none";
        document.getElementById("ge").style.display = "none";
        document.getElementById("u").style.display = "none";
        document.getElementById("c").style.display = "none";
        document.getElementById("p").style.display = "table";
    }
}
function formFilter(val) {
    console.log(val)
    if (val === "Game") {
        document.getElementById("game").style.display = "inline-block";
        document.getElementById("post").style.display = "none";
        document.getElementById("cat").style.display = "none";
    }

    if (val === "Post") {
        document.getElementById("game").style.display = "none";
        document.getElementById("post").style.display = "inline-block";
        document.getElementById("cat").style.display = "none";
    }

    if (val === "Category") {
        document.getElementById("game").style.display = "none";
        document.getElementById("post").style.display = "none";
        document.getElementById("cat").style.display = "inline-block";
    }
}