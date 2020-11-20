#!/bin/bash

# Dependencies
DockerImage_GitVersion="gittools/gitversion:5.3.4-linux-alpine.3.10-x64-netcoreapp3.1"

# Clear artifacts folder
rm .artifacts/ -rf
mkdir .artifacts

# Calculate the version
echo "Calculating next version"
GitVersionOutput="echo $(docker run --rm -v "$(pwd):/repo" $DockerImage_GitVersion /repo)"
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