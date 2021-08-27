#!/bin/bash

# Variables
FUNCTION_URL=<paste function app url including function-key>
ACR_NAME=acrteams2021

# create a webhook in ACR
az acr webhook create -n webhook-notify-teams -r $ACR_NAME --uri $FUNCTION_URL --actions push chart_push
