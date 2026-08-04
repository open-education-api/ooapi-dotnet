namespace OEAPI.Core.Models.ApiModels.Validation;

/// <summary>
///     The OOAPI spec's known-value list for every <c>x-ooapi-extensible-enum</c> schema currently
///     validated by <see cref="ExtensibleEnumAttribute" />, keyed by the spec's own schema name (e.g.
///     <c>"offeringState"</c>) so a usage like <c>[ExtensibleEnum("offeringState")]</c> is traceable
///     straight back to the spec. Centralised here - rather than repeating each value list inline at
///     every <c>[ExtensibleEnum(...)]</c> usage - because several of these schemas back the same-shaped
///     property on multiple, otherwise-unrelated model classes (e.g. <c>offeringState</c> backs
///     <c>State</c> on all 4 offering types); a single shared source means the spec's value list only
///     ever needs updating in one place.
/// </summary>
public static class ExtensibleEnumValues
{
    /// <summary>
    ///     Every registered schema's known values, by spec schema name.
    /// </summary>
    public static readonly IReadOnlyDictionary<string, string[]> Values = new Dictionary<string, string[]>
    {
        ["codeType"] =
        [
            "account_id", "bag_id", "building_id", "component_code", "eckid", "email_address", "esi",
            "group_code", "group_type_code", "identifier", "institution_code", "isbn", "issn",
            "kvk_organisation_id", "kvk_establishment_id", "leerbedrijf_id", "offering_code",
            "organisation_id", "orcid", "product_id", "programme_code", "room_code", "schac_home",
            "student_number", "studielink_number", "system_id", "username", "uuid",
            "national_identity_number"
        ],
        ["documentType"] =
        [
            "additional_document", "assessment_form", "assessment_model", "assignment",
            "attendance_report", "handed_in_document", "instructions", "plagiarism_report",
            "session_report", "test_made", "other"
        ],
        ["offeringState"] = ["concept", "cancelled", "active", "inactive"],
        ["rosteringState"] = ["definitive", "preliminary", "tentative"],
        ["resultValueType"] =
        [
            "pass_or_fail", "insufficient_satisfactory_good", "us_letter", "uk_letter", "de_grade",
            "grade_0_100", "grade_0_10", "grade_0_10_one_decimal", "reference_level_europass", "other"
        ],
        ["modeOfDelivery"] =
        [
            "blended", "coil", "hybrid", "joint_delivery", "online", "presential", "project_based",
            "research_lab_based", "work_based"
        ],
        ["learningComponentType"] =
        [
            "consultation", "excursion", "external", "independent_study", "learning_community",
            "lecture", "practical", "project", "skills_training", "tutorial", "workshop"
        ],
        ["testComponentType"] =
        [
            "unknown", "test_on_paper", "digital_test", "life_skills_test", "oral_test",
            "portfolio_assessment"
        ],
        ["attemptState"] = ["pending", "cancelled", "associated", "finished"],
        ["attendance"] = ["unknown", "not_started", "unfinished", "present", "absent"],
        ["learningOutcomeLevel"] =
        [
            "bloom_1", "bloom_2", "bloom_3", "bloom_4", "bloom_5", "bloom_6", "solo_0", "solo_1",
            "solo_2", "solo_3", "solo_4"
        ],
        ["programmeType"] = ["programme", "minor", "honours", "specialisation", "track", "specification"],
        ["modeOfStudy"] = ["full_time", "part_time", "dual_training", "self_paced"],
        ["qualificationAwarded"] =
        [
            "diploma", "vocational_diploma", "certificate", "associate_degree", "bachelor", "master",
            "doctoral", "none"
        ],
        ["associationState"] = ["pending", "cancelled", "denied", "associated", "queued", "finished"],
        ["remoteAssociationState"] = ["pending", "cancelled", "denied", "associated", "queued"],
        ["academicSessionType"] = ["academic_year", "semester", "trimester", "quarter", "testing_period", "period"],
        ["addressType"] = ["postal", "visit", "deliveries", "invoicing", "teaching"],
        ["associationRole"] =
        [
            "student", "lecturer", "teaching_assistant", "coordinator", "invigilator", "assessor", "guest"
        ],
        ["componentState"] = ["concept", "cancelled", "active", "inactive"],
        ["level"] =
        [
            "pre_vocational", "secondary_vocational_education", "secondary_vocational_education_1",
            "secondary_vocational_education_2", "secondary_vocational_education_3",
            "secondary_vocational_education_4", "associate_degree", "bachelor", "master", "doctoral",
            "post_doctoral", "undefined", "undivided", "nt2_1", "nt2_2"
        ],
        ["gender"] = ["m", "f", "x", "o", "u", "n"],
        ["groupType"] = ["class", "team", "group"],
        ["iceRelationType"] = ["partner", "parent", "other"],
        ["membershipRole"] =
        [
            "student", "lecturer", "teaching_assistant", "coordinator", "invigilator", "assessor", "guest"
        ],
        ["membershipState"] = ["cancelled", "active"],
        ["organisationType"] = ["root", "institute", "department", "faculty", "branch", "academy", "school"],
        ["personAffiliation"] = ["student", "employee", "guest"],
        ["roomType"] =
        [
            "general_purpose", "lecture_room", "computer_room", "laboratory", "office", "workspace",
            "exam_location", "study_room", "examination_room", "conference_room"
        ],
        ["resultState"] = ["in_progress", "postponed", "completed", "queued"],
        ["passState"] = ["unknown", "passed", "failed"],
        ["costType"] = ["stap_eligible", "total_costs"],
        ["studyLoadUnit"] = ["contact_time", "ects", "sbu", "sp", "hour"],
        ["associationAttendance"] = ["unknown", "not_started", "unfinished", "present", "absent"],
        ["personalNeed"] = ["extra_time", "spoken", "spell_checker_on_screen"],
        ["levelOfQualification"] =
        [
            "eqf_0", "eqf_1", "eqf_2", "eqf_3", "eqf_4", "nlqf_4plus", "eqf_5", "eqf_6", "eqf_7", "eqf_8"
        ],
        ["formalDocument"] =
        [
            "certificate", "diploma", "micro_credential_certificate", "school_advice", "testimonial",
            "no_official_document"
        ],
        ["supplementaryRole"] = ["announcement", "badge", "marketing", "promo"],
        ["supplementaryType"] = ["image", "text_http", "text_md", "text_plain", "uri", "video"]
    };
}
