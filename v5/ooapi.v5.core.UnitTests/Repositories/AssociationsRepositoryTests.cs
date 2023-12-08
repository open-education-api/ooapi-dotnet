using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using NUnit.Framework.Legacy;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class AssociationsRepositoryTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public async Task GetAssociation_WhenAssociationExists_ReturnsAssociation()
    {
        // Arrange
        var associationId = _fixture.Create<Guid>();
        var association = _fixture.Build<Association>()
            .With(x => x.AssociationId, associationId)
            .Without(x => x.Person)
            .Without(x => x.ProgramOffering)
            .Without(x => x.CourseOffering)
            .Without(x => x.ComponentOffering)
            .CreateMany(1)
            .AsQueryable();

        var db = association.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Associations.Returns(db);
        var associationsRepository = new AssociationsRepository(dbContext);

        // Act
        var result = await associationsRepository.GetAssociationAsync(associationId);

        // Assert
        Assert.That(association.First(), Is.EqualTo(result));
    }

    [Test]
    public async Task GetAssociation_WhenAssociationDoesNotExist_ReturnsNull()
    {
        // Arrange
        var associationId = _fixture.Create<Guid>();

        var association = new List<Association>().AsQueryable();
        var db = association.BuildMockDbSet();

        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Associations.Returns(db);
        var associationsRepository = new AssociationsRepository(dbContext);

        // Act
        var result = await associationsRepository.GetAssociationAsync(associationId);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetAssociationsByPersonId_WhenAssociationsExist_ReturnsAssociations()
    {
        // Arrange
        var personId = _fixture.Create<Guid>();
        var associations = _fixture.Build<Association>()
            .With(x => x.PersonId, personId)
            .Without(x => x.Person)
            .Without(x => x.ProgramOffering)
            .Without(x => x.CourseOffering)
            .Without(x => x.ComponentOffering)
            .CreateMany(5)
            .AsQueryable();

        var db = associations.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Associations.Returns(db);
        var associationsRepository = new AssociationsRepository(dbContext);

        // Act
        var result = await associationsRepository.GetAssociationsByPersonIdAsync(personId, new DataRequestParameters());

        // Assert
        Assert.That(result.Items, Is.EqualTo(associations).AsCollection);
    }

    [Test]
    public async Task GetAssociationsByPersonId_WhenAssociationsDoNotExist_ReturnsEmptyList()
    {
        // Arrange
        var personId = _fixture.Create<Guid>();

        var association = new List<Association>().AsQueryable();
        var db = association.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Associations.Returns(db);
        var associationsRepository = new AssociationsRepository(dbContext);

        // Act
        var result = await associationsRepository.GetAssociationsByPersonIdAsync(personId, new DataRequestParameters());

        // Assert
        Assert.That(result.Items, Is.Empty);
    }
}