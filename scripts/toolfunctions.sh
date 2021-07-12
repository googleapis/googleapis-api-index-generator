# Script for installing protoc etc. This should be "sourced"
# as it sets variables:
# - PROTOC
# - PROTOBUF_VERSION
# - PROTOBUF_ROOT (relative to REPO_ROOT)

# Expects that REPO_ROOT is already set appropriately.

declare -r PROTOBUF_VERSION=3.17.3
declare -r PROTOBUF_ROOT=packages/$PROTOBUF_VERSION

case "$OSTYPE" in
  linux*)
    declare -r PROTOBUF_PLATFORM=linux-x86_64
    declare -r PROTOC=$PROTOBUF_ROOT/bin/protoc
    ;;
  win* | msys* | cygwin*)
    declare -r PROTOBUF_PLATFORM=win64
    declare -r PROTOC=$PROTOBUF_ROOT/bin/protoc.exe
    ;;
  darwin*)
    declare -r PROTOBUF_PLATFORM=osx-x86_64
    declare -r PROTOC=$PROTOBUF_ROOT/bin/protoc
    ;;
  *)
    echo "Unknown OSTYPE: $OSTYPE"
    exit 1
esac

install_protoc() {
  if [[ -d $REPO_ROOT/$PROTOBUF_ROOT ]]
  then
    return
  fi

  mkdir -p $REPO_ROOT/$PROTOBUF_ROOT
  echo "Downloading protobuf@$PROTOBUF_VERSION"
  (cd "$REPO_ROOT/$PROTOBUF_ROOT"; \
     curl -sSL \
       https://github.com/protocolbuffers/protobuf/releases/download/v$PROTOBUF_VERSION/protoc-$PROTOBUF_VERSION-$PROTOBUF_PLATFORM.zip \
       --output protobuf.zip; \
     unzip -q protobuf.zip; \
     rm protobuf.zip \
  )
}
