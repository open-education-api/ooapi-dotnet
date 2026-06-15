using ooapi.v5.Models;

namespace ooapi.v5.core.Extensions
{
    public static class LanguageTypedPropertyExtensions
    {
        public static List<LanguageTypedString> ExtractStringsByPropertyName(this IEnumerable<LanguageTypedProperty> properties, string propertyName) { 
            return properties?.Where(x => x.PropertyName.Equals(propertyName))
                             .Select(x => new LanguageTypedString()
                             {
                                 Language = x.Language,
                                 Value = x.Value
                             }).ToList() ?? new List<LanguageTypedString>();
        }
    }
}
