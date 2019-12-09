window.onload = function () {
    let x = document.getElementById("toggle");
    let option = document.getElementsByName("categoryId")[0];
    option.value = getCookie("category");
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
    option.onchange = function () {
        document.cookie = "category =" + this.value + ";";
        document.getElementById('form').submit();
    };
};

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}