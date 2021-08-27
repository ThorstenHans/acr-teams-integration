#!/bin/sh

# Login to Azure
az login

# Variables
RG_NAME=rg-acr-teams
LOCATION=germanywestcentral
ACR_NAME=acrteams2021 # must be globally unique
SA_NAME=sademo2021 # must be globally unique and between 3-24 in length (chars and digits)
AI_NAME=ai-acr-teams
FUNCTION_APP_NAME=fnapp-acr-teams

# Set the desired Azure Subscription
az account set --subscription <YOUR SUBSCRIPTION_ID>

# Create an Azure Resource Group
az group create -n $RG_NAME -l $LOCATION

# Create an Azure Container Registry instance
az acr create -n $ACR_NAME --sku Basic -g $RG_NAME -l $LOCATION --admin-enabled false

# Create the Azure Storage Account for the Azure Functions App
az storage account create -n $SA_NAME -g $RG_NAME -l $LOCATION

# Create the Azure Application Insights instance for the Azure Functions App
 az monitor app-insights component create --app $AI_NAME -l $LOCATION --kind web -g $RG_NAME --application-type web

# Create the Azure Functions App
az functionapp create -n $FUNCTION_APP_NAME -g $RG_NAME -consumption-plan-location $LOCATION --os-type Linux --app-insights $AI_NAME --functions-version 3 --runtime dotnet --storage-account $SA_NAME
