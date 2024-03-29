#!/bin/bash
set -euo pipefail

GITHUB_TOKEN=$1
GITHUB_REPO=$2
RELEASE_ASSETS_FOLDER=$3

# Calculate the version from the release folder
VERSION=$(cat $RELEASE_ASSETS_FOLDER/version.json | jq -r '.["major-minor-patch"]')

# Create GitHub Release
>&2 echo "Creating GitHub Release $VERSION ..."

CREATE_RELEASE_RESPONSE=$(curl --request POST \
    --url "https://api.github.com/repos/$GITHUB_REPO/releases" \
    --header "authorization: Bearer $GITHUB_TOKEN" \
    --header "content-type: application/json" \
    --data '{
                "tag_name": "release/'$VERSION'",
                "target_commitish": "main",
                "name": "v'$VERSION'",
                "body": "",
                "draft": false,
                "prerelease": false }')

# Upload artifacts to GitHub Release
ASSETURL=$(echo $CREATE_RELEASE_RESPONSE | jq -r '.upload_url' | sed 's/{?name,label}//g')

for filename in $RELEASE_ASSETS_FOLDER/*; do
    echo "Uploading $filename"
    name="${filename##*/}"
    UPLOADURL=$ASSETURL?name=$name
    echo $UPLOADURL

    curl --request POST \
    --url $UPLOADURL \
    --header "authorization: Bearer $GITHUB_TOKEN" \
    --header "Content-Type: application/octet-stream" \
    --data-binary "@$filename"
done