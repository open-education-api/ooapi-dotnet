using Microsoft.AspNetCore.Mvc;
using ooapi.v5.Controllers;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;

namespace ooapi.v5.UnitTests.Controllers;

public class CoursesControllerTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public async Task CoursesCourseIdComponentsGet_Success_ReturnsComponents()
    {
        //arrange
        var sut = CreateSut(out var componentsService, out var _);
        var courseId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var teachingLanguage = _fixture.Create<string?>();
        var componentType = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Component>();

        DataRequestParameters? dataRequestParameters = null;

        componentsService.GetComponentsByCourseIdAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), courseId, Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.CoursesCourseIdComponentsGetAsync(courseId, filterParams, pagingParams, teachingLanguage, componentType, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var components = result.Value as Pagination<Component>;
        components.Should().NotBeNull();
        components.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public async Task CoursesCourseIdGet_Success_ReturnsCourse()
    {
        //arrange
        var sut = CreateSut(out var _, out var coursesService);
        var courseId = _fixture.Create<Guid>();
        var expand = _fixture.Create<List<string>>();
        var returnTimelineOverrides = _fixture.Create<bool?>();

        var response = new Course();

        coursesService.GetAsync(courseId, Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.CoursesCourseIdGetAsync(courseId, expand, returnTimelineOverrides) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var course = result.Value as Course;
        course.Should().NotBeNull();
        course.Should().Be(response);
    }

    [Test]
    public async Task CoursesGet_ByPrimaryCode_ReturnsCourses()
    {
        //arrange
        var sut = CreateSut(out var _, out var coursesService);
        var primaryCodeParam = _fixture.Create<PrimaryCodeParam>();
        FilterParams? filterParams = null;
        PagingParams? pagingParams = null;
        var teachingLanguage = _fixture.Create<string?>();
        var level = _fixture.Create<string?>();
        var modeOfDelivery = _fixture.Create<List<string>?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Course>();

        DataRequestParameters? dataRequestParameters = null;

        coursesService.GetAllAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.CoursesGetAsync(primaryCodeParam, filterParams, pagingParams, teachingLanguage, level, modeOfDelivery, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var courses = result.Value as Pagination<Course>;
        courses.Should().NotBeNull();
        courses.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.PrimaryCodeSearch.Should().Be(primaryCodeParam.primaryCode);
    }

    [Test]
    public async Task CoursesGet_ByFilterParams_ReturnsCourses()
    {
        //arrange
        var sut = CreateSut(out var componentsService, out var coursesService);
        PrimaryCodeParam? primaryCodeParam = null;
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var teachingLanguage = _fixture.Create<string?>();
        var level = _fixture.Create<string?>();
        var modeOfDelivery = _fixture.Create<List<string>?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Course>();

        DataRequestParameters? dataRequestParameters = null;

        coursesService.GetAllAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.CoursesGetAsync(primaryCodeParam, filterParams, pagingParams, teachingLanguage, level, modeOfDelivery, sort) as OkObjectResult;

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

    private static CoursesController CreateSut(out IComponentsService componentsService, out ICoursesService coursesService)
    {
        componentsService = Substitute.For<IComponentsService>();
        coursesService = Substitute.For<ICoursesService>();
        return new CoursesController(componentsService, coursesService);
    }
}