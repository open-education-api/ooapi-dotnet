using AutoFixture;
using FluentAssertions;
using ooapi.v5.core.Utility;

namespace ooapi.v5.core.UnitTests.Utility;

public class FilterToLinqTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public void Parse_WithAndFilter_ReturnsIQueryable()
    {
        // Arrange
        var items = _fixture.CreateMany<SomeFilterableClass>().ToList();
        string filter = $"integerproperty eq {items[0].IntegerProperty} and stringproperty eq {items[0].StringProperty}";
        var suj = new FilterToLinq<SomeFilterableClass>(filter);
        
        // Act
        var result = suj.Parse(items.AsQueryable());

        // Assert
        result.Should().NotBeNull();
        result.AsEnumerable().Should().HaveCount(1);
    }

    [Test]
    public void Parse_WithOrFilter_ReturnsIQueryable()
    {
        // Arrange
        var items = _fixture.CreateMany<SomeFilterableClass>().ToList();
        string filter = $"integerproperty eq {items[0].IntegerProperty} or stringproperty eq {items[0].StringProperty}";
        var suj = new FilterToLinq<SomeFilterableClass>(filter);

        // Act
        var result = suj.Parse(items.AsQueryable());

        // Assert
        result.Should().NotBeNull();
        result.AsEnumerable().Should().HaveCount(1);
    }

    [Test]
    public void Parse_WithEmptyFilter_ReturnsIQueryable()
    {
        // Arrange
        var items = _fixture.CreateMany<SomeFilterableClass>().ToList();
        var suj = new FilterToLinq<SomeFilterableClass>(string.Empty);

        // Act
        var result = suj.Parse(items.AsQueryable());

        // Assert
        result.Should().NotBeNull();
        result.AsEnumerable().Should().HaveCount(items.Count);
    }

    [TestCase("eq")]
    [TestCase("ne")]
    [TestCase("gt")]
    [TestCase("lt")]
    [TestCase("lte")]
    public void Parse_WithOperatorOnValueTypes_CanReturnIQueryable(string @operator)
    {
        // Arrange
        var items = _fixture.CreateMany<SomeFilterableClass>().ToList();
        string filter = $"integerproperty {@operator} {items[0].IntegerProperty}";
        var suj = new FilterToLinq<SomeFilterableClass>(filter);

        // Act
        var result = suj.Parse(items.AsQueryable());

        // Assert
        result.Should().NotBeNull();
    }

    [TestCase("startswith")]
    [TestCase("endswith")]
    [TestCase("contains")]
    public void Parse_WithOperatorOnReferenceTypes_CanReturnIQueryable(string @operator)
    {
        // Arrange
        var items = _fixture.CreateMany<SomeFilterableClass>().ToList();
        string filter = $"stringproperty {@operator} {items[0].StringProperty}";
        var suj = new FilterToLinq<SomeFilterableClass>(filter);

        // Act
        var result = suj.Parse(items.AsQueryable());

        // Assert
        result.Should().NotBeNull();
    }

    private class SomeFilterableClass
    {
        public int IntegerProperty { get; set; } = 1;

        public string StringProperty { get; set; } = "dummy";
    }
}