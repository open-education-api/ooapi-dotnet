using AutoFixture;
using NSubstitute;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class AssociationsServiceTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public async Task Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IAssociationsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new AssociationsService(dbContext, repository, userRequestContext);
        var associationId = _fixture.Create<Guid>();

        var expected = new Association();
        repository.GetAssociationAsync(associationId).Returns(expected);

        // Act
        var result = await sut.GetAsync(associationId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetAssociationAsync(associationId);
    }


    [Test]
    public async Task GetAssociationsByPersonId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IAssociationsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new AssociationsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var personId = _fixture.Create<Guid>();

        var expected = Substitute.For<Pagination<Association>>();
        repository.GetAssociationsByPersonIdAsync(personId, dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetAssociationsByPersonIdAsync(dataRequestParameters, personId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Association>>());
        await repository.Received(1).GetAssociationsByPersonIdAsync(personId, dataRequestParameters);
    }
}