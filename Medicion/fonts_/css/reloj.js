function activarReloj() {
    var Digital = new Date()
    var day = Digital.getDate()
    var month = Digital.getMonth() + 1
    var year = Digital.getFullYear()
    var hours = Digital.getHours()
    var minutes = Digital.getMinutes()
    var seconds = Digital.getSeconds()
    var dn = "AM"

    if (hours > 12) {
        dn = "PM"
        hours = hours - 12
    }

    if (hours == 0)
        hours = 12

    if (minutes <= 9)
        minutes = "0" + minutes

    if (seconds <= 9)
        seconds = "0" + seconds
    var ctime = hours + ":" + minutes + ":" + seconds + " " + dn
    $(".relojiberdrola").html(day + "/" + month + "/" + year + " " + ctime);
    setTimeout("activarReloj()", 1000)
}