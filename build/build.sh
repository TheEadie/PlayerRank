#!/bin/bash
set -euo pipefail

NEXT_VERSION="$(cat .artifacts/version.json | jq -r '.["major-minor-patch"]')"

# Build
>&2 echo "Building $NEXT_VERSION..."
dotnet build -c Release /p:Version="$NEXT_VERSION"