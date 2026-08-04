using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class TimelineOverrideCourseMappingExtensions
{
    /// <summary>
    ///     Maps a <see cref="TimelineOverrideCourseEntity" /> to its API model. Field-by-field
    ///     identical to <see cref="CourseMappingExtensions.ToApiModel(CourseEntity, string?)" />, except
    ///     the full expanded objects for organisation/coordinators/instructors/programmes/learning
    ///     outcomes are only populated when the matching option is present in
    ///     <paramref name="expandOptions" /> - the caller's <c>expand=</c> composes into every override
    ///     entry, matching the same options recognised for the parent course.
    /// </summary>
    public static TimelineOverrideCourse ToApiModel(
        this TimelineOverrideCourseEntity entity,
        string? consumer,
        IReadOnlySet<string> expandOptions)
    {
        return new TimelineOverrideCourse
        {
            ValidFrom = entity.ValidFrom.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            ValidTo = entity.ValidTo?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            Course = new CourseProperties
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
                Duration = entity.Duration,
                FirstStartDate = entity.FirstStartDate?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                StudyLoad = string.IsNullOrEmpty(entity.StudyLoadJson)
                    ? null
                    : JsonSerializer.Deserialize<StudyLoadDescriptor[]>(entity.StudyLoadJson),
                ModesOfDelivery = string.IsNullOrEmpty(entity.ModesOfDeliveryJson)
                    ? null
                    : JsonSerializer.Deserialize<string[]>(entity.ModesOfDeliveryJson),
                TeachingLanguages = string.IsNullOrEmpty(entity.TeachingLanguagesJson)
                    ? null
                    : JsonSerializer.Deserialize<string[]>(entity.TeachingLanguagesJson),
                FieldsOfStudy = entity.FieldsOfStudy,
                OtherCodes = entity.OtherCodes.ToApiModel(),
                Link = entity.Link,
                Addresses = string.IsNullOrEmpty(entity.AddressesJson)
                    ? null
                    : JsonSerializer.Deserialize<Address[]>(entity.AddressesJson),
                Level = entity.Level,
                Resources = string.IsNullOrEmpty(entity.ResourcesJson)
                    ? null
                    : JsonSerializer.Deserialize<string[]>(entity.ResourcesJson),
                Assessment = string.IsNullOrEmpty(entity.AssessmentJson)
                    ? null
                    : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.AssessmentJson),
                Enrolment = string.IsNullOrEmpty(entity.EnrolmentJson)
                    ? null
                    : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.EnrolmentJson),
                AdmissionRequirements = string.IsNullOrEmpty(entity.AdmissionRequirementsJson)
                    ? null
                    : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.AdmissionRequirementsJson),
                QualificationRequirements = string.IsNullOrEmpty(entity.QualificationRequirementsJson)
                    ? null
                    : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.QualificationRequirementsJson),
                SupplementaryInformation = string.IsNullOrEmpty(entity.SupplementaryInformationJson)
                    ? []
                    : JsonSerializer.Deserialize<SupplementaryInformation[]>(entity.SupplementaryInformationJson),
                Consumer = entity.ConsumerJson.ToCourseConsumer(consumer),
                Ext = string.IsNullOrEmpty(entity.ExtJson)
                    ? null
                    : JsonSerializer.Deserialize<object>(entity.ExtJson),
                OrganisationId = entity.Organisation == null
                    ? null
                    : new Identifier { Value = entity.Organisation.OrganisationId },
                Organisation = expandOptions.Contains("organisation") && entity.Organisation != null
                    ? entity.Organisation.ToApiModel(null)
                    : null,
                CoordinatorIds = entity.Coordinators.Count == 0
                    ? null
                    : [.. entity.Coordinators.Select(p => new Identifier { Value = p.PersonId })],
                Coordinators = expandOptions.Contains("coordinators") && entity.Coordinators.Count > 0
                    ? [.. entity.Coordinators.Select(p => p.ToApiModel(null))]
                    : null,
                InstructorIds = entity.Instructors.Count == 0
                    ? null
                    : [.. entity.Instructors.Select(p => new Identifier { Value = p.PersonId })],
                Instructors = expandOptions.Contains("instructors") && entity.Instructors.Count > 0
                    ? [.. entity.Instructors.Select(p => p.ToApiModel(null))]
                    : null,
                ProgrammeIds = entity.Programmes.Count == 0
                    ? null
                    : [.. entity.Programmes.Select(p => new Identifier { Value = p.ProgrammeId })],
                Programmes = expandOptions.Contains("programmes") && entity.Programmes.Count > 0
                    ? [.. entity.Programmes.Select(p => p.ToApiModel(null))]
                    : null,
                LearningOutcomeIds = entity.LearningOutcomes.Count == 0
                    ? null
                    : [.. entity.LearningOutcomes.Select(o => new Identifier { Value = o.LearningOutcomeId })],
                LearningOutcomes =
                    (expandOptions.Contains("learningoutcomes") || expandOptions.Contains("learningOutcomes"))
                    && entity.LearningOutcomes.Count > 0
                        ? [.. entity.LearningOutcomes.Select(o => o.ToApiModel(null))]
                        : null
            }
        };
    }
}
