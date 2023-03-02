using Newtonsoft.Json;

using ooapi.v5.Enums;

namespace ooapi.v5.Models
{

    public class Consumer
    {
        [JsonIgnore]
        public Guid Id { get; set; } 

        [JsonIgnore]
        public string ModelTypeName { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "consumerKey")]
        public string ConsumerKey { get; set; }

        [JsonIgnore]
        public ConsumerPropertyTypeEnum PropertyType { get; set; } = 0;

        [JsonProperty(PropertyName = "propertyName")]
        public string PropertyName { get; set; }

        [JsonProperty(PropertyName = "propertyValue")]
        public string PropertyValue { get; set; }

    }
}
