class Item {
    constructor(name, id, price, brand, totalPrice, quantity) {
        this.name = name,
        this.id = id,
        this.price = price,
        this.brand = brand,
        this.total = totalPrice,
        this.quantity = quantity,
        this.eml;
    }
    create() {
        this.eml = element("div", "class", "card card-deck", "id", document.querySelectorAll("#Container .card-deck").length),
        this.nameD = element("div", "class", "card card-columns"),
        this.brandD = element("div", "class", "card card-columns"),
        this.pricepuD = element("div", "class", "card card-columns"),
        this.quantityD = element("div", "class", "card card-columns"),
        this.TotalD = element("div", "class", "card card-columns"),
        this.idD = element("div", "style", "display: none");
    }
    arrange() {
        this.create();
        this.eml.appendChild(this.nameD),
        this.eml.appendChild(this.brandD),
        this.eml.appendChild(this.pricepuD),
        this.eml.appendChild(this.quantityD),
        this.eml.appendChild(this.TotalD),
        this.eml.appendChild(this.idD);
    }
    insert() {
        this.arrange();
        this.nameD.append(this.name),
        this.brandD.append(this.brand),
        this.pricepuD.append(this.price),
        this.TotalD.append(this.total);
        this.idD.append(this.id);
        this.quantityD.append(this.quantity);
    };
    show() {
        this.insert();
        document.getElementById("Container").appendChild(this.eml);
    }
}

class ItemC extends Item {
    constructor(name, id, price, brand, totalPrice, quantity) {
        super(name, id, price, brand, totalPrice, quantity)
    }
    create() {
        super.create();
        this.quantityI = element("input", "type", "number", "class", "custome", "value", this.quantity);
    }
    insert() {
        super.insert();
        this.quantityD.innerHTML = "";
        this.quantityD.appendChild(this.quantityI);
        this.quantityI.onkeyup = function () {
            var elm = this.parentElement.parentElement;
            var uid = elm.id;
            var all = elm.querySelectorAll("div");
            var totalprice = all[4];
            var id = all[5].innerHTML;
            var quantity = this.value;
            totalprice.innerHTML = "";
            var Srequest = new Request("http://localhost:59616/api/PriceP?id=" + id + "&Quantity=" + quantity, {
                credentials: "same-origin",
                method: "Get",
                mode: "cors",
                headers: new Headers({ "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8" }),
            });
            HttpDo(Srequest, function (e) {
                totalprice.append(parseFloat(e));
                allItem[uid].total = parseFloat(e);
                allItem[uid].quantity = quantity;
                totalcount();
            });
        };
    }
}
