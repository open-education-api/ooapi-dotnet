using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using ooapi.v5.Enums;
using ooapi.v5.Helpers;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Helpers;

public sealed class ConsumerConverterTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public void GetDynamicConsumers_WithPropertyTypeString_ShouldContainPropertyValue()
    {
        // arrange
        var consumer = _fixture.Build<Consumer>().With(x => x.PropertyType, ConsumerPropertyType.String).Create();

        // act
        var result = ConsumerConverter.GetDynamicConsumers(new List<Consumer> { consumer });

        // assert
        result.Should().NotBeNullOrEmpty();

        var item = result[0].ToObject<Dictionary<string, object>>();
        item.Should().ContainKey("consumerKey").WhoseValue.Should().Be(consumer.ConsumerKey);
        item.Should().ContainKey(consumer.PropertyName).WhoseValue.Should().Be(consumer.PropertyValue);
    }

    [Test]
    public void GetDynamicConsumers_WithPropertyTypeInt_ShouldContainPropertyValue()
    {
        // arrange
        var value = _fixture.Create<int>();
        var consumer = _fixture.Build<Consumer>().With(x => x.PropertyType, ConsumerPropertyType.Int)
            .With(x => x.PropertyValue, value.ToString).Create();

        // act
        var result = ConsumerConverter.GetDynamicConsumers(new List<Consumer> { consumer });

        // assert
        result.Should().NotBeNullOrEmpty();

        var item = result[0].ToObject<Dictionary<string, object>>();
        item.Should().ContainKey("consumerKey").WhoseValue.Should().Be(consumer.ConsumerKey);
        item.Should().ContainKey(consumer.PropertyName).WhoseValue.Should().Be((long)value);
    }

    [Test]
    public void GetDynamicConsumers_WithPropertyTypeBool_ShouldContainPropertyValue()
    {
        // arrange
        var value = _fixture.Create<bool>();
        var consumer = _fixture.Build<Consumer>().With(x => x.PropertyType, ConsumerPropertyType.Bool)
            .With(x => x.PropertyValue, value.ToString).Create();

        // act
        var result = ConsumerConverter.GetDynamicConsumers(new List<Consumer> { consumer });

        // assert
        result.Should().NotBeNullOrEmpty();

        var item = result[0].ToObject<Dictionary<string, object>>();
        item.Should().ContainKey("consumerKey").WhoseValue.Should().Be(consumer.ConsumerKey);
        item.Should().ContainKey(consumer.PropertyName).WhoseValue.Should().Be(value);
    }

    [Test]
    public void GetDynamicConsumers_WithPropertyTypeJArray_ShouldContainPropertyValue()
    {
        // arrange
        var array = new List<int> { 1, 2 };
        var consumer = _fixture.Build<Consumer>().With(x => x.PropertyType, ConsumerPropertyType.JArray).With(
            x => x.PropertyValue,
            JsonConvert.SerializeObject(array)).Create();

        // act
        var result = ConsumerConverter.GetDynamicConsumers(new List<Consumer> { consumer });

        // assert
        result.Should().NotBeNullOrEmpty();

        var item = result[0].ToObject<Dictionary<string, object>>();
        item.Should().ContainKey("consumerKey").WhoseValue.Should().Be(consumer.ConsumerKey);
        item.Should().ContainKey(consumer.PropertyName);

        JsonConvert.DeserializeObject<List<int>>(item![consumer.PropertyName].ToString()!).Should().BeEquivalentTo(array);
    }

    [Test]
    public void GetDynamicConsumers_WithPropertyTypeJObject_ShouldContainPropertyValue()
    {
        // arrange
        var testObject = _fixture.Create<TestClass>();
        var consumer = _fixture.Build<Consumer>().With(x => x.PropertyType, ConsumerPropertyType.JObject).With(
            x => x.PropertyValue,
            JsonConvert.SerializeObject(testObject)).Create();

        // act
        var result = ConsumerConverter.GetDynamicConsumers(new List<Consumer> { consumer });

        // assert
        result.Should().NotBeNullOrEmpty();

        var item = result[0].ToObject<Dictionary<string, object>>();
        item.Should().ContainKey("consumerKey").WhoseValue.Should().Be(consumer.ConsumerKey);
        item.Should().ContainKey(consumer.PropertyName);

        JsonConvert.DeserializeObject<TestClass>(item![consumer.PropertyName].ToString()!).Should().BeEquivalentTo(testObject);
    }

    [Test]
    public void GetDynamicConsumers_GivenCollectionWithSameConsumerKey_ShouldReturnSingleItem()
    {
        // arrange
        var consumerKey = _fixture.Create<string>();
        var consumers = _fixture.Build<Consumer>().With(x => x.PropertyType, ConsumerPropertyType.String).With(x => x.ConsumerKey, consumerKey)
            .CreateMany();

        // act
        var result = ConsumerConverter.GetDynamicConsumers(consumers);

        // assert
        result.Should().NotBeNullOrEmpty();
        result.Count.Should().Be(1);
    }

    private sealed class TestClass
    {
        // ReSharper disable once UnusedMember.Local
        public int Value { get; set; }
    }
}