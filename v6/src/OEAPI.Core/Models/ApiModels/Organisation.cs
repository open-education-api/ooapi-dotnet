using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     A description of a group of people working together to achieve a goal.
/// </summary>
public class Organisation
{
    /// <summary>
    ///     Unique id of this organisation.
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-123514174000</example>
    [JsonPropertyName("organisationId")]
    public string OrganisationId { get; set; } = string.Empty;

    /// <summary>
    ///     The primary human readable identifier for the organisation. This is often the source identifier as defined by the
    ///     root organisation.
    /// </summary>
    /// <example>organisation_id: Org01-Root</example>
    [JsonPropertyName("primaryCode")]
    public IdentifierEntry PrimaryCode { get; set; } = new();

    /// <summary>
    ///     The type of this organisation.
    /// </summary>
    /// <example>root</example>
    [JsonPropertyName("organisationType")]
    [ExtensibleEnum("organisationType")]
    [StringLength(256)]
    public string OrganisationType { get; set; } = string.Empty;

    /// <summary>
    ///     The name of the organisation.
    /// </summary>
    /// <example>Coöperatie SURF U.A.</example>
    [JsonPropertyName("name")]
    public LanguageTypedString[] Name { get; set; } = [];

    /// <summary>
    ///     Short name of the organisation.
    /// </summary>
    /// <example>SURF</example>
    [JsonPropertyName("shortName")]
    [StringLength(256)]
    public string? ShortName { get; set; }

    /// <summary>
    ///     Description of the organisation.
    /// </summary>
    [JsonPropertyName("description")]
    public LanguageTypedString[]? Description { get; set; }

    /// <summary>
    ///     Addresses of this organisation.
    /// </summary>
    [JsonPropertyName("addresses")]
    public Address[]? Addresses { get; set; }

    /// <summary>
    ///     URL of the organisation's website.
    /// </summary>
    /// <example>https://surf.nl</example>
    [JsonPropertyName("link")]
    [StringLength(2048)]
    public string? Link { get; set; }

    /// <summary>
    ///     Logo of this organisation.
    /// </summary>
    /// <example>https://www.surf.nl/themes/surf/logo.svg</example>
    [JsonPropertyName("logo")]
    [StringLength(2048)]
    public string? Logo { get; set; }

    /// <summary>
    ///     An array of additional human readable codes/identifiers for the entity being described.
    /// </summary>
    /// <example>institution_code: 114B529, kvk_organisation_id: 50277374</example>
    [JsonPropertyName("otherCodes")]
    public IdentifierEntry[]? OtherCodes { get; set; }

    /// <summary>
    ///     The identifier of the organisation which is the root organisation of this organisation.
    ///     When the client does not request expansion of `root`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("rootId")]
    public Identifier? RootId { get; set; }

    /// <summary>
    ///     The expanded organisation object which is the root organisation of this organisation.
    ///     When the client requests expansion of `root`, the full expanded organisation object MUST be returned
    ///     here instead of only the identifier. If no root organisation is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("root")]
    public Organisation? Root { get; set; }

    /// <summary>
    ///     The identifier of the organisational unit which is the parent of this organisation.
    ///     When the client does not request expansion of `parent`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("parentId")]
    public Identifier? ParentId { get; set; }

    /// <summary>
    ///     The expanded organisation object which is the parent of this organisation.
    ///     When the client requests expansion of `parent`, the full expanded organisation object MUST be returned
    ///     here instead of only the identifier. If no parent organisation is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("parent")]
    public Organisation? Parent { get; set; }

    /// <summary>
    ///     The identifiers of the organisational units for which this organisation is the parent.
    ///     When the client does not request expansion of `children`, only these identifiers are returned.
    /// </summary>
    [JsonPropertyName("childIds")]
    public Identifier[]? ChildIds { get; set; }

    /// <summary>
    ///     The expanded organisational unit objects for which this organisation is the parent.
    ///     When the client requests expansion of `children`, the full expanded organisation objects MUST be returned
    ///     here instead of only the identifiers. If no children are defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("children")]
    public Organisation[]? Children { get; set; }

    /// <summary>
    ///     Consumer information.
    /// </summary>
    [JsonPropertyName("consumer")]
    public object? Consumer { get; set; }

    /// <summary>
    ///     Free-form extensions.
    /// </summary>
    [JsonPropertyName("ext")]
    public object? Ext { get; set; }
}
