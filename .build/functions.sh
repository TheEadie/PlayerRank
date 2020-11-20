#!/bin/bash

WriteHeading () 
{
    BLUE='\033[0;34m'
    NOCOLOR='\033[0m'

    echo -e "${BLUE}"
    echo -e "#################################"
    echo -e $1
    echo -e "#################################"
    echo -e "${NOCOLOR}"
}