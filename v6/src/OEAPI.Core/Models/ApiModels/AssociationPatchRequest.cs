using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     Shared JSON Merge Patch (RFC 7396, <c>application/merge-patch+json</c>) request body for the
///     spec's 4 offering-association PATCH endpoints (course/learning-component/programme/test-component
///     offering associations). All 4 share this exact shape in the spec - only the specific sub-schema of
///     <c>result</c> differs per association type (each adds a small extra field on top of the shared
///     <see cref="Result" /> shape, e.g. a <c>weight</c> property - not itself an extensible-enum concern),
///     which this project models as the shared <see cref="Result" /> type everywhere (see
///     <see cref="CourseOfferingAssociation.Result" /> and its siblings), so one shared request type
///     covers all 4 rather than 4 near-duplicates.
/// </summary>
public class AssociationPatchRequest
{
    /// <summary>
    ///     The state of this association for the organisation performing the request (pending, cancelled,
    ///     denied, associated, queued - extensible enumeration). Only present in the PATCH request, not
    ///     part of the association's readable representation.
    /// </summary>
    [JsonPropertyName("remoteState")]
    [ExtensibleEnum("remoteAssociationState")]
    [StringLength(256)]
    public string? RemoteState { get; set; }

    /// <summary>
    ///     The result of this association.
    /// </summary>
    [JsonPropertyName("result")]
    public Result? Result { get; set; }
}
