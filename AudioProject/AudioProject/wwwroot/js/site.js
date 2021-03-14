
var Nightly = new Nightly();

document.getElementById("theme").addEventListener("click", function () {
    Nightly.toggle();
});

$(document).ready(function () {

    $("#theme").click(function () {
        if ($("#theme").val() == 'DarkTheme') {
            $("#theme").val('LightTheme')
        }
        else {
            $("#theme").val('DarkTheme')
        }
    });

    $("#SubmitSong").click(function () {
        window.location.replace("../Audio/Display");
    });

    $("#SubmitPlaylist").click(function () {
        window.location.replace("../Playlist");
    });

})

