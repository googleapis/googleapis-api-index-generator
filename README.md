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

# Design

The generator is split into two projects:

- Google.Cloud.Tools.ApiIndex.V1: the code generated (by
  `scripts/generate-schema.sh`) from the schema protos
- Google.Cloud.Tools.ApiIndexGenerator: the entry point for the
  generator

The first of these can freely be used by any C# consuming the schema.

The generator is built of model classes (`ApiModel`, `MethodModel`
etc) which closely correspond to the messages in the schema protos.
The intention is that all business logic is performed while creating
the models, which can then be transformed straightforwardly in
methods conventionally named `ToV1Api`, `ToV1Method` etc.

This approach is designed to lend itself to supporting multiple
schema versions in the future: the schema dependencies are really
all in those `ToV1Xyz` methods.

# Testing

Currently, the simplest way of testing is to run
`scripts/golden-test.sh`. This generates a new index (as
`tmp/api-index-v1.json`) from the protos and YAML files in the
`test` directory, and compares the result with
`test/golden-index-v1.json`.

There are two approaches to testing a change using this "golden" test:

- Work out what the impact should be on the golden index file, edit
  it accordingly, then implement the change until the test passes.
- Implement the change, run the test (which will fail) and copy
  the `tmp/api-index-v1.json` file as the new golden file,
  then check the diff carefully to ensure that it really *is*
  as expected.

The first of these approaches is significantly more diligent and
"test-first" than the second... but does take a bit longer. Please
do review changes to the golden file carefully!

If we find we need to write genuine unit tests, we can easily add a
test project. It's likely that we'd need to also add test protos and
a descriptor test generated from them, however - so while changing
the "golden" test works, that's probably the simplest approach.
