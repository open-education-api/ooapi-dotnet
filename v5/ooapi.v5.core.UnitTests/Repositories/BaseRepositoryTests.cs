using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class BaseRepositoryTests
{
    [Test]
    public void GetAllOrderedBy_WithPrimaryCodeSearch_ReturnsFilteredSet()
    {
        // Arrange
        var repository = GetRepository<Foo>();

        var dataRequestParameters = new DataRequestParameters { PrimaryCodeSearch = "123" };
        var set = new List<Foo>()
        {
            new Foo() { PrimaryCode = "123" },
            new Foo() { PrimaryCode = "456" }
        }.AsQueryable();

        // Act
        var result = repository.GetAllOrderedBy(dataRequestParameters, set);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Foo>>());
        Assert.That(result.Items, Has.Count.EqualTo(1));
    }

    [Test]
    public void GetAllOrderedBy_WithFilters_ReturnsFilteredSet()
    {
        // Arrange
        var set = new List<Foo>()
        {
            new Foo() { PrimaryCode = "123", PrimaryCodeType = "x-test"},
            new Foo() { PrimaryCode = "456", PrimaryCodeType = "not-x-test"}
        }.AsQueryable();

        var dataRequestParameters = new DataRequestParameters { Filters = new Dictionary<string, object> { { "PrimaryCodeType", "x-test" } } };
        var repository = GetRepository<Foo>();

        // Act
        var result = repository.GetAllOrderedBy(dataRequestParameters, set);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Foo>>());
        Assert.That(result.Items, Has.Count.EqualTo(1));
    }

    [Test]
    public void GetAllOrderedBy_WithNoParameters_ReturnsAll()
    {
        // Arrange
        var dataRequestParameters = new DataRequestParameters();
        var set = new List<Foo>()
        {
            new Foo() { PrimaryCode = "123", PrimaryCodeType = "x-test"},
            new Foo() { PrimaryCode = "456", PrimaryCodeType = "not-x-test"}
        }.AsQueryable();
        var repository = GetRepository<Foo>();

        // Act
        var result = repository.GetAllOrderedBy(dataRequestParameters, set);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Foo>>());
        Assert.That(result.Items, Has.Count.EqualTo(2));
    }

    [Test]
    public void GetAllOrderedBy_WithNoSetSupplied_ReturnsSetFromDbContext()
    {
        // Arrange
        var dataRequestParameters = new DataRequestParameters();
        var dbContextSet = new List<Foo>()
        {
            new Foo() { PrimaryCode = "123", PrimaryCodeType = "x-test"},
            new Foo() { PrimaryCode = "456", PrimaryCodeType = "not-x-test"}
        }.AsQueryable();
        var repository = GetRepository<Foo>(dbContextSet);

        // Act
        var result = repository.GetAllOrderedBy(dataRequestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Foo>>());
    }

    private static BaseRepository<T> GetRepository<T>(IQueryable<T>? setForDbContext = null) where T : class
    {
        var dbContext = Substitute.For<ICoreDbContext>();
        if (setForDbContext is not null)
        {
            dbContext.Set<T>().AsQueryable().Returns(setForDbContext);
        }

        return new BaseRepository<T>(dbContext);
    }

    public class Foo
    {
        public string? PrimaryCodeType { get; set; }
        public string? PrimaryCode { get; set; }

        public IdentifierEntry? PrimaryCodeIdentifier
        {
            get
            {
                if (PrimaryCodeType is not null && PrimaryCode is not null)
                {
                    return new IdentifierEntry() { CodeType = PrimaryCodeType, Code = PrimaryCode };
                }

                return null;
            }
            set
            {
                if (value is not null)
                {
                    PrimaryCode = value.Code;
                    PrimaryCodeType = value.CodeType;
                }
            }
        }
    }
}