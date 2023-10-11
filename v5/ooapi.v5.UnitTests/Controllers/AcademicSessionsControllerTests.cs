using AutoFixture;
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
    public void AcademicSessionsGet_Success_ReturnsAcademicSessions()
    {
        //arrange
        var sut = CreateSut(out var academicSessionsService);
        var primaryCodeParam = _fixture.Create<PrimaryCodeParam>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var academicSessionType = _fixture.Create<string?>();
        var parent = _fixture.Create<Guid?>();
        var year = _fixture.Create<Guid?>();
        var sort = _fixture.Create<string?>();

        DataRequestParameters dataRequestParameters = null;

        academicSessionsService.GetAll(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), out new ErrorResponse, Arg.Any<string?>).Returns();

        //act
        var result = sut.AcademicSessionsGet(primaryCodeParam, filterParams, pagingParams, academicSessionType, parent, year, sort);

        //assert
        Assert.IsInstanceOf<List<AcademicSession>>(result);
    }

    private static AcademicSessionsController CreateSut(out IAcademicSessionsService academicSessionsService)
    {
        academicSessionsService = Substitute.For<IAcademicSessionsService>();
        return new AcademicSessionsController(academicSessionsService);
    }
}