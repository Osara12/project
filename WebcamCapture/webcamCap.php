
<div class=main>
	<div class="center clear">
		<div class="demo-wrapper">
			<video id="video" width="128" height="96" autoplay></video>
			<br><p>เมื่อทำข้อสอบเสร็จแล้วกรุณาคลิกปุ่ม "ส่งข้อสอบ"</p>
			<button id="snap">ส่งข้อสอบ</button>
			<br>  
			<canvas id="canvas" width="128" height="96"></canvas>
			<script>
				//document.getElementById("video").style.visibility = "hidden";
				document.getElementById("canvas").style.visibility = "hidden";
				// Put event listeners into place
				window.addEventListener("DOMContentLoaded", function() {
				// Grab elements, create settings, etc.
            	var canvas = document.getElementById('canvas');
           		var context = canvas.getContext('2d');
            	var video = document.getElementById('video');
            	var dataURL = canvas.toDataURL();
				

            	var mediaConfig =  { video: true };
            	var errBack = function(e) {
            		console.log('Cannot access Camera', e)
            	};

				// Put video listeners into place
            	if(navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
            	    navigator.mediaDevices.getUserMedia(mediaConfig).then(function(stream) {
            	        video.src = window.URL.createObjectURL(stream);
            	        video.play();
            	    });
            	}
            	/* Legacy code below! */
            	else if(navigator.getUserMedia) { // Standard
					navigator.getUserMedia(mediaConfig, function(stream) {
						video.src = stream;
						video.play();
					}, errBack());
				}
				 else if(navigator.webkitGetUserMedia) { // WebKit-prefixed
					navigator.webkitGetUserMedia(mediaConfig, function(stream){
						video.src = window.webkitURL.createObjectURL(stream);
						video.play();
					}, errBack());
				} else if(navigator.mozGetUserMedia) { // Mozilla-prefixed
					navigator.mozGetUserMedia(mediaConfig, function(stream){
						video.src = window.URL.createObjectURL(stream);
						video.play();
					}, errBack());
				}



				// Trigger photo take
				document.getElementById('snap').addEventListener('click', function() {
					context.drawImage(video, 0, 0, 128, 96);
					var photo = canvas.toDataURL('image/png');    
					console.log(photo);
					 $.post("script.php",{photo: photo});
					 window.location.href = "end.html";
				}, false);
				});

				


			</script>
		</div>
	</div>
</div>
