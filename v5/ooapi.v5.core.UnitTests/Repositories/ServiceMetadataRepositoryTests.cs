using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class ServiceMetadataRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetServiceMetadata_WhenServiceExists_ReturnsService()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var serviceMetadataRepository = new ServiceMetadataRepository(dbContext);
        var services = _fixture.Build<Service>()
            .CreateMany(4);
        var db = Substitute.For<DbSet<Service>, IQueryable<Service>>();
        DbMockHelper.InitDb(db, services.AsQueryable());
        dbContext.Services.Returns(db);

        // Act
        var result = serviceMetadataRepository.GetServiceMetadataAsync();

        // Assert
        Assert.That(result, Is.EqualTo(services.First()));
    }
}