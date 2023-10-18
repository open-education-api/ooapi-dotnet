using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using ooapi.v5.core.Security;

namespace ooapi.v5.core.UnitTests.Security;

[TestFixture]
public class UserRequestContextTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void UserId_WithUserIdHeader_ReturnsUserIdFromHeader()
    {
        // Arrange
        var userId = _fixture.Create<string>();
        var sut = CreateSutWithHeader(new(nameof(userId), userId));

        // Act
        var result = sut.UserId;

        // Assert
        result.Should().Be(userId);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void UserId_WithIsStudentHeader_ReturnsIsStudentFromHeader(bool isStudent)
    {
        // Arrange
        var sut = CreateSutWithHeader(new(nameof(isStudent), isStudent.ToString()));

        // Act
        var result = sut.IsStudent;

        // Assert
        result.Should().Be(isStudent);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void UserId_WithIsEmployeeHeader_ReturnsIsEmployeeFromHeader(bool isEmployee)
    {
        // Arrange
        var sut = CreateSutWithHeader(new(nameof(isEmployee), isEmployee.ToString()));

        // Act
        var result = sut.IsEmployee;

        // Assert
        result.Should().Be(isEmployee);
    }

    [Test]
    public void UserId_WithBivvHeader_ReturnsBivvFromHeader()
    {
        // Arrange
        var bivv = _fixture.Create<string>();
        var sut = CreateSutWithHeader(new(nameof(bivv), bivv));

        // Act
        var result = sut.Bivv;

        // Assert
        result.Should().Be(bivv);
    }

    [Test]
    public void UserId_WithoutBivvHeader_ReturnsBivvLaag()
    {
        // Arrange
        var sut = CreateSutWithHeader();

        // Act
        var result = sut.Bivv;

        // Assert
        result.Should().Be("laag");
    }

    [TestCase("localhost", true)]
    [TestCase("otherhost", false)]
    public void IsLocal_WithDifferentHosts_ReturnsTrueForLocalhost(string host, bool expectedResult)
    {
        // Arrange
        var httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();
        var context = new DefaultHttpContext();
        httpContextAccessorMock.HttpContext.Returns(context);
        httpContextAccessorMock.HttpContext!.Request.Host = new HostString(host, 80);
        var sut = CreateSut(httpContextAccessorMock);

        // Act
        var result = sut.IsLocal;

        // Assert
        result.Should().Be(expectedResult);
    }

    private static UserRequestContext CreateSutWithHeader(Tuple<string, string>? header = null)
    {
        var httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();
        var context = new DefaultHttpContext();
        if (header != null)
        {
            context.Request.Headers[header.Item1] = header.Item2;
        }
        httpContextAccessorMock.HttpContext.Returns(context);
        return new UserRequestContext(httpContextAccessorMock);
    }

    private static UserRequestContext CreateSut(IHttpContextAccessor httpContextAccessorMock)
    {
        return new UserRequestContext(httpContextAccessorMock);
    }
}