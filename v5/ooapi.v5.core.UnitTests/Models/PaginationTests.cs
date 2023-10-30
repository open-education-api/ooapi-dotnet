using AutoFixture;
using FluentAssertions;
using MockQueryable.NSubstitute;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Models;

public sealed class PaginationTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public async Task Constructor_WithoutFilter_SetItems()
    {
        // arrange
        var items = _fixture.CreateMany<TestObject>().ToList();
        var parameters = _fixture.Build<DataRequestParameters>()
            .With(x => x.PageNumber, 1)
            .With(x => x.PageSize, 100)
            .Without(x => x.Filter)
            .Create();
        var db = items.AsQueryable().BuildMockDbSet();

        // act
        var pagination = await Pagination<TestObject>.CreateAsync(db, parameters);

        // assert
        pagination.TotalItems.Should().Be(items.Count);
        pagination.PageSize.Should().Be(parameters.PageSize);
        pagination.PageNumber.Should().Be(pagination.PageNumber);
        pagination.Items.Count.Should().Be(items.Count);
        pagination.HasNextPage.Should().BeFalse();
        pagination.HasPreviousPage.Should().BeFalse();
    }

    [Test]
    public async Task Constructor_WithFilter_SetItems()
    {
        // arrange
        var items = _fixture.CreateMany<TestObject>(10).ToList();
        var parameters = _fixture.Build<DataRequestParameters>()
            .With(x => x.PageNumber, 2)
            .With(x => x.PageSize, 2)
            .With(x => x.Filter, "value eq test")
            .Create();
        var db = items.AsQueryable().BuildMockDbSet();

        // act
        var pagination = await Pagination<TestObject>.CreateAsync(db, parameters);

        // assert
        pagination.TotalItems.Should().Be(items.Count);
        pagination.PageSize.Should().Be(parameters.PageSize);
        pagination.PageNumber.Should().Be(pagination.PageNumber);
        pagination.Items.Should().BeEmpty();
        pagination.HasNextPage.Should().BeTrue();
        pagination.HasPreviousPage.Should().BeTrue();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class TestObject
    {
        // ReSharper disable once UnusedMember.Local
        public Guid Id { get; set; } = Guid.NewGuid();

        // ReSharper disable once UnusedMember.Local
        public string Value { get; set; } = string.Empty;
    }
}