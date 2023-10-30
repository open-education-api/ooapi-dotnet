using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class ServiceMetadataRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public async Task GetServiceMetadata_WhenServiceExists_ReturnsService()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var serviceMetadataRepository = new ServiceMetadataRepository(dbContext);
        var services = _fixture.Build<Service>()
            .CreateMany(4)
            .AsQueryable();

        var db = services.BuildMockDbSet();
        dbContext.Services.Returns(db);

        // Act
        var result = await serviceMetadataRepository.GetServiceMetadataAsync();

        // Assert
        Assert.That(result, Is.EqualTo(services.First()));
    }
}