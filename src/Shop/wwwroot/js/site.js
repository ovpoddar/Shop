window.onload = function () {
    let x = document.getElementById("toggle");
    x.onclick = function ()
    {
        let y = x.getElementsByTagName("div")[1];
        if (y.className == "dropdown-menu")
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
    let t = document.getElementById("suggesstion");
    t.innerHTML = "";
    for (let i = 0; i < e.length; i++) {
        let s = document.createElement("span");
        s.append(e[i].name);
        t.appendChild(s);
    }
    let s = t.querySelectorAll("span");
    for (let i = 0; i < s.length; i++) {
        s[i].addEventListener("dblclick", function () {
            document.getElementById("suggst").value = this.innerHTML;
            t.innerHTML = "";
        });
    }
}
function call() {
    $.ajax({
        url: "http://localhost:59616/api/Suggestion/" + this.value,
        success: function (result) {
            addsuggestion(result);
        }
    });
}