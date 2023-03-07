#!/bin/bash
set -euo pipefail

>&2 echo "Calculating Verison..."

NEXT_VERSION="$(cat version.json | jq -r '.["next-version"]')"
TAG_PREFIX="$(cat version.json | jq -r '.["tag-prefix"]')"

WriteVersionFile() {
    VERSION="$1"
    if [[ "$2" == "" ]]; then
        NUGETVERSION="$1"
    else
        NUGETVERSION="$1-${2:0:20}"
    fi    

    mkdir -p .artifacts
    >&2 echo "Calculated version: $VERSION"
    jq -n  \
        --arg version "$VERSION" \
        --arg nugetversion "$NUGETVERSION" \
        '{ 
            "major-minor-patch": $version,
            "nuget-version": $nugetversion
        }' > .artifacts/version.json
}

# Read Version parts
IFS='.'  
read -r -a VERSION_PARTS <<<"$NEXT_VERSION"
  
MAJOR="${VERSION_PARTS[0]}"
MINOR="${VERSION_PARTS[1]}"
PATCH=0

# Get last tag
LATEST_TAG=$(git describe --tags --match "$TAG_PREFIX*" --abbrev=0)
HEAD_HASH=$(git rev-parse --verify HEAD)
TAG_HASH=$(git log -1 --format=format:"%H" "$LATEST_TAG" 2>/dev/null | tail -n1)
BRANCH_NAME=$(git rev-parse --abbrev-ref HEAD)

if [[ "$BRANCH_NAME" == "main" ]]; then
    BRANCH_NAME=""
fi

if [[ -z "$LATEST_TAG" ]]; then
    >&2 echo "No previous versions found"
    WriteVersionFile "$NEXT_VERSION.0" "$BRANCH_NAME"
    exit
fi

if [[ -n "$TAG_PREFIX" ]]; then
    LATEST_TAG=${LATEST_TAG#"$TAG_PREFIX"}
fi

if [[ "$HEAD_HASH" == "$TAG_HASH" ]]; then
    >&2 echo "No changes since previous version"
    WriteVersionFile "$LATEST_TAG" "$BRANCH_NAME"
    exit
fi

# Read Version parts
IFS='.'  
read -r -a VERSION_PARTS <<<"$LATEST_TAG"

LATEST_MAJOR="${VERSION_PARTS[0]}"
LATEST_MINOR="${VERSION_PARTS[1]}"
LATEST_PATCH="${VERSION_PARTS[2]}"

if [[ "$MAJOR" > "$LATEST_MAJOR" ]]; then
    WriteVersionFile ${MAJOR}.${MINOR}.${PATCH} "$BRANCH_NAME"
    exit
elif [[ "$MINOR" > "$LATEST_MINOR" ]]; then
    WriteVersionFile ${MAJOR}.${MINOR}.${PATCH} "$BRANCH_NAME"
    exit
else
    ((LATEST_PATCH++))
    WriteVersionFile ${MAJOR}.${MINOR}.${LATEST_PATCH} "$BRANCH_NAME"
    exit
fi