# ooapi
dot.net-implementation of the ooapi-specification

# exception handling
- ExceptionHandlingMiddleware handles all exceptions and returns a 500 error response.
- GetById operations may return a 404 when the result is null, this is handled in the controller operation.
- Invalid request models are converted to a 400 response with the validation errors by the default implementation of .NET.

# unit test conventions
https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
With the following nuget packages:
- AutoFixture (create fake data)
- NSubstitute (mocking)
- FluentAssertions (more readable assertions)