#!/bin/bash

# Variables
FUNCTION_APP_NAME=fnapp-acr-teams

# Grab function url with function key
func azure functionapp list-functions $FUNCTION_APP_NAME --show-keys
