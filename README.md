# API Index Generator for googleapis

This repository contains the generator for an index file in JSON
format for the information in https://github.com/googleapis/googleapis.
The index file will itself live in that repository and be
regenerated in an automated fashion.

# Regenerating the C# code

After modifying the schema, run `scripts/generate-schema.sh`

# Generating the index

- Clone https://github.com/googleapis/googleapis
- Run `scripts/generate-index.sh` passing in the relative path
  to the `googleapis` repo
- The result will be in `tmp`
