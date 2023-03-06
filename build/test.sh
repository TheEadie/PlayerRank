#!/bin/bash
set -euo pipefail

# Test
>&2 echo "Running tests..."
dotnet test -c Release --no-build --logger "trx;LogFileName=PlayerRank.Tests.trx"