#!/bin/bash

echo "Install dotnet-ef"
dotnet tool install --global dotnet-ef --version 9.*
export PATH="/root/.dotnet/tools:$PATH"

echo "Applying migrations..."
dotnet ef database update --project Nsu.Contest.Web.HRManager

echo "Starting the application..."
dotnet Nsu.Contest.Web.HRManager.dll
