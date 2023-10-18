using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ooapi.v5.Models;

namespace ooapi.v5.Helpers;

/// <summary>
/// 
/// </summary>
public static class LanguageTypedStringJsonConverter
{
    internal static List<LanguageTypedString>? GetLanguageTypesStringList(string? languageTypedStringValue)
    {
        if (string.IsNullOrEmpty(languageTypedStringValue))
        {
            return null;
        }

        try
        {
            var result = new List<LanguageTypedString>();
            var jArray = JsonConvert.DeserializeObject<JArray>(languageTypedStringValue);
            if (jArray == null)
            {
                return null;
            }

            foreach (var item in jArray)
            {
                var language = item.Value<string>("language");
                var value = item.Value<string>("value");

                if (language is not null && value is not null)
                {
                    result.Add(new LanguageTypedString() { Language = language, Value = value });
                }
            }
            return result;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
