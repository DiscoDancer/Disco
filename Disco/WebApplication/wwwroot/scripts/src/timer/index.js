﻿import ApiService from "./api-service";

(function () {

    ApiService.addLog(1).then(() => console.log("success"));

    const isFrontPage = document.getElementsByClassName("timer-frontpage").length > 0;

    const TIMER_DEFAULT_VALUE = 0 * 60 + 5;

    if (!isFrontPage) return;

    window.AppState = {
        selectedActivityID: null,
        timeOutToken: null,
        totalSeconds: TIMER_DEFAULT_VALUE
    };

    renderTimer();
    updateSelectedActivityID();
    subscribeOnSelectValueChanged();
    initControlButtons();
    

    function initControlButtons() {
        const startBtn = document.getElementsByClassName("start-button")[0];
        const stopBtn = document.getElementsByClassName("stop-button")[0];
        const resetBtn = document.getElementsByClassName("reset-button")[0];

        resetBtn.onclick = (e) => {
            window.AppState.totalSeconds = TIMER_DEFAULT_VALUE;
            renderTimer();
        };

        stopBtn.onclick = (e) => {
            clearTimeout(window.AppState.timeOutToken);
        };

        startBtn.onclick = (e) => {
            clearTimeout(window.AppState.timeOutToken);
            if (window.AppState.totalSeconds === 0) {
                window.AppState.totalSeconds = TIMER_DEFAULT_VALUE;
            }
            window.AppState.timeOutToken = setTimeout(updateTimer, 1000);
        };
    }

    function renderTimer() {
        const minutesSpan = document.getElementsByClassName("time-minutes")[0];
        const secondsSpan = document.getElementsByClassName("time-seconds")[0];

        minutesSpan.innerHTML = Math.floor(window.AppState.totalSeconds / 60) + "";
        secondsSpan.innerHTML = window.AppState.totalSeconds % 60 + "";

        if (secondsSpan.innerHTML.length < 2) {
            secondsSpan.innerHTML = "0" + secondsSpan.innerHTML;
        }
        if (minutesSpan.innerHTML.length < 2) {
            minutesSpan.innerHTML = "0" + minutesSpan.innerHTML;
        }
    }

    function updateTimer() {
        window.AppState.totalSeconds--;

        renderTimer();

        if (window.AppState.totalSeconds === 0) {
            console.log("DONE");
        } else {
            window.AppState.timeOutToken = setTimeout(updateTimer, 1000);
        }
    }

    function updateSelectedActivityID() {
        const activitiesList = document.getElementsByClassName("timer-activity")[0];
        const avaliableActivities = activitiesList.getElementsByTagName("option");

        const selectedIndex =
            activitiesList
                .getElementsByTagName("select")[0].selectedIndex;

        const selectedOption = avaliableActivities[selectedIndex];

        const selectedActivityId = selectedOption.dataset.id ? +selectedOption.dataset.id : -1;

        window.AppState.selectedActivityID = selectedActivityId;
        console.log(window.AppState.selectedActivityID);
    }

    function subscribeOnSelectValueChanged() {
        const select =
            document.getElementsByClassName("timer-activity")[0].getElementsByTagName("select")[0];

        select.onchange = (e) => {
            updateSelectedActivityID();
        }
    }

})();