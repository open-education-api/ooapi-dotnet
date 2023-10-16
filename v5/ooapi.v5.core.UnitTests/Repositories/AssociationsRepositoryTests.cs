using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class AssociationsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetAssociation_ReturnsAssociation_WhenAssociationExists()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var associationsRepository = new AssociationsRepository(dbContext);
        var associationId = _fixture.Create<Guid>();
        var association = _fixture.Build<Association>()
            .With(x => x.AssociationId, associationId)
            .Without(x => x.Person)
            .Without(x => x.ProgramOffering)
            .Without(x => x.CourseOffering)
            .Without(x => x.ComponentOffering)
            .Create();
        var db = Substitute.For<DbSet<Association>, IQueryable<Association>>();
        DbMockHelper.InitDb(db, new List<Association> { association }.AsQueryable());
        dbContext.Associations.Returns(db);

        // Act
        var result = associationsRepository.GetAssociation(associationId);

        // Assert
        Assert.That(association, Is.EqualTo(result));
    }

    [Test]
    public void GetAssociation_ReturnsNull_WhenAssociationDoesNotExist()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var associationsRepository = new AssociationsRepository(dbContext);
        var associationId = _fixture.Create<Guid>();
        var db = Substitute.For<DbSet<Association>, IQueryable<Association>>();
        DbMockHelper.InitDb(db, new List<Association> { }.AsQueryable());
        dbContext.Associations.Returns(db);

        // Act
        var result = associationsRepository.GetAssociation(associationId);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetAssociationsByPersonId_ReturnsAssociations_WhenAssociationsExist()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var associationsRepository = new AssociationsRepository(dbContext);
        var personId = _fixture.Create<Guid>();
        var associations = _fixture.Build<Association>()
            .With(x => x.PersonId, personId)
            .Without(x => x.Person)
            .Without(x => x.ProgramOffering)
            .Without(x => x.CourseOffering)
            .Without(x => x.ComponentOffering)
            .CreateMany(5);
        var db = Substitute.For<DbSet<Association>, IQueryable<Association>>();
        DbMockHelper.InitDb(db, associations.AsQueryable());
        dbContext.Associations.Returns(db);

        // Act
        var result = associationsRepository.GetAssociationsByPersonId(personId);

        // Assert
        CollectionAssert.AreEqual(associations, result);
    }

    [Test]
    public void GetAssociationsByPersonId_ReturnsEmptyList_WhenAssociationsDoNotExist()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var associationsRepository = new AssociationsRepository(dbContext);
        var personId = _fixture.Create<Guid>();
        var db = Substitute.For<DbSet<Association>, IQueryable<Association>>();
        DbMockHelper.InitDb(db, new List<Association>().AsQueryable());
        dbContext.Associations.Returns(db);

        // Act
        var result = associationsRepository.GetAssociationsByPersonId(personId);

        // Assert
        Assert.That(result.Count, Is.EqualTo(0));
    }
}