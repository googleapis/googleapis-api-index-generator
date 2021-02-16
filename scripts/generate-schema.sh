#!/bin/bash

set -e

SCRIPT=$(readlink -f "$0")
SCRIPT_DIR=$(dirname "$SCRIPT")
REPO_ROOT=$(realpath "$SCRIPT_DIR/..")

cd $REPO_ROOT

source $SCRIPT_DIR/toolfunctions.sh
install_protoc

$PROTOC -I. --csharp_out=src/Google.Cloud.Tools.ApiIndex.V1 index_v1.proto
