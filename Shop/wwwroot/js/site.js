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
    try {
        let p = document.getElementById("p");
        p.addEventListener("click", async function () {
            await storeInLoacal();
            if (allItem.length != 0 && document.cookie.length != 1) {
                location.replace("Payment/index");
            }
        });
    }
    catch (ex) {

    }
    try {
        let q = document.getElementById("pdp");
        q.addEventListener("keyup", function () {
            var Pamnt = 0;
            for (var i = 0; i < allItem.length; i++) {
                Pamnt += allItem[i]["total"];
            }
            if (Pamnt < this.value || Pamnt == this.value) {
                document.getElementById("change").innerHTML = (this.value - Pamnt);
                let pay = document.getElementById("pay");
                pay.innerHTML = "<button class=\"btn-primary btn btn-success\" id=\"pay\">Payment Now</button>";
            }
            else {
                document.getElementById("change").innerHTML = "";
            }
        });
        
    }
    catch (ex) {
    }
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

function Headers(obj) {
    this.Build = Getall();

    function Getall() {
        var keys = [], values = [];
        for (var name in obj) {
            keys.push(name);
        }
        for (var name in keys) {
            var objn, objval;
            objn = '"' + keys[name] + '"', objval = '"' + obj[keys[name]] + '"';
            values.push(objn + ", " + objval);
        }
        return values;
    }
}

function Request() {
    if (typeof arguments[0] !== "string") {
        this.url = arguments[1].url
    } else {
        if (arguments[0] == null || arguments[0] == undefined) {
            throw "undefined"
        } else {
            this.url = arguments[0]
        }
    }
    if (arguments[1].credentials == null || arguments[1].credentials == undefined) {
        this.credentials = "same-origin"
    } else {
        this.credentials = arguments[1].credentials
    }
    if (arguments[1].method == null || arguments[1].method == undefined) {
        this.method = "Get"
    } else {
        this.method = arguments[1].method
    }

    if (arguments[1].mode == null || arguments[1].mode == undefined) {
        this.mode = "cors"
    } else {
        this.mode = arguments[1].mode
    }
    this.headers = arguments[1].headers,
        this.body = arguments[1].body
}

function HttpDo() {
    var url, type, headers, parameter, callback, self = this;
    args = arguments[0];
    if (typeof args == "string") {
        url = args,
            type = "Get",
            headers = "";
        callback = arguments[1]
    }
    else if (typeof args == "object") {
        url = args.url,
            type = args.method,
            headers = args.headers.Build,
            parameter = args.body;
        callback = arguments[1]

    }
    var respnce = httpCall(url, type, headers, parameter, callback);
    return respnce;
}

function httpCall(url, type, headers, parameter, callback) {
    request = new XMLHttpRequest();
    request.open(type, url);

    for (var i = 0; i < headers.length; i++) {
        var data = headers[i].replace(/"/g, "").split(",");
        request.setRequestHeader(data[0], data[1]);
    }


    if (parameter == undefined || parameter == null) {
        request.send();
    }
    else {
        request.send(parameter);
    }
    request.onreadystatechange = function () {
        if (200 == request.status && 2 == request.readyState && "OK" == request.statusText) {
            try {
                request.hasOwnProperty("DONE"), request.onload = function () {
                    if (typeof callback == "function") {
                        callback(request.responseText);
                    }
                    else {
                        throw new TypeError("callback is not working");
                    }
                };

            }
            catch (ex) {
                if (typeof callback == "function") {
                    callback(request.responseText);
                }
                else {
                    throw new TypeError("callback is not working");
                }
            }
        }
        else if (500 == request.status && 4 == request.readyState && "Internal Server Error" == request.statusText) {
            setInterval(httpCall(e, t, n), 5e3)
        }
        else {
            return false;
        }
    }

}

function storeInLoacal() {
    for (var i = 0; i < allItem.length; i++) {
        document.cookie = i+"=" + allItem[i].quantityI.value + "+" + allItem[i].id + ";expires=Wed, 18 Dec 2023 12:00:00 GMT&quot;";
    }
    
}

function element() {
    var t, e = document.createElement(arguments[0]);
    for (t = 1; t < arguments.length; t++)t % 2 && e.setAttribute(arguments[t], arguments[t + 1]);
    return e
}

function totalcount() {
    var con = document.getElementById("Total");
    var text = 0;

    for (var i = 0; i < allItem.length; i++) {
        text += allItem[i]["total"];
    }
    con.innerHTML = text;
}