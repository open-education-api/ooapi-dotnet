using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using ooapi.v5.Helpers;

namespace ooapi.v5.core.UnitTests.Helpers;

public sealed class ListOneOfConverterTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void CanConvert_ThrowsNotSupportedException()
    {
        // act
        var act = () => new ListOneOfConverter().CanConvert(null!);

        // assert
        act.Should().ThrowExactly<NotSupportedException>();
    }

    [Test]
    public void ReadJson_ThrowsNotSupportedException()
    {
        // act
        var act = () => new ListOneOfConverter().ReadJson(null!, null!, null!, null!);

        // assert
        act.Should().ThrowExactly<NotSupportedException>();
    }

    [Test]
    public void WriteJson_ListIsNull_ReturnsJsonNull()
    {
        // arrange
        var items = new TestClassWithId { ChildrenListWithId = null! };

        // act
        var result = JsonConvert.SerializeObject(items);

        // assert
        result.Should().Be("{\"ChildrenListWithId\":null}");
    }

    [Test]
    public void WriteJson_WithItemsWithId_ReturnsJsonWithIdArray()
    {
        // arrange
        var items = _fixture.Create<TestClassWithId>();

        // act
        var result = JsonConvert.SerializeObject(items);

        // assert
        result.Should().NotBeNullOrEmpty();
        result.Should().ContainAll(items.ChildrenListWithId.Select(x => x.Id.ToString()));
    }

    [Test]
    public void WriteJson_WithItemsWithValue_ReturnsJsonWithValueArray()
    {
        // arrange
        var items = _fixture.Create<TestClassWithValue>();

        // act
        var result = JsonConvert.SerializeObject(items);

        // assert
        result.Should().NotBeNullOrEmpty();
        result.Should().ContainAll(items.ChildrenListWithValues.Select(x => x.Value?.Value.ToString()));
    }

    private sealed class TestClassWithId
    {
        [JsonConverter(typeof(ListOneOfConverter))]
        // ReSharper disable once UnusedMember.Local
        public List<TestClassChildrenWithId> ChildrenListWithId { get; init; } = new ();
    }

    private sealed class TestClassChildrenWithId
    {
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public Guid? Id { get; set; }
    }

    private sealed class TestClassWithValue
    {
        [JsonConverter(typeof(ListOneOfConverter))]
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once CollectionNeverUpdated.Local
        public List<TestClassChildrenWithIdAndValue> ChildrenListWithValues { get; init; } = new ();
    }

    private sealed class TestClassChildrenWithIdAndValue
    {
        // ReSharper disable once UnusedMember.Local
        public Guid? Id { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public TestClassChildrenWithValue? Value { get; set; }
    }

    private sealed class TestClassChildrenWithValue
    {
        // ReSharper disable once UnusedMember.Local
        public string Value { get; set; } = string.Empty;
    }
}