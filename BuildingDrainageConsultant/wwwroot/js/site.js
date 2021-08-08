
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