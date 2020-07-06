window.onload = function () {
    var x = document.getElementById("toggle");
    x.onclick = function ()
    {
        var y = x.getElementsByTagName("div")[1];
        if (y.className === "dropdown-menu")
        {
            y.className = "d-block";
        }
        else
        {
            y.className = "dropdown-menu";
        }
    }
};
function getCookie(cname) {
    var name = cname + "=";
    var decodecookie = decodeURIComponent(document.cookie);
    var ca = decodecookie.split(";");
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == " ") {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
function addsuggestion(e) {
    var t = document.getElementById("suggesstion");
    t.innerHTML = "";
    for (let i = 0; i < e.length; i++) {
        var s = document.createElement("span");
        s.append(e[i].name);
        t.appendChild(s);
    }
    var s = t.querySelectorAll("span");
    for (let i = 0; i < s.length; i++) {
        s[i].addEventListener("dblclick", function () {
            document.getElementById("suggst").value = this.innerHTML;
            t.innerHTML = "";
        });
    }
}
function call() {
    var settings = {
        "url": "http://localhost:59616/api/Suggestion/" + this.value,
        "method": "GET",
        "timeout": 0,
        "headers": {
            "Authorization": "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJBbWFyIFBvZGRhciIsImVtYWlsIjoiYW1hcnBvZGRlcjBAZ21haWwuY29tIiwiZ2VuZGVyIjoiTWFsZSIsImV4cCI6MTU5NDA5MzA3NCwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFtYXJwb2RkYXIiLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjU5NjE2LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzUwLyJ9.cn3mkaTx9Yueat9RVOyJx7MBQasyXP3xhwmXaeVR9b9AW-mnPr3Cmj6hu2ESQ8W6zI5wO_MN5R6QpombZKimsg",
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            query: "",
            variables: {}
        })
    };
    $.ajax(settings).done(function (response) {
        addsuggestion(response);
    });
}