#!/bin/bash
set -euo pipefail

NEXT_VERSION="$(cat .artifacts/version.json | jq -r '.["major-minor-patch"]')"
NUGET_VERSION="$(cat .artifacts/version.json | jq -r '.["nuget-version"]')"

# Publish
>&2 echo "Publishing $NUGET_VERSION..."
dotnet pack -c Release --no-build -o ".artifacts/" /p:Version=$NEXT_VERSION /p:PackageVersion=$NUGET_VERSION