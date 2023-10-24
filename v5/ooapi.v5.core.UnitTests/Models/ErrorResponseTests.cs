using AutoFixture;
using FluentAssertions;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Models;

[TestFixture]
public class ErrorResponseTests
{
    [TestCase(400, "test", "Bad request")]
    [TestCase(401, "test", "Unauthorized")]
    [TestCase(403, "test", "Forbidden")]
    [TestCase(404, "test", "Not Found")]
    [TestCase(405, "test", "Method not allowed")]
    [TestCase(429, "test", "Too many requests")]
    [TestCase(500, "test", "Internal Server Error")]
    public void ErrorResponse_WithStatusCodeAndDetail_SetsStatusCodeDetailAndTitle(int statusCode, string detail, string title)
    {
        // Arrange

        // Act
        var sut = new ErrorResponse(statusCode, detail);

        // Assert
        sut.Detail.Should().Be(detail);
        sut.Status.Should().Be(statusCode.ToString());
        sut.Title.Should().Be(title);
    }
}