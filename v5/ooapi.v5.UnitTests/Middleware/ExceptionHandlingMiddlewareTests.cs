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
        Task next(HttpContext x) => throw exception;
        var logger = Substitute.For<ILogger<ExceptionHandlingMiddleware>>();
        var sut = new ExceptionHandlingMiddleware(logger);

        // Act
        await sut.InvokeAsync(context, next);

        // Assert
        logger.Received().Log(LogLevel.Error,
                Arg.Any<EventId>(),
                Arg.Any<Arg.AnyType>(),
                Arg.Any<Exception>(),
                Arg.Any<Func<Arg.AnyType, Exception?, string>>());

        context.Response.StatusCode.Should().Be(500);
    }

    [Test]
    public async Task InvokeAsync_NoExceptionOccors_ReturnsNormalResponse()
    {
        // Arrange
        var context = new DefaultHttpContext();
        static Task next(HttpContext context) { context.Response.StatusCode = 200; return Task.CompletedTask; }
        var logger = Substitute.For<ILogger<ExceptionHandlingMiddleware>>();
        var sut = new ExceptionHandlingMiddleware(logger);

        // Act
        await sut.InvokeAsync(context, next);

        // Assert
        logger.DidNotReceiveWithAnyArgs().Log(LogLevel.Error,
                Arg.Any<EventId>(),
                Arg.Any<Arg.AnyType>(),
                Arg.Any<Exception>(),
                Arg.Any<Func<Arg.AnyType, Exception?, string>>());
        context.Response.StatusCode.Should().Be(200);
    }
}