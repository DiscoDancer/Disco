const playIcon = document.getElementsByClassName("play-icon")[0];
const audio = document.getElementsByTagName("audio")[0];

playIcon.addEventListener("click", () => {
    audio.play();
});