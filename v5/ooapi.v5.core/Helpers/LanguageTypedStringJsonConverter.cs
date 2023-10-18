using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ooapi.v5.Models;

namespace ooapi.v5.Helpers;


public static class LanguageTypedStringJsonConverter
{
    internal static List<LanguageTypedString> GetLanguageTypesStringList(string? languageTypedStringValue)
    {
        if (string.IsNullOrEmpty(languageTypedStringValue))
        {
            return new List<LanguageTypedString>();
        }

        try
        {
            var result = new List<LanguageTypedString>();
            var jArray = JsonConvert.DeserializeObject<JArray>(languageTypedStringValue);
            if (jArray == null)
            {
                return new List<LanguageTypedString>();
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
            return new List<LanguageTypedString>();
        }
    }
}
