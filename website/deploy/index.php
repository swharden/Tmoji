<?php

// Ensure key file exists (though it should not be web-accessible)
$keyFilePath = realpath('../../../../../../') . '/deploy.key';
if (file_exists($keyFilePath) == false) {
    echo "ERROR: file not found $keyFilePath";
    die();
}

// Get authorization header from Apache
$pass = $_SERVER['PHP_AUTH_PW'];
if (empty($pass)){
    echo "ERROR: Password Required";
    die();
}

// Compare given token vs one on disk
$correctPassword = trim(file_get_contents($keyFilePath));
if ($pass == $correctPassword) {
	echo "PULL:\n";
    echo exec('git pull');
	echo "\n\nSTATUS:\n";
    echo exec('git status');
} else {
    echo "ERROR: Authorization Failed";
}