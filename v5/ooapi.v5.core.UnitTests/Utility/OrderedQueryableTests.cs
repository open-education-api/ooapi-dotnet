using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Formatters;
using ooapi.v5.Attributes;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Utility;

[TestFixture]
public class OrderedQueryableTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly IQueryable<Room> _rooms = new List<Room>
    {
        new Room { Name = "3B", TotalSeats = 8, AvailableSeats = 8 },
        new Room { Name = "3C", TotalSeats = 8, AvailableSeats = 4 },
        new Room { Name = "3A", TotalSeats = 6, AvailableSeats = 0 }
    }.AsQueryable();

    [Test]
    public void OrderBy_MultiplePropertiesAscending_OrdersByByValuesAscending()
    {
        // Act
        var result = _rooms.OrderBy("totalSeats,availableSeats").ToList();

        // Assert
        result[0].Name.Should().Be("3A");
        result[1].Name.Should().Be("3C");
        result[2].Name.Should().Be("3B");
    }

    [Test]
    public void OrderBy_MultiplePropertiesAscendingAndDescending_OrdersByValuesDescending()
    {
        // Act
        var result = _rooms.OrderBy("totalSeats,-availableSeats").ToList();

        // Assert
        result[0].Name.Should().Be("3A");
        result[1].Name.Should().Be("3B");
        result[2].Name.Should().Be("3C");
    }


    [Test]
    public void OrderBy_Nothing_OrdersByDefaultAscending()
    {
        // Act
        var result = _rooms.OrderBy().ToList();

        // Assert
        result[0].Name.Should().Be("3A");
        result[1].Name.Should().Be("3B");
        result[2].Name.Should().Be("3C");
    }

    [Test]
    public void OrderBy_DefaultDescending_OrdersByDefaultDescending()
    {
        // Act
        var result = _rooms.OrderBy("-name").ToList();

        // Assert
        result[0].Name.Should().Be("3C");
        result[1].Name.Should().Be("3B");
        result[2].Name.Should().Be("3A");
    }


    [Test]
    public void OrderBy_PropertyWithWhitespace_OrdersByProperty()
    {
        // Act
        var result = _rooms.OrderBy(" -name ").ToList();

        // Assert
        result[0].Name.Should().Be("3C");
        result[1].Name.Should().Be("3B");
        result[2].Name.Should().Be("3A");
    }

    [Test]
    public void OrderBy_EmptyProperty_OrdersByValidProperty()
    {
        // Act
        var result = _rooms.OrderBy("-name, ").ToList();

        // Assert
        result[0].Name.Should().Be("3C");
        result[1].Name.Should().Be("3B");
        result[2].Name.Should().Be("3A");
    }

    [Test]
    public void OrderBy_NonExistingProperty_OrdersByDefault()
    {
        // Act
        var result = _rooms.OrderBy("doesnotexist").ToList();

        // Assert
        result[0].Name.Should().Be("3A");
        result[1].Name.Should().Be("3B");
        result[2].Name.Should().Be("3C");
    }

    [Test]
    public void OrderBy_NonSortableProperty_OrdersByDefault()
    {
        // Arrange
        var rooms = new List<Room>
        {
            new Room { Name = "3B", TotalSeats = 8, AvailableSeats = 8, Abbreviation = "200" },
            new Room { Name = "3C", TotalSeats = 8, AvailableSeats = 4, Abbreviation = "111" },
            new Room { Name = "3A", TotalSeats = 6, AvailableSeats = 0, Abbreviation = "999" }
        }.AsQueryable();

        // Act
        var result = _rooms.OrderBy("abbreviation").ToList();

        // Assert
        result[0].Name.Should().Be("3A");
        result[1].Name.Should().Be("3B");
        result[2].Name.Should().Be("3C");
    }

    [Test]
    public void FilterBy_MultipleIntegerProperties_FiltersByProperties()
    {
        // Arrange
        var filters = new Dictionary<string, object>
        {
            { "totalSeats", 8 },
            { "availableSeats", 4 },
        };

        // Act
        var result = _rooms.FilterBy(filters).ToList();

        // Assert
        result.Count().Should().Be(1);
        result[0].Name.Should().Be("3C");
    }

    [Test]
    public void FilterBy_StringProperty_FiltersByProperty()
    {
        // Arrange
        var rooms = new List<Room>
        {
            new Room { Name = "3B", TotalSeats = 8, AvailableSeats = 8, Abbreviation = "200" },
            new Room { Name = "3C", TotalSeats = 8, AvailableSeats = 4, Abbreviation = "111" },
            new Room { Name = "3A", TotalSeats = 6, AvailableSeats = 0, Abbreviation = "999" }
        }.AsQueryable();

        var filters = new Dictionary<string, object>
        {
            { "abbreviation", "111" },
        };

        // Act
        var result = rooms.FilterBy(filters).ToList();

        // Assert
        result.Count().Should().Be(1);
        result[0].Name.Should().Be("3C");
    }

    [Test]
    public void FilterBy_NonExistingProperty_ThrowFormatException()
    {
        // Arrange
        var filters = new Dictionary<string, object>
        {
            { "doesNotExist", 8 },
        };

        // Act
        FluentActions.Invoking(() => _rooms.FilterBy(filters)).Should().Throw<FormatException>().WithMessage("'DoesNotExist' is not a filterable field. Remove it from the filter parameter and call the API again");
    }

    [Test]
    public void SearchBy_MultipleSearchableProperties_SearchesBySearchableProperties()
    {
        // Arrange
        var searchableEntities = new List<SearchableEntity>
        {
            new SearchableEntity { Id = 1, Name = "3A computer room" },
            new SearchableEntity { Id = 2, Name = "3B toilet room", },
            new SearchableEntity { Id = 3, Name = "3C large room", Description = "Room with computer and beamer." },
        }.AsQueryable();

        // Act
        var result = searchableEntities.SearchBy("computer").ToList();

        // Assert
        result.Count().Should().Be(2);
        result[0].Id.Should().Be(1);
        result[0].Id.Should().Be(3);
    }

    [Test]
    public void SearchBy_NoSearchableProperties_WithNullSource()
    {
        // Arrange
        FluentActions.Invoking(() => _rooms.SearchBy("searchTern")).Should().Throw<InputFormatterException>().WithMessage("There are no searchable fields defined for this endpoint. Clear the search parameter and call the API again.");
    }

    private class SearchableEntity
    {
        public int Id { get; set; }

        [Searchable]
        public string? Name { get; set; } = default!;

        [Searchable]
        public string? Description { get; set; } = default!;
    }
}