// using AutoFixture;
// using NSubstitute;
// using ooapi.v5.core.Repositories;
// using ooapi.v5.core.Repositories.Interfaces;
// using ooapi.v5.core.Security;
// using ooapi.v5.core.Services;
// using ooapi.v5.core.Utility;
// using ooapi.v5.Models;
//
// namespace ooapi.v5.core.UnitTests.Services;
//
// [TestFixture]
// public class OrganizationsServiceTests
// {
//     private readonly IFixture _fixture = new Fixture();
//
//     [Test]
//     public void GetAll_CallsRepository()
//     {
//         // Arrange
//         var dbContext = Substitute.For<ICoreDbContext>();
//         var repository = Substitute.For<IOrganizationsRepository>();
//         var userRequestContext = Substitute.For<IUserRequestContext>();
//         var sut = new OrganizationsService(dbContext, repository, userRequestContext);
//         var dataRequestParameters = new DataRequestParameters();
//
//         var organizations = _fixture.Build<Organization.CreateMany<Organization>();
//         var expected = new Pagination<Organization>()
//         repository.GetAllOrderedBy(dataRequestParameters).Returns(expected);
//
//         // Act
//         var result = sut.GetAll(dataRequestParameters);
//
//         // Assert
//         Assert.That(result, Is.EqualTo(expected));
//         repository.Received(1).GetAllOrderedBy(dataRequestParameters);
//     }
//
//     [Test]
//     public void Get_CallsRepository()
//     {
//         // Arrange
//         var dbContext = Substitute.For<ICoreDbContext>();
//         var repository = Substitute.For<IOrganizationsRepository>();
//         var userRequestContext = Substitute.For<IUserRequestContext>();
//         var sut = new OrganizationsService(dbContext, repository, userRequestContext);
//         var dataRequestParameters = new DataRequestParameters();
//         var organizationId = _fixture.Create<Guid>();
//
//         var expected = new Organization();
//         repository.GetOrganization(organizationId, dataRequestParameters).Returns(expected);
//
//         // Act
//         var result = sut.Get(organizationId, dataRequestParameters);
//
//         // Assert
//         Assert.That(result, Is.EqualTo(expected));
//         repository.Received(1).GetOrganization(organizationId, dataRequestParameters);
//     }
// }