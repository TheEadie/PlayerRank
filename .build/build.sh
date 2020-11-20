#!/bin/bash
source `dirname "$0"`/functions.sh

# Dependencies
DockerImage_GitVersion="gittools/gitversion:5.3.4-linux-alpine.3.10-x64-netcoreapp3.1"

# Clear artifacts folder
rm .artifacts/ -rf
mkdir .artifacts

# Calculate the version
WriteHeading "Calculating next version"
GitVersionOutput="echo $(docker run --rm -v "$(pwd):/repo" $DockerImage_GitVersion /repo)"
Version=$($GitVersionOutput | jq -r '.MajorMinorPatch')
NuGetVersion=$($GitVersionOutput | jq -r '.NuGetVersion')

touch .artifacts/version.json
$GitVersionOutput > .artifacts/version.json

# Build
WriteHeading "Building version: $Version"
dotnet build -c Release /p:Version=$Version

# Test
WriteHeading "Running tests"
dotnet test --logger "trx;LogFileName=PlayerRank.Tests.trx"

# Create NuGet Package
WriteHeading "Creating NuGet Package: $NuGetVersion"
dotnet pack -c Release -o ".artifacts/" /p:Version=$Version /p:PackageVersion=$NuGetVersion