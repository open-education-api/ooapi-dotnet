using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class AddressMappingExtensions
{
    public static Address ToApiModel(this AddressEntity entity)
    {
        string addressType = string.IsNullOrEmpty(entity.AddressType) ? "postal" : entity.AddressType;

        Geolocation? geolocation = null;
        if (entity.Latitude.HasValue && entity.Longitude.HasValue)
            geolocation = new Geolocation
            {
                Latitude = entity.Latitude.Value,
                Longitude = entity.Longitude.Value
            };

        return new Address
        {
            AddressType = addressType,
            Street = entity.Street,
            StreetNumber = entity.StreetNumber,
            Additional = entity.Name != null
                ? [new LanguageTypedString { Language = "en", Value = entity.Name }]
                : null,
            PostCode = entity.PostCode,
            City = entity.City,
            CountryCode = entity.CountryCode != null ? new Country { Iso3166Alpha2 = entity.CountryCode } : null,
            Geolocation = geolocation,
            Ext = string.IsNullOrEmpty(entity.ExtJson)
                ? null
                : JsonSerializer.Deserialize<object>(entity.ExtJson)
        };
    }
}
