$(document).ready(function ()
{

    var timerfromVal = moment().format('hh:mm A');
    $('#FromtimerId').timepicker(
        {
            template: 'dropdown',
            minuteStep: 1,
            secondStep: 1,
            showMeridian: true,
            defaultTime: timerfromVal
        });

    $('#FromtimerId').timepicker().on('hide.timepicker', function (e)
    {
        $('#FromTime').val(e.time.value);
        $("#FromTimeValdiation").hide();
        let from = $("#FromtimerId").val();
        let to = $("#TotimerId").val();
        if (CompareTimes(from, to)) {
            $("#GreaterValdiation").show();
        } else {
            $("#GreaterValdiation").hide();
        }
    });

    var timerToVal = moment().format('hh:mm A');
    $('#TotimerId').timepicker(
        {
            template: 'dropdown',
            minuteStep: 1,
            secondStep: 1,
            showMeridian: true,
            defaultTime: timerToVal
        });

    $('#TotimerId').timepicker().on('hide.timepicker', function (e) {
        $('#ToTime').val(e.time.value);
        $("#ToTimeValdiation").hide();
        let from = $("#FromtimerId").val();
        let to = $("#TotimerId").val();
        if (CompareTimes(from, to)) {
            $("#GreaterValdiation").show();
        } else {
            $("#GreaterValdiation").hide();
        }
    });
    
});
let getTime = (v) => {
    return Date.parse("11-7-2018 " + v);
}
function CompareTimes(from, to) {
    if (getTime(from) >= getTime(to)) {
        return true;
    } else {
        return false;
    }
}
var AttendanceTimeVal = moment().format('hh:mm A');
$('#AttendanceTimeId').timepicker(
    {
        template: 'dropdown',
        minuteStep: 1,
        secondStep: 1,
        showMeridian: true,
        defaultTime: AttendanceTimeVal
    });

$('#AttendanceTimeId').timepicker().on('hide.timepicker', function (e) {
    $('#AttendanceTime').val(e.time.value);
    $("#AttendanceTimeValdiation").hide();
});