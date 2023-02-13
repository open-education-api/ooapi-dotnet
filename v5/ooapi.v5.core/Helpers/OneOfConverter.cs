using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using ooapi.v5.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ooapi.v5.Helpers
{
    public class OneOfConverter : JsonConverter

    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);

            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {

                JObject o = (JObject)t;

                var properties = o.Properties();

                JObject result = new JObject();
                JToken jToken = null;
                string resultId = "";
                foreach (var prop in properties)
                {
                    try
                    {
                        if (prop.Name != "Id" && !prop.Value.IsNullOrEmpty())
                        {
                            jToken = prop.Value;
                            foreach (var item in jToken)
                            {
                                var val = item.Values().Values().FirstOrDefault();
                                var isNull = (val == null);
                                if (!isNull)
                                {
                                    string valie = val.ToString();
                                    if (!string.IsNullOrEmpty(valie))
                                    {
                                        result.Add(item);
                                    }
                                }
                            }
                        }
                        if (prop.Name == "Id" && prop.Value != null)
                        {
                            resultId = prop.Value.ToString();
                        }
                    }
                    catch (Exception)
                    {
                        //throw;
                    }
                }

                if (jToken != null)
                    result.WriteTo(writer);
                else
                    writer.WriteValue(resultId);


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
}
