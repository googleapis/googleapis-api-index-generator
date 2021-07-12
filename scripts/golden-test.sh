#!/bin/bash

# Script to generate a test API index from the protos in the "test"
# directory. The output is placed in the tmp directory, and diffed
# against the golden JSON file in the test directory.

set -e

SCRIPT=$(readlink -f "$0")
SCRIPT_DIR=$(dirname "$SCRIPT")
REPO_ROOT=$(realpath "$SCRIPT_DIR/..")

echo "Generating test index"
$SCRIPT_DIR/generate-index.sh test

echo "Comparing test index with golden"
diff tmp/api-index-v1.json $REPO_ROOT/test/golden-index-v1.json

echo "Golden test complete"
