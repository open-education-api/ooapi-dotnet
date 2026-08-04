using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     A person that has a relationship with this institution.
/// </summary>
public class Person : IValidatableObject
{
    /// <summary>
    ///     Unique id of this person.
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    [JsonPropertyName("personId")]
    public string PersonIdValue { get; set; } = string.Empty;

    /// <summary>
    ///     The primary human readable identifier for the person. This is often the source identifier as defined by the
    ///     institution. Optional per spec (not in this resource's `required` list) - nullable so an
    ///     omitted write-request value stays `null` rather than a default-constructed
    ///     <see cref="IdentifierEntry" /> with an empty, validation-failing <c>codeType</c>.
    /// </summary>
    [JsonPropertyName("primaryCode")]
    public IdentifierEntry? PrimaryCode { get; set; }

    /// <summary>
    ///     The first name of this person.
    /// </summary>
    /// <example>Martina</example>
    [JsonPropertyName("givenName")]
    [StringLength(256)]
    public string? GivenName { get; set; }

    /// <summary>
    ///     The Name a person chooses to use. this is part of a Self Sovereign name e.g. in the eduId process comparable to
    ///     schema.org alternateName
    /// </summary>
    /// <example>Marieke</example>
    [JsonPropertyName("alternateName")]
    [StringLength(256)]
    public string? AlternateName { get; set; }

    /// <summary>
    ///     The name how the person would like to be called. Usually first name of this person.
    /// </summary>
    /// <example>Maartje</example>
    [JsonPropertyName("preferredName")]
    [StringLength(256)]
    public string? PreferredName { get; set; }

    /// <summary>
    ///     The prefix of the family name of this person.
    /// </summary>
    /// <example>van</example>
    [JsonPropertyName("surnamePrefix")]
    [StringLength(256)]
    public string? SurnamePrefix { get; set; }

    /// <summary>
    ///     The family name of this person.
    /// </summary>
    /// <example>Damme</example>
    [JsonPropertyName("surname")]
    [StringLength(256)]
    public string? Surname { get; set; }

    /// <summary>
    ///     The name of this person which will be displayed.
    /// </summary>
    [JsonPropertyName("displayName")]
    [StringLength(256)]
    public string? DisplayName { get; set; }

    /// <summary>
    ///     The initials of this person.
    /// </summary>
    [JsonPropertyName("initials")]
    [StringLength(256)]
    public string? Initials { get; set; }

    /// <summary>
    ///     The name of the person as printed on official identification documents.
    /// </summary>
    [JsonPropertyName("idCheckName")]
    [StringLength(2048)]
    public string? IdCheckName { get; set; }

    /// <summary>
    ///     Whether this person has an active enrolment.
    /// </summary>
    /// <example>false</example>
    [JsonPropertyName("activeEnrolment")]
    public bool ActiveEnrolment { get; set; }

    /// <summary>
    ///     The date of birth of this person, using the full-date format as defined in RFC 3339 (section 5.6).
    /// </summary>
    [JsonPropertyName("dateOfBirth")]
    [StringLength(256)]
    public string? DateOfBirth { get; set; }

    /// <summary>
    ///     The city of birth of this person.
    /// </summary>
    [JsonPropertyName("cityOfBirth")]
    [StringLength(256)]
    public string? CityOfBirth { get; set; }

    /// <summary>
    ///     Country of birth.
    /// </summary>
    [JsonPropertyName("countryOfBirth")]
    public Country? CountryOfBirth { get; set; }

    /// <summary>
    ///     Nationality information.
    /// </summary>
    [JsonPropertyName("nationality")]
    public Nationality? Nationality { get; set; }

    /// <summary>
    ///     The date of nationality of this person, using the full-date format as defined in RFC 3339 (section 5.6).
    /// </summary>
    [JsonPropertyName("dateOfNationality")]
    [StringLength(256)]
    public string? DateOfNationality { get; set; }

    /// <summary>
    ///     The affiliations of this person.
    /// </summary>
    [JsonPropertyName("affiliations")]
    [ExtensibleEnum("personAffiliation")]
    public string[]? Affiliations { get; set; }

    /// <summary>
    ///     The primary email address of this person.
    /// </summary>
    [JsonPropertyName("email")]
    [StringLength(256)]
    public string? Email { get; set; }

    /// <summary>
    ///     The secondary email address of this person.
    /// </summary>
    [JsonPropertyName("secondaryEmail")]
    [StringLength(256)]
    public string? SecondaryEmail { get; set; }

    /// <summary>
    ///     The telephone number of this person.
    /// </summary>
    [JsonPropertyName("telephoneNumber")]
    [StringLength(256)]
    public string? TelephoneNumber { get; set; }

    /// <summary>
    ///     The mobile number of this person.
    /// </summary>
    [JsonPropertyName("mobileNumber")]
    [StringLength(256)]
    public string? MobileNumber { get; set; }

    /// <summary>
    ///     The url of the informal picture of this person.
    /// </summary>
    [JsonPropertyName("photoSocial")]
    [StringLength(2048)]
    public string? PhotoSocial { get; set; }

    /// <summary>
    ///     The url of the official picture of this person.
    /// </summary>
    [JsonPropertyName("photoOfficial")]
    [StringLength(2048)]
    public string? PhotoOfficial { get; set; }

    /// <summary>
    ///     Gender of this person.
    /// </summary>
    [JsonPropertyName("gender")]
    [ExtensibleEnum("gender")]
    [StringLength(256)]
    public string? Gender { get; set; }

    /// <summary>
    ///     A title prefix to be used for this person.
    /// </summary>
    [JsonPropertyName("titlePrefix")]
    [StringLength(256)]
    public string? TitlePrefix { get; set; }

    /// <summary>
    ///     A title suffix to be used for this person.
    /// </summary>
    [JsonPropertyName("titleSuffix")]
    [StringLength(256)]
    public string? TitleSuffix { get; set; }

    /// <summary>
    ///     The name of the office where this person is located.
    /// </summary>
    [JsonPropertyName("office")]
    [StringLength(256)]
    public string? Office { get; set; }

    /// <summary>
    ///     Address information.
    /// </summary>
    [JsonPropertyName("address")]
    public Address? Address { get; set; }

    /// <summary>
    ///     In Case of Emergency contact - full name.
    /// </summary>
    [JsonPropertyName("ICEName")]
    [StringLength(256)]
    public string? IceName { get; set; }

    /// <summary>
    ///     Phone number of In Case of Emergency contact.
    /// </summary>
    [JsonPropertyName("ICEPhoneNumber")]
    [StringLength(256)]
    public string? IcePhoneNumber { get; set; }

    /// <summary>
    ///     ICE relation type.
    /// </summary>
    [JsonPropertyName("ICERelation")]
    [ExtensibleEnum("iceRelationType")]
    public string? IceRelation { get; set; }

    /// <summary>
    ///     The language(s) of choice for this person according to RFC4647.
    /// </summary>
    [JsonPropertyName("languageOfChoice")]
    [RegexPattern("language")]
    public string[]? LanguageOfChoice { get; set; }

    /// <summary>
    ///     An array of additional human readable codes/identifiers for the entity being described.
    /// </summary>
    [JsonPropertyName("otherCodes")]
    public IdentifierEntry[]? OtherCodes { get; set; }

    /// <summary>
    ///     Assigned resources or time based on the needs of a person.
    /// </summary>
    [JsonPropertyName("assignedNeeds")]
    public PersonalNeed[]? AssignedNeeds { get; set; }

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

    /// <summary>
    ///     The spec's own <c>anyOf</c> requires <c>surname</c>, <c>givenName</c>, or <c>preferredName</c>
    ///     (each paired with <c>primaryCode</c>/<c>activeEnrolment</c>, which are always present here
    ///     regardless of client input - see their own doc comments/defaults) - without this check a
    ///     client could create a person whose own <c>GET</c> response fails that same schema.
    /// </summary>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Surname) && string.IsNullOrEmpty(GivenName) && string.IsNullOrEmpty(PreferredName))
            yield return new ValidationResult(
                "At least one of surname, givenName, or preferredName must be provided.",
                [nameof(Surname), nameof(GivenName), nameof(PreferredName)]);
    }
}
