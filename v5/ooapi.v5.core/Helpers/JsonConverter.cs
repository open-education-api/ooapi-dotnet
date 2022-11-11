using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ooapi.v5.Models;
using System.Xml.Linq;

namespace ooapi.v5.Helpers;

public static class JsonConverter
{
    internal static List<LanguageTypedString> GetLanguageTypesStringList(string languageTypedStringValue)
    {

        if (string.IsNullOrEmpty(languageTypedStringValue))
            return null;
        try
        {
            List<LanguageTypedString> result = new List<LanguageTypedString>();
            JArray jArray = JsonConvert.DeserializeObject<JArray>(languageTypedStringValue);
            if (jArray == null)
                return null;
            foreach (var item in jArray)
            {
                result.Add(new LanguageTypedString() { Language = item.Value<string>("language"), Value = item.Value<string>("value") });
            }
            return result;
        }
        catch (Exception)
        {
            return null;
        }

    }
}
