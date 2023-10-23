using Newtonsoft.Json;
using ooapi.v5.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ooapi.v5.Models;

/// <summary>
/// The primary human readable identifier for this component. This is often the source identifier as defined by the institution.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Get/Set")]
public class IdentifierEntry
{
    /// <summary>
    /// The code/identifier type.   This is an *extensible enumeration*. Use &#x60;x-&#x60; to prefix custom values  The predefined values are:   - &#x60;brin&#x60;: The registration number for a Dutch educational institution that is issued by the Dutch Ministry of Education, Culture and Science   - &#x60;crohoCreboCode&#x60;: programs with a CREBO and CROHO number are accredited by the Dutch Ministry of Education, Culture and Science (OCW)   - &#x60;programCode&#x60;: Identifier for the program (collection of courses)   - &#x60;componentCode&#x60;: The code for a component (part of a course)   - &#x60;offeringCode&#x60;: The code to identify a specific offering (program, course or component offering)   - &#x60;organizationId&#x60;: The identifier for the organization   - &#x60;buildingId&#x60;: The number or code to identify a building   - &#x60;bagId&#x60;: The identification of a building as it is known in the Dutch Building Administration (BAG)   - &#x60;roomCode&#x60;: The code for a room   - &#x60;systemId&#x60;: Identifier assigned to an entity in context of a specific system   - &#x60;productId&#x60;: Identifier assigned to a specific product   - &#x60;nationalIdentityNumber&#x60;: Identifier assigned by the governement of the person. e.g. a social security number in the USA   - &#x60;studentNumber&#x60;: Identifier for the student   - &#x60;studielinkNumber&#x60;: Identifier for the person as determined by Studielink   - &#x60;esi&#x60;: European Student Identifier   - &#x60;userName&#x60;: The name of a user   - &#x60;accountId&#x60;: Identifier assigned to a specific account   - &#x60;emailAdress&#x60;: An email address   - &#x60;groupCode&#x60;: The identifier for a group (of persons)   - &#x60;isbn&#x60;: International Standard Book Number that serve as product identifiers for Books   - &#x60;issn&#x60;: International Standard Book Number that serve as product identifiers for periodicals   - &#x60;orcId&#x60;: Open Researcher and Contributor ID   - &#x60;uuid&#x60;: A universally unique identifier   - &#x60;schacHome&#x60;: Home organization using the domain name of the organization   - &#x60;identifier&#x60;: Generic Identifier 
    /// </summary>
    /// <value>The code/identifier type.   This is an *extensible enumeration*. Use &#x60;x-&#x60; to prefix custom values  The predefined values are:   - &#x60;brin&#x60;: The registration number for a Dutch educational institution that is issued by the Dutch Ministry of Education, Culture and Science   - &#x60;crohoCreboCode&#x60;: programs with a CREBO and CROHO number are accredited by the Dutch Ministry of Education, Culture and Science (OCW)   - &#x60;programCode&#x60;: Identifier for the program (collection of courses)   - &#x60;componentCode&#x60;: The code for a component (part of a course)   - &#x60;offeringCode&#x60;: The code to identify a specific offering (program, course or component offering)   - &#x60;organizationId&#x60;: The identifier for the organization   - &#x60;buildingId&#x60;: The number or code to identify a building   - &#x60;bagId&#x60;: The identification of a building as it is known in the Dutch Building Administration (BAG)   - &#x60;roomCode&#x60;: The code for a room   - &#x60;systemId&#x60;: Identifier assigned to an entity in context of a specific system   - &#x60;productId&#x60;: Identifier assigned to a specific product   - &#x60;nationalIdentityNumber&#x60;: Identifier assigned by the governement of the person. e.g. a social security number in the USA   - &#x60;studentNumber&#x60;: Identifier for the student   - &#x60;studielinkNumber&#x60;: Identifier for the person as determined by Studielink   - &#x60;esi&#x60;: European Student Identifier   - &#x60;userName&#x60;: The name of a user   - &#x60;accountId&#x60;: Identifier assigned to a specific account   - &#x60;emailAdress&#x60;: An email address   - &#x60;groupCode&#x60;: The identifier for a group (of persons)   - &#x60;isbn&#x60;: International Standard Book Number that serve as product identifiers for Books   - &#x60;issn&#x60;: International Standard Book Number that serve as product identifiers for periodicals   - &#x60;orcId&#x60;: Open Researcher and Contributor ID   - &#x60;uuid&#x60;: A universally unique identifier   - &#x60;schacHome&#x60;: Home organization using the domain name of the organization   - &#x60;identifier&#x60;: Generic Identifier </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "codeType")]
    public string CodeType { get; set; } = default!;

    /// <summary>
    /// Human readable value for the code/identifier
    /// </summary>
    /// <value>Human readable value for the code/identifier</value>
    [JsonRequired]
    [JsonProperty(PropertyName = "code")]
    [MaxLength(256)]
    [Searchable]
    public string Code { get; set; } = default!;
}