<?php

$bruteForceProtection = false;
$failedAttemptsLimit = 5;
$failedAttemptsTimeLimit = 300;	// 5 minutes
$failedAttemptsPath = "failedAttempts.json";
$passwords = array(
	0 => "5612",	// Numerals [4]
	1 => "32276",	// Numerals [5]
	2 => "bzcd",	// Characters [4]
	3 => "h4b2",	// Characters + numerals [4]
	4 => "z+1c"		// Characters + numerals + special symbols [4]
);
$passwordsMatchMessage = "OK";
$passwordsNotMatchMessage = "KO";

if(isset($_POST["difficulty"]) && isset($_POST["password"])) {
	if($bruteForceProtection) {
		// Create file if not exists
		if(!file_exists($failedAttemptsPath)) {
			$failedAttempts = array();
			for($i = 0; $i < count($passwords); $i++) {
				$failedAttempts[$i] = array(0, time());
			}

			// Create & fill file
			touch($failedAttemptsPath);
			file_put_contents($failedAttemptsPath, json_encode($failedAttempts));
		}
		
		// Load file
		$content = file_get_contents($failedAttemptsPath);
		$failedAttempts = json_decode($content, true);
		
		// Limit reached
		if($failedAttempts[$_POST["difficulty"]][0] >= $failedAttemptsLimit
		&& $failedAttempts[$_POST["difficulty"]][1] >= time() - $failedAttemptsTimeLimit) {
			header('HTTP/1.1 429 Too Many Requests');
			echo "429 Too Many Requests";
			exit;
		}
		else {
			// Reset counter when time limit passes
			if($failedAttempts[$_POST["difficulty"]][0] >= $failedAttemptsLimit) {
				$failedAttempts[$_POST["difficulty"]] = array(0, time());
			}

			// Passwords match
			if($passwords[$_POST["difficulty"]] == $_POST["password"]) {
				$failedAttempts[$_POST["difficulty"]] = array(0, time());
				echo $passwordsMatchMessage;
			}
			else {
				$failedAttempts[$_POST["difficulty"]][0]++;
				$failedAttempts[$_POST["difficulty"]][1] = time();
				echo $passwordsNotMatchMessage;
			}
			file_put_contents($failedAttemptsPath, json_encode($failedAttempts));
		}
	}
	else {
		echo ($passwords[$_POST["difficulty"]] == $_POST["password"] ? $passwordsMatchMessage : $passwordsNotMatchMessage);
	}
}
else {
	header('HTTP/1.1 400 Bad Request');
	echo "400 Bad Request";
	exit;
}

?>