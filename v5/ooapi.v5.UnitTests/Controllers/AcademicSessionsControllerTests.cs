using Microsoft.AspNetCore.Mvc;
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
    public async Task AcademicSessionsGet_ByPrimaryCode_ReturnsAcademicSessions()
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

        academicSessionsService.GetAllAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), academicSessionType, Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.AcademicSessionsGetAsync(primaryCodeParam, filterParams, pagingParams, academicSessionType, parent, year, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var academicSessions = result.Value as Pagination<AcademicSession>;
        academicSessions.Should().NotBeNull();
        academicSessions.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.PrimaryCodeSearch.Should().Be(primaryCodeParam.primaryCode);
    }

    [Test]
    public async Task AcademicSessionsGet_ByFilterParams_ReturnsAcademicSessions()
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

        academicSessionsService.GetAllAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), academicSessionType, Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.AcademicSessionsGetAsync(primaryCodeParam, filterParams, pagingParams, academicSessionType, parent, year, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var academicSessions = result.Value as Pagination<AcademicSession>;
        academicSessions.Should().NotBeNull();
        academicSessions.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public async Task AcademicSessionsAcademicSessionIdGet_Success_ReturnsAcademicSession()
    {
        //arrange
        var sut = CreateSut(out var academicSessionsService);
        var academicSessionId = _fixture.Create<Guid>();
        var expand = _fixture.Create<List<string>>();

        var response = new AcademicSession();

        DataRequestParameters? dataRequestParameters = null;

        academicSessionsService.GetAsync(academicSessionId, Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.AcademicSessionsAcademicSessionIdGetAsync(academicSessionId, expand) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

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