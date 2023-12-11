using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using ooapi.v5.Enums;

namespace ooapi.v5.Models;

[ExcludeFromCodeCoverage(Justification = "Get/Set")]
public class Consumer : ConsumerBase
{
    [JsonIgnore]
    public Guid Id { get; set; }
}