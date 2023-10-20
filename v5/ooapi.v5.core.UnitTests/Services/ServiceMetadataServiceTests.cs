using NSubstitute;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class ServiceMetadatasServiceTests
{
    [Test]
    public void Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IServiceMetadataRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ServiceMetadataService(dbContext, repository, userRequestContext);

        var expected = new Service();
        repository.GetServiceMetadata().Returns(expected);

        // Act
        var result = sut.Get();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetServiceMetadata();
    }
}