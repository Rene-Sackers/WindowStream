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

window.streamImageClicked = function (imageElement, clientX, clientY, streamWindowInstance) {
	var boundingRect = imageElement.getBoundingClientRect();
	var relativeX = (clientX - boundingRect.left) / boundingRect.width;
	var relativeY = (clientY - boundingRect.top) / boundingRect.height;

	console.log(relativeX, relativeY);
	streamWindowInstance.invokeMethodAsync("PerformClickJs", relativeX, relativeY);
	streamWindowInstance.dispose();
};