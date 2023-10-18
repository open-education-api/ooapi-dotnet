using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

/// <summary>
/// 
/// </summary>
[DataContract]
public class LanguageOfChoice
{
    /// <summary>
    /// Unique id of this languageOfChoice
    /// </summary>
    /// <value>Unique id of this languageOfChoice</value>
    [JsonIgnore]
    public Guid LanguageOfChoiceId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Value { get; set; }
}
