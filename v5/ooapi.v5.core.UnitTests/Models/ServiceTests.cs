using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using ooapi.v5.Enums;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Models;

public sealed class ServiceTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void ConsumersList_ReturnsListOfString()
    {
        // arrange
        var consumers = _fixture.Build<ConsumerRegistration>()
            .CreateMany().ToList();
        var service = _fixture.Build<Service>().With(x => x.Consumers, consumers).Create();
        service.Consumers.Should().NotBeEmpty();

        // act
        var result = service.ConsumersList;

        // assert
        result.Should().NotBeNullOrEmpty().And.HaveCount(service.Consumers.Count);
        result.Should().ContainItemsAssignableTo<JObject>();
    }

    [Test]
    public void ConsumersList_NoConsumers_ReturnsEmptyList()
    {
        // arrange
        var service = _fixture.Build<Service>().With(x => x.Consumers, new List<ConsumerRegistration>()).Create();
        service.Consumers.Should().BeEmpty();

        // act
        var result = service.ConsumersList;

        // assert
        result.Should().BeEmpty();
    }
}