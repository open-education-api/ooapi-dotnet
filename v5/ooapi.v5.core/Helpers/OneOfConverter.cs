using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ooapi.v5.Helpers;

public class OneOfConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is null)
        {
            writer.WriteNull();
            return;
        }

        var t = JToken.FromObject(value);

        if (t.Type != JTokenType.Object)
        {
            t.WriteTo(writer);
        }
        else
        {
            var o = (JObject)t;

            var properties = o.Properties();

            var result = new JObject();
            JToken? jToken = null;
            var resultId = "";
            foreach (var prop in properties)
            {

                if (prop.Name != "Id" && !prop.Value.IsNullOrEmpty())
                {
                    jToken = prop.Value;
                    if (jToken is not null)
                    {
                        foreach (var item in jToken)
                        {
                            var val = item.Values().Values().FirstOrDefault();
                            if (val is not null)
                            {
                                var valie = val.ToString();
                                if (!string.IsNullOrEmpty(valie))
                                {
                                    result.Add(item);
                                }
                            }
                        }
                    }
                }
                if (prop.Name == "Id" && prop.Value != null)
                {
                    resultId = prop.Value.ToString();
                }
            }

            if (jToken is not null)
            {
                result.WriteTo(writer);
            }
            else
            {
                writer.WriteValue(resultId);

            }
        }
    }

    public override bool CanConvert(Type objectType)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
