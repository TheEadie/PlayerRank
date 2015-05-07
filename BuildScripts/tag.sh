#!/bin/bash

if [ -n "$G_TagPrefix" ]
then
	buildTagString="$G_TagPrefix"
else
	buildTagString="build/"
fi

if [ -n "$G_NextRelease" ]
then
	nextRelease="$G_NextRelease"
fi

nextBuildNumber=$TRAVIS_BUILD_NUMBER
printf -v nextBuildNumber %05d $nextBuildNumber

if [ -z "$nextRelease" ]
then
	tag=$buildTagString$nextBuildNumber
else
	tag=$buildTagString$nextRelease.$nextBuildNumber
fi

if [ "$TRAVIS_PULL_REQUEST" = "false" ] && [ "$TRAVIS_BRANCH" = "master" ]
then
	git tag $tag
	remote=$(git config --get remote.origin.url)
	remote=$(echo $remote | sed 's,git://,https://'$token'@,g')
	git push --tags $remote &> /dev/null
fi