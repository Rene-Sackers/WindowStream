var noSleepInstance = new NoSleep();

window.fullscreenStream = function () {
	var element = document.getElementById("stream");

	if (typeof (element.requestFullscreen) !== "undefined")
		element.requestFullscreen();
	else if (typeof (element.webkitRequestFullScreen) !== "undefined")
		element.webkitRequestFullScreen();
	else if (typeof (element.webkitRequestFullscreen) !== "undefined")
		element.webkitRequestFullscreen();
	else if (typeof (element.mozRequestFullScreen) !== "undefined")
		element.mozRequestFullScreen();
	else if (typeof (element.msRequestFullscreen) !== "undefined")
		element.msRequestFullscreen();

	noSleepInstance.enable();
};