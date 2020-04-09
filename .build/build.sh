#!/bin/bash

# Install pre-reqs
dotnet tool install -g GitVersion.Tool

# Clear artifacts folder
rm .artifacts/ -rf
mkdir .artifacts

# Calculate the version
GitVersionOutput=dotnet-gitversion
Version=$($GitVersionOutput | jq -r '.MajorMinorPatch')
NuGetVersion=$($GitVersionOutput | jq -r '.NuGetVersion')

echo "Writing version to file"
touch .artifacts/version.json
$GitVersionOutput > .artifacts/version.json

# Build
echo "Building version:" $Version
dotnet build -c Release /p:Version=$Version

# Test
echo "Running tests"
dotnet test --logger "trx;LogFileName=PlayerRank.Tests.trx"

# Create NuGet Package
echo "Creating NuGet Package:" $NuGetVersion
dotnet pack -c Release -o ".artifacts/" /p:Version=$Version /p:PackageVersion=$NuGetVersion