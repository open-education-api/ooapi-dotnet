using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class TimelineOverrideProgrammeMappingExtensions
{
    /// <summary>
    ///     Maps a <see cref="TimelineOverrideProgrammeEntity" /> to its API model. Field-by-field
    ///     identical to <see cref="ProgrammeMappingExtensions.ToApiModel(ProgrammeEntity, string?)" />,
    ///     except the full expanded objects for organisation/parent/children/coordinators/instructors
    ///     are only populated when the matching option is present in <paramref name="expandOptions" /> -
    ///     the caller's <c>expand=</c> composes into every override entry, matching the same options
    ///     recognised for the parent programme.
    /// </summary>
    public static TimelineOverrideProgramme ToApiModel(
        this TimelineOverrideProgrammeEntity entity,
        string? consumer,
        IReadOnlySet<string> expandOptions)
    {
        return new TimelineOverrideProgramme
        {
            ValidFrom = entity.ValidFrom.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            ValidTo = entity.ValidTo?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            Programme = new ProgrammeProperties
            {
                PrimaryCode = new IdentifierEntry
                {
                    CodeType = entity.PrimaryCodeType,
                    Code = entity.PrimaryCode
                },
                Abbreviation = entity.Abbreviation,
                Name = string.IsNullOrEmpty(entity.NameJson)
                    ? []
                    : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.NameJson) ??
                      [],
                Description = string.IsNullOrEmpty(entity.DescriptionJson)
                    ? null
                    : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.DescriptionJson),
                TeachingLanguages = string.IsNullOrEmpty(entity.TeachingLanguagesJson)
                    ? null
                    : JsonSerializer.Deserialize<string[]>(entity.TeachingLanguagesJson),
                ProgrammeType = entity.ProgrammeType,
                ModeOfStudy = entity.ModeOfStudy,
                ModesOfDelivery = string.IsNullOrEmpty(entity.ModesOfDeliveryJson)
                    ? null
                    : JsonSerializer.Deserialize<string[]>(entity.ModesOfDeliveryJson),
                LevelOfQualification = entity.LevelOfQualification,
                FormalDocument = entity.FormalDocument,
                StudyLoad = string.IsNullOrEmpty(entity.StudyLoadJson)
                    ? null
                    : JsonSerializer.Deserialize<StudyLoadDescriptor[]>(entity.StudyLoadJson),
                QualificationAwarded = entity.QualificationAwarded,
                QualificationDesignations = string.IsNullOrEmpty(entity.QualificationDesignationsJson)
                    ? []
                    : JsonSerializer.Deserialize<string[]>(entity.QualificationDesignationsJson) ?? [],
                Duration = entity.Duration,
                OtherCodes = entity.OtherCodes.ToApiModel(),
                Consumer = entity.ConsumerJson.ToProgrammeConsumer(consumer),
                Ext = string.IsNullOrEmpty(entity.ExtJson)
                    ? null
                    : JsonSerializer.Deserialize<object>(entity.ExtJson),
                ParentId = entity.ParentEntityId.HasValue
                    ? new Identifier { Value = entity.Parent?.ProgrammeId ?? string.Empty }
                    : null,
                Parent = expandOptions.Contains("parent") && entity.Parent != null
                    ? entity.Parent.ToApiModel(null)
                    : null,
                ChildIds = entity.Children.Count == 0
                    ? null
                    : [.. entity.Children.Select(c => new Identifier { Value = c.ProgrammeId })],
                Children = expandOptions.Contains("children") && entity.Children.Count > 0
                    ? [.. entity.Children.Select(c => c.ToApiModel(null))]
                    : null,
                OrganisationId = entity.OrganisationEntityId.HasValue
                    ? new Identifier { Value = entity.Organisation?.OrganisationId ?? string.Empty }
                    : null,
                Organisation = expandOptions.Contains("organisation") && entity.Organisation != null
                    ? entity.Organisation.ToApiModel(null)
                    : null,
                CoordinatorIds = entity.Coordinators.Count == 0
                    ? null
                    : [.. entity.Coordinators.Select(c => new Identifier { Value = c.PersonId })],
                Coordinators = expandOptions.Contains("coordinators") && entity.Coordinators.Count > 0
                    ? [.. entity.Coordinators.Select(c => c.ToApiModel(null))]
                    : null,
                InstructorIds = entity.Instructors.Count == 0
                    ? null
                    : [.. entity.Instructors.Select(i => new Identifier { Value = i.PersonId })],
                Instructors = expandOptions.Contains("instructors") && entity.Instructors.Count > 0
                    ? [.. entity.Instructors.Select(i => i.ToApiModel(null))]
                    : null
            }
        };
    }
}
