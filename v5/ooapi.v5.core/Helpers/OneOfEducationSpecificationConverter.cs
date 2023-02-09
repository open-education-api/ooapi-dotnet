using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using ooapi.v5.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ooapi.v5.Helpers
{
    public class OneOfEducationSpecificationConverter: JsonConverter

    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

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

                JToken educationSpecificationResult =null;
                string educationSpecificationIdResult = "";
                foreach (var prop in properties)
                {
                    if (prop.Name == "EducationSpecification" && !prop.Value.IsNullOrEmpty())
                    {
                        educationSpecificationResult = prop.Value;
                        //educationSpecificationResult = JsonConvert.SerializeObject(prop.Value);
                    }
                    if (prop.Name == "Id" && prop.Value != null)
                    {
                        educationSpecificationIdResult = prop.Value.ToString();
                    }
                }

                if (educationSpecificationResult != null)
                    educationSpecificationResult.WriteTo(writer);
                    //writer.WriteValue(educationSpecificationResult);
                else
                    writer.WriteValue(educationSpecificationIdResult);


            }
        }

        //public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        //{
        //    JToken t = JToken.FromObject(value);

        //    if (t.Type != JTokenType.Object)
        //    {
        //        t.WriteTo(writer);
        //    }
        //    else
        //    {

        //        JObject o = (JObject)t;
        //        IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();

        //        o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));


        //        o.WriteTo(writer);
        //    }
        //}
    }
}
