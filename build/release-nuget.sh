#!/bin/bash
set -euo pipefail

NUGET_API_TOKEN=$1

# Upload to NuGet
>&2 echo "Upload to nuget.org..."
dotnet nuget push **/*.nupkg -k $NUGET_API_TOKEN -s https://api.nuget.org/v3/index.json
