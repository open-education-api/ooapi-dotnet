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

public class AcademicSessionsControllerTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void AcademicSessionsGet_ByPrimaryCode_ReturnsAcademicSessions()
    {
        //arrange
        var sut = CreateSut(out var academicSessionsService);
        var primaryCodeParam = _fixture.Create<PrimaryCodeParam>();
        FilterParams? filterParams = null;
        PagingParams? pagingParams = null;
        var academicSessionType = _fixture.Create<string?>();
        var parent = _fixture.Create<Guid?>();
        var year = _fixture.Create<Guid?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<AcademicSession>();

        DataRequestParameters? dataRequestParameters = null;

        academicSessionsService.GetAll(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), out var errorResponse, academicSessionType).Returns(response);

        //act
        var result = sut.AcademicSessionsGet(primaryCodeParam, filterParams, pagingParams, academicSessionType, parent, year, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var academicSessions = result.Value as Pagination<AcademicSession>;
        academicSessions.Should().NotBeNull();
        academicSessions.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.PrimaryCodeSearch.Should().Be(primaryCodeParam.primaryCode);
    }

    [Test]
    public void AcademicSessionsGet_ByFilterParams_ReturnsAcademicSessions()
    {
        //arrange
        var sut = CreateSut(out var academicSessionsService);
        PrimaryCodeParam? primaryCodeParam = null;
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var academicSessionType = _fixture.Create<string?>();
        var parent = _fixture.Create<Guid?>();
        var year = _fixture.Create<Guid?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<AcademicSession>();

        DataRequestParameters? dataRequestParameters = null;

        academicSessionsService.GetAll(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), out var errorResponse, academicSessionType).Returns(response);

        //act
        var result = sut.AcademicSessionsGet(primaryCodeParam, filterParams, pagingParams, academicSessionType, parent, year, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var academicSessions = result.Value as Pagination<AcademicSession>;
        academicSessions.Should().NotBeNull();
        academicSessions.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public void AcademicSessionsAcademicSessionIdGet_Success_ReturnsAcademicSession()
    {
        //arrange
        var sut = CreateSut(out var academicSessionsService);
        var academicSessionId = _fixture.Create<Guid>();
        var expand = _fixture.Create<List<string>>();

        var response = new AcademicSession();

        DataRequestParameters? dataRequestParameters = null;

        academicSessionsService.Get(academicSessionId, Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), out var errorResponse).Returns(response);

        //act
        var result = sut.AcademicSessionsAcademicSessionIdGet(academicSessionId, expand) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var academicSessions = result.Value as AcademicSession;
        academicSessions.Should().NotBeNull();
        academicSessions.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.Expand.Should().BeEquivalentTo(expand);
    }

    private static AcademicSessionsController CreateSut(out IAcademicSessionsService academicSessionsService)
    {
        academicSessionsService = Substitute.For<IAcademicSessionsService>();
        return new AcademicSessionsController(academicSessionsService);
    }
}