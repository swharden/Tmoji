<?php

// Ensure key file exists.
// It should be stored outside the www folder and permission 400.
$keyFilePath = realpath('/home/customer/deploy.key');
if (file_exists($keyFilePath) == false) {
    echo "ERROR: file not found $keyFilePath";
    die();
}

// Get authorization header from Apache
$pass = $_SERVER['PHP_AUTH_PW'];
if (empty($pass)) {
    echo "ERROR: Password Required";
    die();
}

// Compare the given password to the one in the key file
$correctPassword = trim(file_get_contents($keyFilePath));
if ($pass == $correctPassword) {
	
    echo "\n\nPULL:\n";
	$outputPull = null;
    exec('git pull', $outputPull);
	echo join("\n",$outputPull);
	
    echo "\n\nSTATUS:\n";
	$outputStatus = null;
    exec('git status', $outputStatus);
	echo join("\n",$outputStatus);

} else {
    echo "ERROR: Incorrect password";
}
