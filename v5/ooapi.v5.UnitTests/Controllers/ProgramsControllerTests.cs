using Microsoft.AspNetCore.Mvc;
using ooapi.v5.Controllers;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;

namespace ooapi.v5.UnitTests.Controllers;

public class ProgramsControllerTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void ProgramsGet_ByPrimaryCode_ReturnsPrograms()
    {
        //arrange
        var sut = CreateSut(out var programsService, out var _, out var _);
        var primaryCodeParam = _fixture.Create<PrimaryCodeParam>();
        FilterParams? filterParams = null;
        PagingParams? pagingParams = null;
        var teachingLanguage = _fixture.Create<string?>();
        var programType = _fixture.Create<string?>();
        var qualificationAwarded = _fixture.Create<string?>();
        var levelOfQualification = _fixture.Create<string?>();
        var sector = _fixture.Create<string?>();
        var fieldsOfStudy = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Models.Program>();

        DataRequestParameters? dataRequestParameters = null;

        programsService.GetAll(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), out var errorResponse).Returns(response);

        //act
        var result = sut.ProgramsGet(primaryCodeParam, filterParams, pagingParams, teachingLanguage, programType, qualificationAwarded, levelOfQualification, sector, fieldsOfStudy, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var programs = result.Value as Pagination<Models.Program>;
        programs.Should().NotBeNull();
        programs.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.PrimaryCodeSearch.Should().Be(primaryCodeParam.primaryCode);
    }

    [Test]
    public void ProgramsGet_ByFilterParams_ReturnsPrograms()
    {
        //arrange
        var sut = CreateSut(out var programsService, out var _, out var _);
        PrimaryCodeParam? primaryCodeParam = null;
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var teachingLanguage = _fixture.Create<string?>();
        var programType = _fixture.Create<string?>();
        var qualificationAwarded = _fixture.Create<string?>();
        var levelOfQualification = _fixture.Create<string?>();
        var sector = _fixture.Create<string?>();
        var fieldsOfStudy = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Models.Program>();

        DataRequestParameters? dataRequestParameters = null;

        programsService.GetAll(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), out var errorResponse).Returns(response);

        //act
        var result = sut.ProgramsGet(primaryCodeParam, filterParams, pagingParams, teachingLanguage, programType, qualificationAwarded, levelOfQualification, sector, fieldsOfStudy, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var programs = result.Value as Pagination<Models.Program>;
        programs.Should().NotBeNull();
        programs.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public void ProgramsProgramIdCoursesGet_Success_ReturnsCourses()
    {
        //arrange
        var sut = CreateSut(out var _, out var coursesService, out var _);
        var programId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var teachingLevel = _fixture.Create<string?>();
        var level = _fixture.Create<string?>();
        var modeOfDelivery = _fixture.Create<List<string>?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Course>();

        DataRequestParameters? dataRequestParameters = null;

        coursesService.GetCoursesByProgramId(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), programId, out var errorResponse).Returns(response);

        //act
        var result = sut.ProgramsProgramIdCoursesGet(programId, filterParams, pagingParams, teachingLevel, level, modeOfDelivery, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var courses = result.Value as Pagination<Course>;
        courses.Should().NotBeNull();
        courses.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public void ProgramsProgramIdGet_Success_ReturnsProgram()
    {
        //arrange
        var sut = CreateSut(out var programsService, out var _, out var _);
        var programId = _fixture.Create<Guid>();
        var expand = _fixture.Create<List<string>>();
        var returnTimelineOverrides = _fixture.Create<bool?>();

        var response = new Models.Program();

        DataRequestParameters? dataRequestParameters = null;

        programsService.Get(programId, Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), out var errorResponse).Returns(response);

        //act
        var result = sut.ProgramsProgramIdGet(programId, expand, returnTimelineOverrides) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var program = result.Value as Models.Program;
        program.Should().NotBeNull();
        program.Should().Be(response);
    }

    [Test]
    public void ProgramsProgramIdOfferingsGet_Success_ReturnsProgramOfferings()
    {
        //arrange
        var sut = CreateSut(out var _, out var _, out var programOfferingService);
        var programId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var teachingLanguage = _fixture.Create<string?>();
        var modeOfStudy = _fixture.Create<string?>();
        var resultExpected = _fixture.Create<bool?>();
        var since = _fixture.Create<DateTime?>();
        var until = _fixture.Create<DateTime?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<ProgramOffering>();

        DataRequestParameters? dataRequestParameters = null;

        programOfferingService.GetAll(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), out var errorResponse).Returns(response);

        //act
        var result = sut.ProgramsProgramIdOfferingsGet(programId, filterParams, pagingParams, teachingLanguage, modeOfStudy, resultExpected, since, until, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var programOfferings = result.Value as Pagination<ProgramOffering>;
        programOfferings.Should().NotBeNull();
        programOfferings.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public void ProgramsProgramIdProgramsGet_Success_ReturnsPrograms()
    {
        //arrange
        var sut = CreateSut(out var programsService, out var _, out var _);
        var programId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var teachingLanguage = _fixture.Create<string?>();
        var programType = _fixture.Create<string?>();
        var qualificationAwarded = _fixture.Create<string?>();
        var levelOfQualification = _fixture.Create<string?>();
        var sector = _fixture.Create<string?>();
        var fieldsOfStudy = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Models.Program>();

        DataRequestParameters? dataRequestParameters = null;

        programsService.GetProgramsByProgramId(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), programId, out var errorResponse).Returns(response);

        //act
        var result = sut.ProgramsProgramIdProgramsGet(programId, filterParams, pagingParams, teachingLanguage, programType, qualificationAwarded, levelOfQualification, sector, fieldsOfStudy, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var programs = result.Value as Pagination<Models.Program>;
        programs.Should().NotBeNull();
        programs.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    private static ProgramsController CreateSut(out IProgramsService programsService, out ICoursesService coursesService, out IProgramOfferingService programOfferingService)
    {
        programsService = Substitute.For<IProgramsService>();
        coursesService = Substitute.For<ICoursesService>();
        programOfferingService = Substitute.For<IProgramOfferingService>();
        return new ProgramsController(programsService, coursesService, programOfferingService);
    }
}