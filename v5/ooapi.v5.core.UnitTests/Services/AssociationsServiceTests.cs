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
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IAssociationsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new AssociationsService(dbContext, repository, userRequestContext);
        var associationId = _fixture.Create<Guid>();

        var expected = new Association();
        repository.GetAssociation(associationId).Returns(expected);

        // Act
        var result = sut.Get(associationId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetAssociation(associationId);
    }


    [Test]
    public void GetAssociationsByPersonId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IAssociationsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new AssociationsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var personId = _fixture.Create<Guid>();

        var associations = new List<Association>();
        repository.GetAssociationsByPersonId(personId).Returns(associations);

        // Act
        var result = sut.GetAssociationsByPersonId(dataRequestParameters, personId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Association>>());
        repository.Received(1).GetAssociationsByPersonId(personId);
    }
}