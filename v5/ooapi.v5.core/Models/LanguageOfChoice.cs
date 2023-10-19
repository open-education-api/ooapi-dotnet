using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;


[DataContract]
public class LanguageOfChoice
{
    /// <summary>
    /// Unique id of this languageOfChoice
    /// </summary>
    /// <value>Unique id of this languageOfChoice</value>
    [JsonIgnore]
    public Guid LanguageOfChoiceId { get; set; }

    public string Value { get; set; } = default!;
}
