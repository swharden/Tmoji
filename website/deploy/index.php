<?php

// Ensure root files are accessible
if (file_exists('/home/customer/pull.key') == false) {
    echo "ERROR: pull.key does not exist";
    die();
}

// Get authorization header from Apache
$headers = apache_request_headers();
if (!isset($headers['Authorization'])) {
    echo "ERROR: Authorization Required";
    die();
}

// Compare given token vs one on disk
$givenToken = substr($headers['Authorization'], 7);
$realToken = trim(file_get_contents('/home/customer/pull'));
if ($givenToken == $realToken) {
    system('deploy.sh');
} else {
    echo "ERROR: Authorization Failed";
}