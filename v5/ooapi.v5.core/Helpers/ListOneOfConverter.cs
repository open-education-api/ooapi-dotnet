using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using ooapi.v5.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ooapi.v5.Helpers
{
    public class ListOneOfConverter : JsonConverter

    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);

            if (t.Type != JTokenType.Array)
            {
                t.WriteTo(writer);
            }
            else
            {
                JArray a = (JArray)t;

                JArray array = new JArray();

                foreach (JObject o in a)
                {
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

                    if (result.HasValues)
                        array.Add(result);
                    else array.Add(resultId);

                    //if (jToken != null)
                    //    array.Add(jToken);
                    //else array.Add(resultId);
                }
                //if (array != null)
                //    array.WriteTo(writer);
                //else
                //    writer.WriteValue(resultId);

                array.WriteTo(writer);

                //    JObject o = (JObject)t;


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
