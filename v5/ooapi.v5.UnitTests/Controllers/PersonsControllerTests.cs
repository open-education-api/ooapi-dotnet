using Microsoft.AspNetCore.Mvc;
using ooapi.v5.Controllers;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;

namespace ooapi.v5.UnitTests.Controllers;

public class PersonsControllerTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public async Task PersonsGet_ByPrimaryCode_ReturnsPersons()
    {
        //arrange
        var sut = CreateSut(out var personsService, out var _, out var _);
        var primaryCodeParam = _fixture.Create<PrimaryCodeParam>();
        FilterParams? filterParams = null;
        PagingParams? pagingParams = null;
        var affiliations = _fixture.Create<List<string>?>();
        var sort = _fixture.Create<string?>();

        var response = Substitute.For<Pagination<Person>>();

        DataRequestParameters? dataRequestParameters = null;

        personsService.GetAllAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.PersonsGetAsync(primaryCodeParam, filterParams, pagingParams, affiliations, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var persons = result.Value as Pagination<Person>;
        persons.Should().NotBeNull();
        persons.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.PrimaryCodeSearch.Should().Be(primaryCodeParam.primaryCode);
    }

    [Test]
    public async Task PersonsGet_ByFilterParams_ReturnsPersons()
    {
        //arrange
        var sut = CreateSut(out var personsService, out var _, out var _);
        PrimaryCodeParam? primaryCodeParam = null;
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var affiliations = _fixture.Create<List<string>?>();
        var sort = _fixture.Create<string?>();

        var response = Substitute.For<Pagination<Person>>();

        DataRequestParameters? dataRequestParameters = null;

        personsService.GetAllAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.PersonsGetAsync(primaryCodeParam, filterParams, pagingParams, affiliations, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var persons = result.Value as Pagination<Person>;
        persons.Should().NotBeNull();
        persons.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public async Task PersonsPersonIdAssociationsGet_Success_ReturnsAssociations()
    {
        //arrange
        var sut = CreateSut(out var _, out var associationsService, out var _);
        var personId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var associationType = _fixture.Create<string?>();
        var role = _fixture.Create<string?>();
        var state = _fixture.Create<string?>();
        var resultState = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = Substitute.For<Pagination<Association>>();

        DataRequestParameters? dataRequestParameters = null;

        associationsService.GetAssociationsByPersonIdAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), personId, Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.PersonsPersonIdAssociationsGetAsync(personId, filterParams, pagingParams, associationType, role, state, resultState, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var asscociations = result.Value as Pagination<Association>;
        asscociations.Should().NotBeNull();
        asscociations.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public async Task PersonsPersonIdGet_Success_ReturnsPerson()
    {
        //arrange
        var sut = CreateSut(out var personsService, out var _, out var _);
        var personId = _fixture.Create<Guid>();

        var response = new Person();

        personsService.GetAsync(personId, Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.PersonsPersonIdGetAsync(personId) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var person = result.Value as Person;
        person.Should().NotBeNull();
        person.Should().Be(response);
    }

    [Test]
    public async Task PersonsPersonIdGroupsGet_Success_ReturnsGroups()
    {
        //arrange
        var sut = CreateSut(out var _, out var _, out var groupsService);
        var personId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var groupType = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = Substitute.For<Pagination<Group>>();

        DataRequestParameters? dataRequestParameters = null;

        groupsService.GetGroupsByPersonIdAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), personId, Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.PersonsPersonIdGroupsGetAsync(personId, filterParams, pagingParams, groupType, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var groups = result.Value as Pagination<Group>;
        groups.Should().NotBeNull();
        groups.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    private static PersonsController CreateSut(out IPersonsService personsService, out IAssociationsService associationsService, out IGroupsService groupsService)
    {
        personsService = Substitute.For<IPersonsService>();
        associationsService = Substitute.For<IAssociationsService>();
        groupsService = Substitute.For<IGroupsService>();
        return new PersonsController(personsService, associationsService, groupsService);
    }
}