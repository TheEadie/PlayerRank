#!/bin/bash
set -eu -o pipefail

# Clear artifacts folder
>&2 echo "Cleaning..."
rm .artifacts/ -rf
mkdir .artifacts