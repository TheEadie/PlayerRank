#!/bin/bash

G_BuidNumber=$BUILD_NUMBER
G_Branch=$BRANCH
G_PullRequest=$PULL_REQUEST
buildTagString="build/shippable/"

if [ "$G_PullRequest" = "false" ] && [ "$G_Branch" = "master" ]
then

	nextBuildNumber=$G_BuidNumber

	tag=$buildTagString$nextBuildNumber

	remote=$(git config --get remote.origin.url)
	remote=$(awk -F/ 'gsub($3,"git@github.com",$0)' <<< "$remote")
	remote=$(echo $remote | sed 's,/,:,3')
	remote=$(echo $remote | sed 's,https://,,g')

	echo "Attempting to tag with: "
	echo $tag
	echo "to"
	echo $remote
	
	git tag $tag
	git push --tags $remote
fi