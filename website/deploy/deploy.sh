#!/bin/bash
# This script pulls the latest website from GitHub.
# - This file should be read-only
# - This file should be outside your web folder
# - Call from PHP via system() not exec()
# - The PHP script calling this should require authentication

git pull
git status