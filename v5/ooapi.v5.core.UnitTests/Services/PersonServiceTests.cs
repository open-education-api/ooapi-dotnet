using AutoFixture;
using NSubstitute;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class PersonsServiceTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public async Task GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IPersonsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new PersonsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();

        var expected = new Pagination<Person>();
        repository.GetAllOrderedByAsync(dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetAllAsync(dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetAllOrderedByAsync(dataRequestParameters);
    }
    
    [Test]
    public async Task Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IPersonsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new PersonsService(dbContext, repository, userRequestContext);
        var personId = _fixture.Create<Guid>();

        var expected = new Person();
        repository.GetPersonAsync(personId).Returns(expected);

        // Act
        var result = await sut.GetAsync(personId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetPersonAsync(personId);
    }

    [Test]
    public async Task GetPersonsByGroupId_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IPersonsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new PersonsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var GroupId = _fixture.Create<Guid>();

        var persons = new Pagination<Person>();
        repository.GetPersonsByGroupIdAsync(GroupId, dataRequestParameters).Returns(persons);

        // Act
        var result = await sut.GetPersonsByGroupIdAsync(dataRequestParameters, GroupId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Person>>());
        await repository.Received(1).GetPersonsByGroupIdAsync(GroupId, dataRequestParameters);
    }
}