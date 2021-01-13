// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function FillDrawerByFreezerId() {
    var freezerId = $('#Freezer').val();
    //var url = '/Api/Support/DrawersFree/' + freezerId;
    //var url = '/SampleTrackingStage/Api/Support/DrawersFree/' + freezerId;
    var url = '/SampleTracking/Api/Support/DrawersFree/' + freezerId;
    $('FreezerId').val = freezerId;
    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        success: function (drawers) {
            $("#Drawers").html(""); // clear before appending new list 
            $("#Drawers").append($('<option></option>').val(0).html("--Select a Drawer--"));
            $.each(drawers, function (i, drawer) {
                $("#Drawers").append(
                    $('<option></option>').val(drawer.id).html(drawer.description));
            });
        }
    });
}

function FillDrawerSlotsByDrawerId() {
    var drawerId = $('#Drawers').val();
    var trayDescription = $('#trayName').val();
    //var url = '/Api/Support/DrawerSlotsFree/' + drawerId + '/' + trayDescription;
    //var url = '/SampleTrackingStage/Api/Support/DrawerSlotsFree/' + drawerId + '/' + trayDescription;
    var url = '/SampleTracking/Api/Support/DrawerSlotsFree/' + drawerId + '/' + trayDescription;

    $('DrawerId').val = drawerId;


    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        success: function (drawerSlots) {
            $("#DrawerSlots").html(""); // clear before appending new list 
            $("#DrawerSlots").append($('<option></option>').val(0).html("--Select a Drawer Slot--"));
            $.each(drawerSlots, function (i, drawerSlot) {
                $("#DrawerSlots").append(
                    $('<option></option>').val(drawerSlot.id).html(drawerSlot.slot));
            });
        }
    });
}

//var timeleft = 5;
//var downloadTimer = setInterval(function () {
//    document.getElementById("countdown").innerHTML = timeleft + " seconds remaining until refresh";
//    timeleft -= 1;
//    if (timeleft <= 0) {
//        clearInterval(downloadTimer);
////        document.getElementById("countdown").innerHTML = "Finished"
//    }
//}, 1000);


function showScanPopup() {
    $(document).ready(function () {
        $('#myModal').modal('show');
    });
}
// Read a page's GET URL variables and return them as an associative array.
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

$(document).ready(function () {
    $('div.PostBackOnChange').change(function () {
        $('form').submit();
    });
});

function showCloseSessionButton() {
    var drawerId = $('#Drawers').val();
    var freezerId = $('#Freezer').val();
    var drawerSlotId = $('#DrawerSlots').val();
    $('DrawerSlotId').val = drawerSlotId;

    if (drawerId !== null && freezerId !== null && drawerSlotId !== null) {
        //var y = document.getElementById("freezerLocationVerifyDiv");
        var x = document.getElementById("divValidateLocation");
        //var y = document.getElementById("inputFreezerLocationValidate");
        //var validateFreezerLocation = getUrlVars()["ValidateFreezerLocation"];
        //if (y.style.display === "none") {
        //    y.style.display = "block";
        //    y.style.marginTop = "10px";
        //} else {
        //    y.style.display = "none";
        //}

        //if (validateFreezerLocation === "true") {
        //    x.style.display = "block";
        //    x.style.marginTop = "10px";
        //}

        if (x.style.display === "none") {
            x.style.display = "block";
            x.style.marginTop = "10px";
        } else {
            x.style.display = "none";
        }
    }
}

function getInputValue() {
    //var printerName = document.getElementById("PrinterName");
    var copies = document.getElementById("Copies");
    var logNumbers = document.getElementById("logNumbers");
    var address = "http://igt-ws-local/ClearMini/CrystalReportViewerFramework.application?samples=" + logNumbers.value
        //+ "&printer=" + printerName.value
        + "&copies=" + copies.value;

    window.location.href = address;
    //window.location.href = "http://igt-ws-local/SampleTracking";

    //alert(printerName.value + " " + copies.value + " " + logNumbers.value);
}