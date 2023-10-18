using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ooapi.v5.Middleware;

namespace ooapi.v5.UnitTests.Middleware;

[TestFixture]
public class ExceptionHandlingMiddlewareTests
{
    [Test]
    public async Task InvokeAsync_ExceptionOccors_HandlesException()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var exception = new Exception("Exception!");
        RequestDelegate next = x => throw exception;
        var logger = Substitute.For<ILogger<ExceptionHandlingMiddleware>>();
        var sut = new ExceptionHandlingMiddleware(logger);

        // Act
        await sut.InvokeAsync(context, next);

        // Assert
        logger.Received().LogError(exception, exception.Message);
        context.Response.StatusCode.Should().Be(500);
    }

    [Test]
    public async Task InvokeAsync_NoExceptionOccors_ReturnsNormalResponse()
    {
        // Arrange
        var context = new DefaultHttpContext();
        RequestDelegate next = (context) => { context.Response.StatusCode = 200; return Task.CompletedTask; };
        var logger = Substitute.For<ILogger<ExceptionHandlingMiddleware>>();
        var sut = new ExceptionHandlingMiddleware(logger);

        // Act
        await sut.InvokeAsync(context, next);

        // Assert
        logger.DidNotReceiveWithAnyArgs().LogError(Arg.Any<Exception>(), message: default, args: default!);
        context.Response.StatusCode.Should().Be(200);
    }
}