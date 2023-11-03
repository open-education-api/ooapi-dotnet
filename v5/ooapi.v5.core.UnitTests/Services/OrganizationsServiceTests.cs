using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class OrganizationsServiceTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public async Task GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IOrganizationsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new OrganizationsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();

        var organizations = _fixture.Build<Organization>()
            .With(x => x.Address, _fixture.Build<Address>()
                .Without(x => x.Organizations)
                .Without(x => x.Components)
                .Without(x => x.Programs)
                .Without(x => x.ComponentOfferings)
                .Without(x => x.CourseOfferings)
                .Without(x => x.ProgramOfferings)
                .Without(x => x.Courses)
                .CreateMany<Address>(1)
                .ToArray())
            .Without(x => x.Addresses)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .CreateMany(5);
        var db = organizations.AsQueryable().BuildMockDbSet();

        var expected = await Pagination<Organization>.CreateAsync(db, dataRequestParameters);
        repository.GetAllOrderedByAsync(dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetAllAsync(dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(result.Items[0].Addresses, Has.Count.EqualTo(organizations.First().Address.Count));
        await repository.Received(1).GetAllOrderedByAsync(dataRequestParameters);
    }

    [Test]
    public async Task Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IOrganizationsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new OrganizationsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var organizationId = _fixture.Create<Guid>();

        var expected = new Organization();
        repository.GetOrganizationAsync(organizationId, dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetAsync(organizationId, dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetOrganizationAsync(organizationId, dataRequestParameters);
    }
}