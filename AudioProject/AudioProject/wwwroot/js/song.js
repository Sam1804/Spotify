
$(document).ready(function () {

    var wavesurfer = WaveSurfer.create({
        container: '#waveform',
        waveColor: 'darkblue',
        progressColor: '#1d82dc',
        barWidth:'3',
    });
    
    wavesurfer.on('ready', function () {
        wavesurfer.play();
        wavesurfer.setMute(false);
    });

    var volume = 0.2;

    $("#test").click(function () {
        var path = $(this).attr('data-path');
        wavesurfer.load('../Song/' + path);
        wavesurfer.setVolume(volume)
    });

   
    $("#play").click(function () {
        wavesurfer.playPause();
        if ($("#play").val() == 'Play') {
            $("#play").val('Pause')
        }
        else {
            $("#play").val('Play')
        }
    });

    $("#mute").click(function () {
        wavesurfer.toggleMute();
        if ($("#mute").val() == 'Mute') {
            $("#mute").val('UnMute')
        }
        else {
            $("#mute").val('Mute')
        }
    });

    $("#more").click(function () {
        wavesurfer.setVolume(volume = volume + 0.1)
    });

    $("#less").click(function () {
        wavesurfer.setVolume(volume = volume - 0.1)
    });

})
