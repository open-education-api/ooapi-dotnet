using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ooapi.v5.Helpers;


public class ListOneOfConverter : JsonConverter
{

    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is null)
        {
            writer.WriteNull();
            return;
        }

        var t = JToken.FromObject(value);

        if (t.Type != JTokenType.Array)
        {
            t.WriteTo(writer);
        }
        else
        {
            var a = (JArray)t;
            var array = new JArray();

            foreach (var o in a.OfType<JObject>())
            {
                var properties = o.Properties();

                var result = new JObject();
                var resultId = "";
                foreach (var prop in properties)
                {
                    if (prop.Name != "Id" && !prop.Value.IsNullOrEmpty())
                    {
                        var jToken = prop.Value;
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

                if (result.HasValues)
                {
                    array.Add(result);
                }
                else
                {
                    array.Add(resultId);
                }
            }

            array.WriteTo(writer);
        }
    }


    /// <param name="objectType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override bool CanConvert(Type objectType)
    {
        throw new NotImplementedException();
    }


    /// <param name="reader"></param>
    /// <param name="objectType"></param>
    /// <param name="existingValue"></param>
    /// <param name="serializer"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }


}
