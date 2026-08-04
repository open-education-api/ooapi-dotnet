using Xunit;

// OeapiWebApplicationFactory overrides the database connection via process environment variables
// (see its own doc comment for why), which is global mutable state shared by the whole test
// process. Parallel test execution would let one factory's env vars leak into another factory's
// host build. Disabling parallelization trades some test-run speed for correctness/determinism.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
