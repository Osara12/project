<?php
    
		$data = $_POST["photo"];

		list($type, $data) = explode(';', $data);
		list(, $data)      = explode(',', $data);
		$data = base64_decode($data);

		
		date_default_timezone_set("Asia/Bangkok");
		$date = date('d-m-Y-H-i-sa');
		file_put_contents($_SERVER['DOCUMENT_ROOT'] . '/webcam/photos/'.$date.'.png' , $data);
		
	die;
?>




