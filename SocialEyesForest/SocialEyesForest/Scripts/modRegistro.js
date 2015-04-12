var rootPath = '';
var markerEvento;

function Evento(/*pFechaEvento,*/ pLat, pLng, pUbicacion, pIdTipoEvento, pIdMotivo, pSubMotivo, pObservaciones) {
    //this.FechaEvento = pFechaEvento;
    this.Lat = pLat;
    this.Lng = pLng;
    this.Ubicacion = pUbicacion;
    this.IdTipoEvento = pIdTipoEvento;
    this.IdMotivo = pIdMotivo;
    this.SubMotivo = pSubMotivo;
    this.Observaciones = pObservaciones;
}

//Establecer tamaño de la ventana 
var dWidth = 300;
var dTop = 30;
var wHeight = 0;
var wWidth = 0;
var dHeight = 0;
var dLeft = 0;

function calculateDialogMeasures() {
    wHeight = $(window).height();
    wWidth = $(window).width();
    dHeight = wHeight - dTop - 10;
    dLeft = wWidth - dWidth - 10;
}

function initDialog() {
    $(window).resize(function () {
        calculateDialogMeasures();
        $("#dialogo").dialog("option", "height", dHeight);
        $("#dialogo").dialog("option", "position", [dLeft, dTop]);
        var position = $('.selector').dialog('option', 'position');
        console.log(dHeight, dLeft, dTop);
        console.log(position.left);
    });

    calculateDialogMeasures();
    $("#dialogo").dialog({
        autoOpen: false,
        width: dWidth,
        height: dHeight,
        position: [dLeft, dTop]
    });
    console.log(dHeight, dLeft, dTop);
}

function initializeJQcontrols() {
    cargarCombos('#cbbTipoEvento', rootPath + 'tipoEventos/listar', null, null, null);
    cargarCombos('#cbbMotivo', rootPath + 'motivos/listar', null, null, null);

    $("#btnGrabar").click(grabarEvento);
}

function initModule() {
    console.log("Inicializando modulo...");
    //initDialog();
    initDialog();
    initializeJQcontrols();

    var drawingManager = new window.google.maps.drawing.DrawingManager({
        drawingControlOptions: {
            drawingModes: [
                //window.google.maps.drawing.OverlayType.POLYGON,
                window.google.maps.drawing.OverlayType.MARKER
            ]
        },
        polygonOptions: {
            editable: true
        },
        markerOptions: {
            //icon: {
            //    path: "M42.8-72.919c0.663-7.855 3.029-15.066 7.2-21.675L34.002-110c-5.054 4.189-10.81 6.509-17.332 6.919 c-5.976 0.52-11.642-0.574-16.971-3.287c-5.478 2.626-11.121 3.723-17.002 3.287c-6.086-0.523-11.577-2.602-16.495-6.281 l-16.041 15.398c3.945 6.704 6.143 13.72 6.574 21.045c0.205 3.373-0.795 8.016-3.038 14.018c-1.175 3.327-2.061 6.213-2.667 8.627 c-0.562 2.394-0.911 4.34-1.027 5.801c-0.082 6.396 1.78 12.168 5.602 17.302c2.986 3.745 7.911 7.886 14.748 12.41 c7.482 3.665 13.272 6.045 17.326 7.06c1.163 0.521 2.301 1.025 3.363 1.506C-7.9-5.708-6.766-5.232-5.586-4.713 C-3.034-3.242-1.243-1.646-0.301 0C0.858-1.782 2.69-3.338 5.122-4.713c1.717-0.723 3.173-1.346 4.341-1.896 c1.167-0.494 2.037-0.865 2.54-1.09c0.866-0.414 2.002-0.888 3.376-1.41c1.386-0.527 3.101-1.168 5.144-1.882 c3.951-1.348 6.83-2.62 8.655-3.77c6.634-4.524 11.48-8.595 14.566-12.235c3.958-5.152 5.879-10.953 5.79-17.475 c-0.232-2.922-1.52-7.594-3.85-13.959C43.463-64.631 42.479-69.445 42.8-72.919z",
            //    scale: 0.5,
            //    fillColor: "red",
            //    strokeWeight: 1,
            //    fillOpacity: 0.8
            //},
            draggable: true
        },
        map: map

    });
    window.google.maps.event.addListener(drawingManager, "overlaycomplete", function (e) {
        if (e.type != window.google.maps.drawing.OverlayType.MARKER) {
            drawingManager.setDrawingMode(null);

            var newShape = e.overlay;
            newShape.type = e.type;
            newShape.key = -1;
            window.google.maps.event.addListener(newShape, "click", function () {
                setSelection(newShape);
            });
            setSelection(newShape);
        }
        if (e.type == window.google.maps.drawing.OverlayType.MARKER) {
            drawingManager.setDrawingMode(null);
            markerEvento = e.overlay;
            markerEvento.type = e.type;
            $("#dialogo").dialog("open");
        }
    });

    window.google.maps.event.addListener(drawingManager, "drawingmode_changed", clearSelection);

}

function grabarEvento() {
    var evento = new Evento(
        markerEvento.getPosition().lat(),
        markerEvento.getPosition().lng(),
        "MiUbicacion",
        $("#cbbTipoEvento").val(),
        $("#cbbMotivo").val(),
        $("#txtSubMotivo").val(),
        $("#txtObservacion").val()
    );
    var data = JSON.stringify(evento);
    console.log(data);
    $.ajax({
        url: rootPath + 'eventos/post',
        contentType: 'application/json',
        dataType: 'json',
        type: 'POST',
        data: data,
        success: function (msg) {
            console.log(msg);
            idEvento = msg.Id;
            grabarFotos(idEvento);
            alert("Evento grabado satisfactoriamente\nId:" + idEvento);
            
        }
    });
}

var idEvento;

function grabarFotos(id) {
    //$('#txtUploadFile').on('change', function(e) {
    var input = document.getElementById('txtUploadFile');
        var files = input.files; //e.target.files;
        if (files.length > 0) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append("file" + x, files[x]);
                }

                $.ajax({
                    type: "POST",
                    url: 'eventos/postImage?id=' + idEvento,
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function(result) {
                        console.log(result);
                    },
                    error: function(xhr, status, p3, p4) {
                        var err = "Error " + " " + status + " " + p3 + " " + p4;
                        if (xhr.responseText && xhr.responseText[0] == "{")
                            err = JSON.parse(xhr.responseText).Message;
                        console.log(err);
                    }
                });
            } else {
                alert("This browser doesn't support HTML5 file uploads!");
            }
        }
    //});
}
