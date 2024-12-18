#!/bin/sh

FLAG_FILE="/tmp/migration_complete_$1"

if [ -f "$FLAG_FILE" ]; then
    echo "Migration completed for $1. Health check passed."
    exit 0
else
    echo "Migration not completed for $1. Health check failed."
    exit 1
fi
