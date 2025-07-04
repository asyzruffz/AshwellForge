window.mpegtsInterop = {
    initialize: (videoElementId, streamUrl) => {
        if (!mpegts.getFeatureList().mseLivePlayback) {
            console.error("mpegts.js is not supported in this browser.");
            return;
        }
        const video = document.getElementById(videoElementId);
        // Create and configure the player
        const player = mpegts.createPlayer({
            type: 'flv',
            isLive: true,
            url: streamUrl
        });
        player.attachMediaElement(video);
        try {
            player.load();
        }
        catch (err) {
            console.error("Error loading video:", err);
        }
        finally {
            // Store instance for later cleanup
            window.mpegtsInterop[videoElementId] = player;
        }
    },

    play: (videoElementId) => {
        const player = window.mpegtsInterop[videoElementId];
        try {
            console.log("Start playing");
            player.play();
        }
        catch (err) {
            console.error("Error playing video:", err);
        }
    },

    destroy: (videoElementId) => {
        const player = window.mpegtsInterop[videoElementId];
        if (player) {
            player.unload();
            player.detachMediaElement();
            player.destroy();
            delete window.mpegtsInterop[videoElementId];
        }
    }
};
