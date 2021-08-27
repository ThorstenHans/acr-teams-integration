#!/bin/bash

# Variables
ACR_NAME=acrteams2021

docker pull alpine:latest
docker tag alpine:latest $ACR_NAME.azurecr.io/alpine:0.0.1

# Login to ACR
az acr login -n $ACR_NAME

# Push the container image to ACR
docker push $ACR_NAME.azurecr.io/alpine:0.0.1
