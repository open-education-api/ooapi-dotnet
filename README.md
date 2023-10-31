# ooapi
dot.net-implementation of the ooapi-specification
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=open-education-api_ooapi-dotnet&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=open-education-api_ooapi-dotnet)

# error handling
- ExceptionHandlingMiddleware handles all exceptions, logs the error, and returns a 500 error response. This middleware can be extended to handle specific exceptions and return a appropriate error response.
- GetById operations may return a 404 when the result is null, this is handled in the controller operation. Some operations that return a collection can also return a 404 according to the spec, this is currently not implemented.
- Invalid request models are converted to a 400 response with the validation errors by ValidateModelStateAttribute.

# unit test conventions
https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
With the following nuget packages:
- AutoFixture (create fake data)
- NSubstitute (mocking)
- FluentAssertions (more readable assertions)
