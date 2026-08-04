using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class PersonMappingExtensions
{
    public static Person ToApiModel(this PersonEntity entity, string? consumer)
    {
        return new Person
        {
            PersonIdValue = entity.PersonId,
            PrimaryCode = new IdentifierEntry
            {
                CodeType = entity.PrimaryCodeType,
                Code = entity.PrimaryCode
            },
            GivenName = entity.GivenName,
            AlternateName = entity.AlternateName,
            PreferredName = entity.PreferredName,
            SurnamePrefix = entity.SurnamePrefix,
            Surname = entity.Surname,
            DisplayName = entity.DisplayName,
            Initials = entity.Initials,
            IdCheckName = entity.IdCheckName,
            ActiveEnrolment = entity.ActiveEnrolment,
            DateOfBirth = entity.DateOfBirth,
            CityOfBirth = entity.CityOfBirth,
            CountryOfBirth = string.IsNullOrEmpty(entity.CountryOfBirthJson)
                ? null
                : JsonSerializer.Deserialize<Country>(entity.CountryOfBirthJson),
            Nationality = string.IsNullOrEmpty(entity.NationalityJson)
                ? null
                : JsonSerializer.Deserialize<Nationality>(entity.NationalityJson),
            DateOfNationality = entity.DateOfNationality,
            Affiliations = string.IsNullOrEmpty(entity.AffiliationsJson)
                ? null
                : JsonSerializer.Deserialize<string[]>(entity.AffiliationsJson),
            Email = entity.Email,
            SecondaryEmail = entity.SecondaryEmail,
            TelephoneNumber = entity.TelephoneNumber,
            MobileNumber = entity.MobileNumber,
            PhotoSocial = entity.PhotoSocial,
            PhotoOfficial = entity.PhotoOfficial,
            Gender = entity.Gender,
            TitlePrefix = entity.TitlePrefix,
            TitleSuffix = entity.TitleSuffix,
            Office = entity.Office,
            Address = string.IsNullOrEmpty(entity.AddressJson)
                ? null
                : JsonSerializer.Deserialize<Address>(entity.AddressJson),
            IceName = entity.IceName,
            IcePhoneNumber = entity.IcePhoneNumber,
            IceRelation = string.IsNullOrEmpty(entity.IceRelationJson)
                ? null
                : JsonSerializer.Deserialize<string?>(entity.IceRelationJson),
            LanguageOfChoice = string.IsNullOrEmpty(entity.LanguageOfChoiceJson)
                ? null
                : JsonSerializer.Deserialize<string[]>(entity.LanguageOfChoiceJson),
            OtherCodes = entity.OtherCodes.ToApiModel(),
            AssignedNeeds = string.IsNullOrEmpty(entity.AssignedNeedsJson)
                ? null
                : JsonSerializer.Deserialize<PersonalNeed[]>(entity.AssignedNeedsJson),
            Consumer = entity.ConsumerJson.ToPersonConsumer(consumer),
            Ext = entity.ExtJson != null ? JsonSerializer.Deserialize<object>(entity.ExtJson) : null
        };
    }

    /// <summary>
    ///     Maps a <see cref="Person" /> API model to a new <see cref="PersonEntity" />, for the create half
    ///     of an upsert. Person has no relationship fields of its own, so no FK resolution is needed by
    ///     the caller after calling this method (unlike most other resources' <c>ToEntity</c>).
    /// </summary>
    public static PersonEntity ToEntity(this Person model)
    {
        // PersonEntity has a unique index on (PrimaryCodeType, PrimaryCode) - primaryCode is
        // optional per spec, but every omitting client can't fall back to the same empty string
        // without colliding on the second such person, so an omitted code value falls back to the
        // newly generated PersonId (already unique) instead.
        string personId = Guid.NewGuid().ToString();
        return new PersonEntity
        {
            PersonId = personId,
            PrimaryCodeType = model.PrimaryCode?.CodeType ?? string.Empty,
            PrimaryCode = model.PrimaryCode?.Code ?? personId,
            GivenName = model.GivenName,
            AlternateName = model.AlternateName,
            PreferredName = model.PreferredName,
            SurnamePrefix = model.SurnamePrefix,
            Surname = model.Surname,
            DisplayName = model.DisplayName,
            Initials = model.Initials,
            IdCheckName = model.IdCheckName,
            ActiveEnrolment = model.ActiveEnrolment,
            DateOfBirth = model.DateOfBirth,
            CityOfBirth = model.CityOfBirth,
            CountryOfBirthJson = model.CountryOfBirth != null ? JsonSerializer.Serialize(model.CountryOfBirth) : null,
            NationalityJson = model.Nationality != null ? JsonSerializer.Serialize(model.Nationality) : null,
            DateOfNationality = model.DateOfNationality,
            Gender = model.Gender,
            TitlePrefix = model.TitlePrefix,
            TitleSuffix = model.TitleSuffix,
            Office = model.Office,
            Email = model.Email,
            SecondaryEmail = model.SecondaryEmail,
            TelephoneNumber = model.TelephoneNumber,
            MobileNumber = model.MobileNumber,
            PhotoSocial = model.PhotoSocial,
            PhotoOfficial = model.PhotoOfficial,
            IceName = model.IceName,
            IcePhoneNumber = model.IcePhoneNumber,
            AffiliationsJson = model.Affiliations != null ? JsonSerializer.Serialize(model.Affiliations) : null,
            LanguageOfChoiceJson =
                model.LanguageOfChoice != null ? JsonSerializer.Serialize(model.LanguageOfChoice) : null,
            OtherCodes =
                model.OtherCodes?.Select(oc => new OtherCodeEntity { CodeType = oc.CodeType, Code = oc.Code })
                    .ToList() ?? [],
            AssignedNeedsJson = model.AssignedNeeds != null ? JsonSerializer.Serialize(model.AssignedNeeds) : null,
            AddressJson = model.Address != null ? JsonSerializer.Serialize(model.Address) : null,
            IceRelationJson = model.IceRelation != null ? JsonSerializer.Serialize(model.IceRelation) : null,
            ConsumerJson = model.Consumer != null ? JsonSerializer.Serialize(model.Consumer) : null,
            ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : null,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    /// <summary>
    ///     Updates an existing <see cref="PersonEntity" /> from a <see cref="Person" /> API model, for the
    ///     update half of an upsert.
    /// </summary>
    public static void UpdateFrom(this PersonEntity entity, Person model)
    {
        entity.PrimaryCodeType = model.PrimaryCode?.CodeType ?? entity.PrimaryCodeType;
        entity.PrimaryCode = model.PrimaryCode?.Code ?? entity.PrimaryCode;
        entity.GivenName = model.GivenName ?? entity.GivenName;
        entity.AlternateName = model.AlternateName ?? entity.AlternateName;
        entity.PreferredName = model.PreferredName ?? entity.PreferredName;
        entity.SurnamePrefix = model.SurnamePrefix ?? entity.SurnamePrefix;
        entity.Surname = model.Surname ?? entity.Surname;
        entity.DisplayName = model.DisplayName ?? entity.DisplayName;
        entity.Initials = model.Initials ?? entity.Initials;
        entity.IdCheckName = model.IdCheckName ?? entity.IdCheckName;
        entity.ActiveEnrolment = model.ActiveEnrolment;
        entity.DateOfBirth = model.DateOfBirth ?? entity.DateOfBirth;
        entity.CityOfBirth = model.CityOfBirth ?? entity.CityOfBirth;
        entity.CountryOfBirthJson = model.CountryOfBirth != null
            ? JsonSerializer.Serialize(model.CountryOfBirth)
            : entity.CountryOfBirthJson;
        entity.NationalityJson = model.Nationality != null
            ? JsonSerializer.Serialize(model.Nationality)
            : entity.NationalityJson;
        entity.DateOfNationality = model.DateOfNationality ?? entity.DateOfNationality;
        entity.Gender = model.Gender ?? entity.Gender;
        entity.TitlePrefix = model.TitlePrefix ?? entity.TitlePrefix;
        entity.TitleSuffix = model.TitleSuffix ?? entity.TitleSuffix;
        entity.Office = model.Office ?? entity.Office;
        entity.Email = model.Email ?? entity.Email;
        entity.SecondaryEmail = model.SecondaryEmail ?? entity.SecondaryEmail;
        entity.TelephoneNumber = model.TelephoneNumber ?? entity.TelephoneNumber;
        entity.MobileNumber = model.MobileNumber ?? entity.MobileNumber;
        entity.PhotoSocial = model.PhotoSocial ?? entity.PhotoSocial;
        entity.PhotoOfficial = model.PhotoOfficial ?? entity.PhotoOfficial;
        entity.IceName = model.IceName ?? entity.IceName;
        entity.IcePhoneNumber = model.IcePhoneNumber ?? entity.IcePhoneNumber;
        entity.AffiliationsJson = model.Affiliations != null
            ? JsonSerializer.Serialize(model.Affiliations)
            : entity.AffiliationsJson;
        entity.LanguageOfChoiceJson = model.LanguageOfChoice != null
            ? JsonSerializer.Serialize(model.LanguageOfChoice)
            : entity.LanguageOfChoiceJson;
        entity.OtherCodes.SyncFrom(model.OtherCodes);
        entity.AssignedNeedsJson = model.AssignedNeeds != null
            ? JsonSerializer.Serialize(model.AssignedNeeds)
            : entity.AssignedNeedsJson;
        entity.AddressJson = model.Address != null ? JsonSerializer.Serialize(model.Address) : entity.AddressJson;
        entity.IceRelationJson = model.IceRelation != null
            ? JsonSerializer.Serialize(model.IceRelation)
            : entity.IceRelationJson;
        entity.ConsumerJson = model.Consumer != null ? JsonSerializer.Serialize(model.Consumer) : entity.ConsumerJson;
        entity.ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : entity.ExtJson;
        entity.ModifiedAt = DateTime.UtcNow;
    }
}
