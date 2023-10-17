using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class ProgramsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetAllOrderedBy_WithDataRequestParameters_ReturnsSet()
    {
        // Arrange
        var educationSpecifications = new List<Program>()
        {
            _fixture.Build<Program>()
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create(),
            _fixture.Build<Program>()
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create(),
            _fixture.Build<Program>()
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create()
        }.AsQueryable();
        var db = Substitute.For<DbSet<Program>, IQueryable<Program>>();
        DbMockHelper.InitDb(db, educationSpecifications);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramsNoTracking.Returns(db);
        var programsRepository = new ProgramsRepository(dbContext);

        // Act
        var result = programsRepository.GetAllOrderedBy(new DataRequestParameters());

        // Assert
        Assert.IsInstanceOf<Pagination<Program>>(result);
        Assert.That(result.Items.Count, Is.EqualTo(3));
    }
}