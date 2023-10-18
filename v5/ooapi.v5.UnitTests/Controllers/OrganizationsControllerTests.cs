using Microsoft.AspNetCore.Mvc;
using ooapi.v5.Controllers;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Enums;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;

namespace ooapi.v5.UnitTests.Controllers;

public class OrganizationsControllerTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void OrganizationsGet_Success_ReturnsOrganizations()
    {
        //arrange
        var sut = CreateSut(out var organizationsService, out var _, out var _, out var _, out var _, out var _);
        var primaryCodeParam = _fixture.Create<PrimaryCodeParam>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var organizationType = _fixture.Create<OrganizationType?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Organization>();

        DataRequestParameters? dataRequestParameters = null;

        organizationsService.GetAll(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), out var errorResponse).Returns(response);

        //act
        var result = sut.OrganizationsGet(primaryCodeParam, filterParams, pagingParams, organizationType, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var organizations = result.Value as Pagination<Organization>;
        organizations.Should().NotBeNull();
        organizations.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public void OrganizationsOrganizationIdComponentsGet_Success_ReturnsComponents()
    {
        //arrange
        var sut = CreateSut(out var _, out var componentsService, out var _, out var _, out var _, out var _);
        var organizationId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var teachingLanguage = _fixture.Create<string?>();
        var componentType = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Component>();

        DataRequestParameters? dataRequestParameters = null;

        componentsService.GetComponentsByOrganizationId(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), organizationId, out var errorResponse).Returns(response);

        //act
        var result = sut.OrganizationsOrganizationIdComponentsGet(organizationId, filterParams, pagingParams, teachingLanguage, componentType, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var components = result.Value as Pagination<Component>;
        components.Should().NotBeNull();
        components.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public void OrganizationsOrganizationIdCoursesGet_Success_ReturnsCourses()
    {
        //arrange
        var sut = CreateSut(out var _, out var _, out var coursesService, out var _, out var _, out var _);
        var organizationId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var teachingLanguage = _fixture.Create<string?>();
        var level = _fixture.Create<string?>();
        var modeOfDelivery = _fixture.Create<List<string>?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Course>();

        DataRequestParameters? dataRequestParameters = null;

        coursesService.GetCoursesByOrganizationId(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), organizationId, out var errorResponse).Returns(response);

        //act
        var result = sut.OrganizationsOrganizationIdCoursesGet(organizationId, filterParams, pagingParams, teachingLanguage, level, modeOfDelivery, sort) as OkObjectResult;

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
    public void OrganizationsOrganizationIdEducationSpecificationsGet_Success_ReturnsEducationSpecifications()
    {
        //arrange
        var sut = CreateSut(out var _, out var _, out var _, out var educationSpecificationsService, out var _, out var _);
        var organizationId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var educationSpecificationType = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<EducationSpecification>();

        DataRequestParameters? dataRequestParameters = null;

        educationSpecificationsService.GetEducationSpecificationsByOrganizationId(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), organizationId, out var errorResponse).Returns(response);

        //act
        var result = sut.OrganizationsOrganizationIdEducationSpecificationsGet(organizationId, filterParams, pagingParams, educationSpecificationType, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var eductionSpecifactions = result.Value as Pagination<EducationSpecification>;
        eductionSpecifactions.Should().NotBeNull();
        eductionSpecifactions.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public void OrganizationsOrganizationIdGet_Success_ReturnsOrganization()
    {
        //arrange
        var sut = CreateSut(out var organizationsService, out var _, out var _, out var _, out var _, out var _);
        var organizationId = _fixture.Create<Guid>();
        var expand = _fixture.Create<List<string>?>();

        var response = new Organization();

        DataRequestParameters? dataRequestParameters = null;

        organizationsService.Get(organizationId, Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), out var errorResponse).Returns(response);

        //act
        var result = sut.OrganizationsOrganizationIdGet(organizationId, expand) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var organization = result.Value as Organization;
        organization.Should().NotBeNull();
        organization.Should().Be(response);
    }

    [Test]
    public void OrganizationsOrganizationIdGroupsGet_Success_ReturnsGroups()
    {
        //arrange
        var sut = CreateSut(out var _, out var _, out var _, out var _, out var groupsService, out var _);
        var organizationId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var groupType = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Group>();

        DataRequestParameters? dataRequestParameters = null;

        groupsService.GetGroupsByOrganizationId(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), organizationId, out var errorResponse).Returns(response);

        //act
        var result = sut.OrganizationsOrganizationIdGroupsGet(organizationId, filterParams, pagingParams, groupType, sort) as OkObjectResult;

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
    public void OrganizationsOrganizationIdProgramsGet_Success_ReturnsPrograms()
    {
        //arrange
        var sut = CreateSut(out var _, out var _, out var _, out var _, out var _, out var programsService);
        var organizationId = _fixture.Create<Guid>();
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

        programsService.GetProgramsByOrganizationId(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), organizationId, out var errorResponse).Returns(response);

        //act
        var result = sut.OrganizationsOrganizationIdProgramsGet(organizationId, filterParams, pagingParams, teachingLanguage, programType, qualificationAwarded, levelOfQualification, sector, fieldsOfStudy, sort) as OkObjectResult;

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

    private static OrganizationsController CreateSut(out IOrganizationsService organizationsService, out IComponentsService componentsService, out ICoursesService coursesService, out IEducationSpecificationsService educationSpecificationsService, out IGroupsService groupsService, out IProgramsService programsService)
    {
        organizationsService = Substitute.For<IOrganizationsService>();
        componentsService = Substitute.For<IComponentsService>();
        coursesService = Substitute.For<ICoursesService>();
        educationSpecificationsService = Substitute.For<IEducationSpecificationsService>();
        groupsService = Substitute.For<IGroupsService>();
        programsService = Substitute.For<IProgramsService>();
        return new OrganizationsController(organizationsService, componentsService, coursesService, educationSpecificationsService, groupsService, programsService);
    }
}