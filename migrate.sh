#!/bin/sh

set -e

PROJECT_DIR=$1
FLAG_FILE="/tmp/migration_complete_$PROJECT_DIR"

echo "Starting migrations for $PROJECT_DIR..."

cd $PROJECT_DIR

dotnet tool install --global dotnet-ef --version 9.* || echo "dotnet-ef already installed"
export PATH="$PATH:/root/.dotnet/tools"

dotnet restore

dotnet ef migrations add Initial
dotnet ef database update

touch $FLAG_FILE

echo "Migrations completed for $PROJECT_DIR."
