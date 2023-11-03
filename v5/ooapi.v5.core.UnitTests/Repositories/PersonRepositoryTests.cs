using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class PersonsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
     public async Task GetPerson_WhenPersonExists_ReturnsPerson()
    {
        // Arrange
        var personId = _fixture.Create<Guid>();
        var person = _fixture.Build<Person>()
            .With(x => x.PersonId, personId)
            .Without(x => x.Address)
            .Without(x => x.Groups)
            .Create();
        var persons = new List<Person> { person }.AsQueryable();

        var db = persons.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Persons.Returns(db);
        var personsRepository = new PersonsRepository(dbContext);

        // Act
        var result = await personsRepository.GetPersonAsync(personId);

        // Assert
        Assert.That(result, Is.EqualTo(person));
    }

    [Test]
    public async Task GetPerson_WhenPersonNotFound_ReturnsNull()
    {
        // Arrange
        var personId = _fixture.Create<Guid>();
        var person = _fixture.Build<Person>()
            .With(x => x.PersonId, personId)
            .Without(x => x.Address)
            .Without(x => x.Groups)
            .Create();
        var persons = new List<Person> { person }.AsQueryable();

        var db = persons.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Persons.Returns(db);
        var personsRepository = new PersonsRepository(dbContext);

        // Act
        var result = await personsRepository.GetPersonAsync(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }
}