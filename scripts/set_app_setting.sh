#!/bin/bash

# Variables
FUNCTION_APP_NAME=fnapp-acr-teams
RESOURCE_GROUP_NAME=rg-acr-teams
WEBHOOK_URL=<your webhook url provided by Microsoft Teams>

# Set appSettings on Azure Functions App
az functionapp config appsettings set -n $FUNCTION_APP_NAME -g $RESOURCE_GROUP_NAME --settings "ContainerRegistryTeams__TeamsWebhookUrl=$WEBHOOK_URL"
