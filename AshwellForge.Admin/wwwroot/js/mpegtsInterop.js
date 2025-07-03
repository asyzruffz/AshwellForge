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
        player.load();
        // Store instance for later cleanup
        window.mpegtsInterop[videoElementId] = player;
    },

    play: () => {
        const player = window.mpegtsInterop[videoElementId];
        console.log("Start playing");
        player.play();
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
