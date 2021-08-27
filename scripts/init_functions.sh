#!/bin/bash
func init --worker-runtime dotnet
func new -l csharp --template "Http Trigger" -n FnNotifyTeams -a function

dotnet add package Microsoft.NET.Sdk.Functions
dotnet add package Microsoft.Azure.Functions.Extensions
dotnet add package Microsoft.Extensions.DependencyInjection

