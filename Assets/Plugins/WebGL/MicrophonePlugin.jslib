mergeInto(LibraryManager.library, {
    InitMicrophone: function() {
        if (typeof window.microphoneStream !== 'undefined') {
            return 1;
        }

        var constraints = { audio: true, video: false };
        
        navigator.mediaDevices.getUserMedia(constraints)
            .then(function(stream) {
                window.microphoneStream = stream;
                window.audioContext = new (window.AudioContext || window.webkitAudioContext)();
                window.micInput = window.audioContext.createMediaStreamSource(stream);
                window.analyser = window.audioContext.createAnalyser();
                window.analyser.fftSize = 512;
                window.micInput.connect(window.analyser);
                window.dataArray = new Uint8Array(window.analyser.frequencyBinCount);
                console.log("Microphone initialized successfully");
            })
            .catch(function(err) {
                console.error("Microphone initialization failed: " + err);
            });
        
        return 1;
    },

    GetMicrophoneVolume: function() {
        if (typeof window.analyser === 'undefined') {
            return 0;
        }

        window.analyser.getByteFrequencyData(window.dataArray);
        
        var sum = 0;
        for (var i = 0; i < window.dataArray.length; i++) {
            sum += window.dataArray[i];
        }
        var average = sum / window.dataArray.length;
        
        return average / 255.0;
    },

    StopMicrophone: function() {
        if (typeof window.microphoneStream !== 'undefined') {
            window.microphoneStream.getTracks().forEach(function(track) {
                track.stop();
            });
            if (window.audioContext) {
                window.audioContext.close();
            }
            delete window.microphoneStream;
            delete window.audioContext;
            delete window.micInput;
            delete window.analyser;
            delete window.dataArray;
            console.log("Microphone stopped");
        }
    }
});
