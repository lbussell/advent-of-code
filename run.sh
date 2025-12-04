#!/usr/bin/env bash

# The whole point of this script is to run the .NET app from it's project
# directory so that it can find the correct appsettings.json.
# And I'm lazy and don't want to change directories.

# Redirect output to /dev/null to avoid cluttering the output
pushd src/Cli > /dev/null
dotnet run -- run "$@"
popd > /dev/null
