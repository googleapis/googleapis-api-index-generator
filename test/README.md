This directory contains:

- This file :)
- Protos and YAML files for sample APIs (under google)
- A minimal copy of real protos from googleapis, for the sake of
  importing annotations
- An expected API index
- An empty "grafeas" directory as the simplest way of avoiding
  having to change the main generator script

Currently the only test for this repository is "run the index
generator, and check it matches the expected output".

The sample APIs can include "real" APIs (which don't need to be kept
up-to-date) and ones just for the purpose of testing the API index
generator.
