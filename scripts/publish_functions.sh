#!/bin/bash

# Variables
FUNCTION_APP_NAME=fnapp-acr-teams

# deploy the current code to Azure Functions app
func azure functionapp publish $FUNCTION_APP_NAME -b local
