function datatime() {
    var dateUser = todocument.getElementById("date");
    var hourUser = document.getElementsByName("horaire");
    var date = dateUser.toString();
    var hour = hourUser.toString();
    var dateTimeUser = date + " " + hour;
    document.getElementById("ol").value = dateTimeUser; 
}    