using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using ooapi.v5.Helpers;

namespace ooapi.v5.core.UnitTests.Helpers;

public sealed class OneOfConverterTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public void CanConvert_ThrowsNotSupportedException()
    {
        // act
        var act = () => new OneOfConverter().CanConvert(null!);

        // assert
        act.Should().ThrowExactly<NotSupportedException>();
    }

    [Test]
    public void ReadJson_ThrowsNotSupportedException()
    {
        // act
        var act = () => new OneOfConverter().ReadJson(null!, null!, null!, null!);

        // assert
        act.Should().ThrowExactly<NotSupportedException>();
    }

    [Test]
    public void WriteJson_ListIsNull_ReturnsJsonNull()
    {
        // arrange
        var items = new TestClassWithId { ChildWithId = null! };

        // act
        var result = JsonConvert.SerializeObject(items);

        // assert
        result.Should().Be("{\"ChildWithId\":null}");
    }

    [Test]
    public void WriteJson_WithItemWithId_ReturnsJsonWithId()
    {
        // arrange
        var items = _fixture.Create<TestClassWithId>();

        // act
        var result = JsonConvert.SerializeObject(items);

        // assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(items.ChildWithId.Id.ToString());
    }

    [Test]
    public void WriteJson_WithItemWithValue_ReturnsJsonWithValue()
    {
        // arrange
        var items = _fixture.Create<TestClassWithValue>();

        // act
        var result = JsonConvert.SerializeObject(items);

        // assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(items.ChildWithValues.Value?.Value);
    }

    private sealed class TestClassWithId
    {
        [JsonConverter(typeof(OneOfConverter))]
        // ReSharper disable once UnusedMember.Local
        public TestClassChildrenWithId ChildWithId { get; init; } = new ();
    }

    private sealed class TestClassChildrenWithId
    {
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public Guid? Id { get; set; }
    }

    private sealed class TestClassWithValue
    {
        [JsonConverter(typeof(OneOfConverter))]
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once CollectionNeverUpdated.Local
        public TestClassChildrenWithIdAndValue ChildWithValues { get; init; } = new ();
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