using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class PersonsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
     public void GetPerson_ReturnsPerson_WhenPersonExists()
    {
        // Arrange
        var personId = _fixture.Create<Guid>();
        var person = _fixture.Build<Person>()
            .With(x => x.PersonId, personId)
            .Without(x => x.Address)
            .Without(x => x.Groups)
            .Create();
        var persons = new List<Person> { person }.AsQueryable();

        var db = Substitute.For<DbSet<Person>, IQueryable<Person>>();
        DbMockHelper.InitDb(db, persons);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Persons.Returns(db);
        var personsRepository = new PersonsRepository(dbContext);

        // Act
        var result = personsRepository.GetPerson(personId);

        // Assert
        Assert.That(result, Is.EqualTo(person));
    }

    [Test]
    public void GetPerson_ReturnsNull_WhenPersonDoesNotFound()
    {
        // Arrange
        var personId = _fixture.Create<Guid>();
        var person = _fixture.Build<Person>()
            .With(x => x.PersonId, personId)
            .Without(x => x.Address)
            .Without(x => x.Groups)
            .Create();
        var persons = new List<Person> { person }.AsQueryable();

        var db = Substitute.For<DbSet<Person>, IQueryable<Person>>();
        DbMockHelper.InitDb(db, persons);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Persons.Returns(db);
        var personsRepository = new PersonsRepository(dbContext);

        // Act
        var result = personsRepository.GetPerson(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }
}