for (let component of document.getElementsByClassName("custom-audio-component")) {
    const playIcon = component.getElementsByClassName("play-icon")[0];
    const audio = component.getElementsByTagName("audio")[0];

    playIcon.addEventListener("click", () => {
        audio.play();
    });
}