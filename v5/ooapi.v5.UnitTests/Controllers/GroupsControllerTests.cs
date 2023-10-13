using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ooapi.v5.Controllers;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;

namespace ooapi.v5.UnitTests.Controllers;

public class GroupsControllerTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GroupsGet_ByPrimaryCode_ReturnsGroups()
    {
        //arrange
        var sut = CreateSut(out var groupsService, out var _);
        var primaryCodeParam = _fixture.Create<PrimaryCodeParam>();
        FilterParams? filterParams = null;
        PagingParams? pagingParams = null;
        var groupType = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Group>();

        DataRequestParameters? dataRequestParameters = null;

        groupsService.GetAll(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), out var errorResponse).Returns(response);

        //act
        var result = sut.GroupsGet(primaryCodeParam, filterParams, pagingParams, groupType, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var groups = result.Value as Pagination<Group>;
        groups.Should().NotBeNull();
        groups.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.PrimaryCodeSearch.Should().Be(primaryCodeParam.primaryCode);
    }

    [Test]
    public void GroupsGet_ByFilterParams_ReturnsGroups()
    {
        //arrange
        var sut = CreateSut(out var groupsService, out var _);
        PrimaryCodeParam? primaryCodeParam = null;
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var groupType = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Group>();

        DataRequestParameters? dataRequestParameters = null;

        groupsService.GetAll(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), out var errorResponse).Returns(response);

        //act
        var result = sut.GroupsGet(primaryCodeParam, filterParams, pagingParams, groupType, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var groups = result.Value as Pagination<Group>;
        groups.Should().NotBeNull();
        groups.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public void GroupsGroupIdGet_Success_ReturnsGroup()
    {
        //arrange
        var sut = CreateSut(out var groupsService, out var _);
        var groupId = _fixture.Create<Guid>();
        var expand = _fixture.Create<List<string>>();

        var response = new Group();

        groupsService.Get(groupId, out var errorResponse).Returns(response);

        //act
        var result = sut.GroupsGroupIdGet(groupId, expand) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var group = result.Value as Group;
        group.Should().NotBeNull();
        group.Should().Be(response);
    }

    [Test]
    public void GroupsGroupIdPersonsGet_Success_ReturnsPersons()
    {
        //arrange
        var sut = CreateSut(out var _, out var personsService);
        var groupId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var affiliations = _fixture.Create<List<string>?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Person>();

        DataRequestParameters? dataRequestParameters = null;

        personsService.GetPersonsByGroupId(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), groupId, out var errorResponse).Returns(response);

        //act
        var result = sut.GroupsGroupIdPersonsGet(groupId, filterParams, pagingParams, affiliations, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var persons = result.Value as Pagination<Person>;
        persons.Should().NotBeNull();
        persons.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    private static GroupsController CreateSut(out IGroupsService groupsService, out IPersonsService personsService)
    {
        groupsService = Substitute.For<IGroupsService>();
        personsService = Substitute.For<IPersonsService>();
        return new GroupsController(groupsService, personsService);
    }
}