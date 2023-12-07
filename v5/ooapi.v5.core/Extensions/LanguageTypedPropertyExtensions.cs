using ooapi.v5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ooapi.v5.core.Extensions
{
    public static class LanguageTypedPropertyExtensions
    {
        public static List<LanguageTypedString> ExtractStringsByPropertyName(this IEnumerable<LanguageTypedProperty> properties, string propertyName) { 
            return properties.Where(x => x.PropertyName.Equals(propertyName))
                             .Select(x => new LanguageTypedString()
                             {
                                 Language = x.Language,
                                 Value = x.Value
                             }).ToList();
        }
    }
}
