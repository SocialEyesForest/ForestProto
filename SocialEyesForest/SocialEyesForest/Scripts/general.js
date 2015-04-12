
var map;
var selectedShape;
function initialize() {
    var canvas = document.getElementById("map-canvas");
    var mapOptions = {
        zoom: 7,
        center: new window.google.maps.LatLng(-12, -65),
        mapTypeId: window.google.maps.MapTypeId.SATELLITE,
        // controles adicionales
        streetViewControl: true,
        panControl: false,
        scaleControl: true,
        mapTypeControl: true,
        mapTypeControlOptions: {
            style: window.google.maps.MapTypeControlStyle.HORIZONTAL_BAR
        },
        overviewMapControl: true
    };
    map = new window.google.maps.Map(canvas, mapOptions);

    initModule();
}

function clearSelection() {
    if (selectedShape) {
        selectedShape.setEditable(false);
        selectedShape = null;
    }
}
function setSelection(shape) {
    clearSelection();
    selectedShape = shape;
    shape.setEditable(true);
}

function deleteSelectedShape() {
    if (selectedShape) {
        selectedShape.setMap(null);
    }
}

function UserControl(controlDiv, map, id, caption, tip, action, enabled) {

    controlDiv.style.padding = "5px";

    // Set CSS for the control border
    var controlUi = document.createElement("div");
    controlUi.id = id;
    controlUi.style.backgroundColor = "white";
    controlUi.style.borderStyle = "solid";
    controlUi.style.borderWidth = "1px";
    controlUi.style.cursor = "pointer";
    controlUi.style.textAlign = "center";
    controlUi.title = tip;
    controlDiv.appendChild(controlUi);

    // Set CSS for the control interior
    var controlText = document.createElement("div");
    controlText.id = id + "Text";
    controlText.style.fontFamily = "Arial,sans-serif";
    controlText.style.fontSize = "12px";
    controlText.style.paddingTop = "1.5px";
    controlText.style.paddingBottom = "1.5px";
    controlText.style.paddingLeft = "6px";
    controlText.style.paddingRight = "6px";
    controlText.innerHTML = caption;
    controlUi.appendChild(controlText);

    if (enabled) {
        window.google.maps.event.addDomListener(controlUi, "click", action);
    } else {
        controlUi.style.borderColor = "#cccccc";
        controlText.style.color = "#cccccc";
    }
}

function cargarCombos(idCombo, url, textoDefault, change, despues) {
    $(idCombo).prop("disabled", "disabled"); // deshabilita el combo mientras se carga
    $(idCombo).get(0).options.length = 0; // limpia combo
    if (url != null) {
        $(idCombo).get(0).options[0] = new Option("Cargando...", "-1");
        $.getJSON(url, function (data) {
            $(idCombo).get(0).options.length = 0; // limpia combo
            if (textoDefault != null)
                $(idCombo).get(0).options[0] = new Option(textoDefault, "-1"); // texto por defecto
            $.each(data, function (index, item) { // agrega items
                $(idCombo).get(0).options[$(idCombo).get(0).options.length] = new Option(item.t, item.k);
            });
            if (change)
                $(idCombo).change(function () {
                    change(this.value); // agrega el evento change
                });
            $(idCombo).prop("disabled", false); // habilita el combo despues de cargarse
            if (despues) {
                despues(); // metodo para ejecutar despues de cargado el combo
            }
        });
    }
}
