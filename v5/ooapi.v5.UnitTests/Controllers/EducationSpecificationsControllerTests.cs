using Microsoft.AspNetCore.Mvc;
using ooapi.v5.Controllers;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;

namespace ooapi.v5.UnitTests.Controllers;

public class EducationSpecificationsControllerTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public async Task EducationSpecificationsEducationSpecificationIdCoursesGet_Success_ReturnsCourses()
    {
        //arrange
        var sut = CreateSut(out var _, out var coursesService, out var _);
        var educationSpecificationId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var teachingLanguage = _fixture.Create<string?>();
        var level = _fixture.Create<string?>();
        var modeOfDelivery = _fixture.Create<List<string>?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Course>();

        DataRequestParameters? dataRequestParameters = null;

        coursesService.GetCoursesByEducationSpecificationIdAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), educationSpecificationId, Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.EducationSpecificationsEducationSpecificationIdCoursesGetAsync(educationSpecificationId, filterParams, pagingParams, teachingLanguage, level, modeOfDelivery, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var courses = result.Value as Pagination<Course>;
        courses.Should().NotBeNull();
        courses.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public async Task EducationSpecificationsEducationSpecificationIdEducationSpecificationsGet_Success_ReturnsEducationSpecifications()
    {
        //arrange
        var sut = CreateSut(out var educationSpecificationsService, out var _, out var _);
        var educationSpecificationId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<EducationSpecification>();

        DataRequestParameters? dataRequestParameters = null;

        educationSpecificationsService.GetEducationSpecificationsByEducationSpecificationIdAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), educationSpecificationId, Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.EducationSpecificationsEducationSpecificationIdEducationSpecificationsGetAsync(educationSpecificationId, filterParams, pagingParams, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var educationSpecifications = result.Value as Pagination<EducationSpecification>;
        educationSpecifications.Should().NotBeNull();
        educationSpecifications.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public async Task EducationSpecificationsEducationSpecificationIdGet_Success_ReturnsEducationSpecification()
    {
        //arrange
        var sut = CreateSut(out var educationSpecificationsService, out var _, out var _);
        var educationSpecificationId = _fixture.Create<Guid>();
        var expand = _fixture.Create<List<string>>();
        var returnTimelineOverrides = _fixture.Create<bool?>();

        var response = new EducationSpecification();

        DataRequestParameters? dataRequestParameters = null;

        educationSpecificationsService.GetAsync(educationSpecificationId, Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.EducationSpecificationsEducationSpecificationIdGetAsync(educationSpecificationId, returnTimelineOverrides, expand) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var educationSpecification = result.Value as EducationSpecification;
        educationSpecification.Should().NotBeNull();
        educationSpecification.Should().Be(response);
    }

    [Test]
    public async Task EducationSpecificationsEducationSpecificationIdProgramsGet_Success_ReturnsEducationSpecifications()
    {
        //arrange
        var sut = CreateSut(out var _, out var _, out var programsService);
        var educationSpecificationId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var teachingLanguage = _fixture.Create<string?>();
        var programType = _fixture.Create<string?>();
        var qualificationAwarded = _fixture.Create<string?>();
        var levelOfQualification = _fixture.Create<string?>();
        var sector = _fixture.Create<string?>();
        var fieldsOfStudy = _fixture.Create<string?>();
        var crohoCreboCode = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Models.Program>();

        DataRequestParameters? dataRequestParameters = null;

        programsService.GetProgramsByEducationSpecificationIdAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), educationSpecificationId, Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.EducationSpecificationsEducationSpecificationIdProgramsGetAsync(educationSpecificationId, filterParams, pagingParams, teachingLanguage, programType, qualificationAwarded, levelOfQualification, sector, fieldsOfStudy, crohoCreboCode, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var programs = result.Value as Pagination<Models.Program>;
        programs.Should().NotBeNull();
        programs.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public async Task EducationSpecificationsGet_Success_ReturnsEducationSpecifications()
    {
        //arrange
        var sut = CreateSut(out var educationSpecificationsService, out var _, out var _);
        var primaryCodeParam = _fixture.Create<PrimaryCodeParam>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var educationSpecificationType = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<EducationSpecification>();

        DataRequestParameters? dataRequestParameters = null;

        educationSpecificationsService.GetAllAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.EducationSpecificationsGetAsync(primaryCodeParam, filterParams, pagingParams, educationSpecificationType, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var educationSpecifications = result.Value as Pagination<EducationSpecification>;
        educationSpecifications.Should().NotBeNull();
        educationSpecifications.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    private static EducationSpecificationsController CreateSut(out IEducationSpecificationsService educationSpecificationsService, out ICoursesService coursesService, out IProgramsService programsService)
    {
        educationSpecificationsService = Substitute.For<IEducationSpecificationsService>();
        coursesService = Substitute.For<ICoursesService>();
        programsService = Substitute.For<IProgramsService>();
        return new EducationSpecificationsController(educationSpecificationsService, coursesService, programsService);
    }
}