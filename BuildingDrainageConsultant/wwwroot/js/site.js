
var BingMapKey = "AicjvH6hf25PGxy10gGK8o-ZefLcBBqlraxAmLN5sVjanvZ7XjSMqeOmLGStnS0i";

var renderRequestsMap = function (divIdForMap, requestData) {
    if (requestData) {
        var bingMap = createBingMap(divIdForMap);
        addRequestPins(bingMap, requestData);
    }
}

function createBingMap(divIdForMap) {
    return new Microsoft.Maps.Map(
        document.getElementById(divIdForMap), {
        credentials: BingMapKey
    });
}

function addRequestPins(bingMap, requestData) {
    var locations = [];

    $.each(requestData, function (index, data) {

        var location = new Microsoft.Maps.Location(data.lat, data.long);
        locations.push(location);

        var pin = new Microsoft.Maps.Pushpin(location, {
            title: data.name,
        });

        bingMap.entities.push(pin);
    });

    var sofiaCenter = new Microsoft.Maps.Location(42.695735341683395, 23.32238837042256);

    bingMap.setView({
        center: sofiaCenter,
        mapTypeId: Microsoft.Maps.MapTypeId.canvasLight,
        padding: 80,
        zoom: 12
    });
}

$(function () {
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});

showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#window-modal .modal-body').html(res);
            $('#window-modal .modal-title').html(title);
            $('#window-modal').modal('show');
        }
    })
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#window-modal .modal-body').html('');
                    $('#window-modal .modal-title').html('');
                    $('#window-modal').modal('hide');
                }
                else
                    $('#window-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}