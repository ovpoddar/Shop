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