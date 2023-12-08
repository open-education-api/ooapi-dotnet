using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.Enums;
using ooapi.v5.Models;

namespace ooapi.v5.Helpers;

public static class ConsumerConverter
{
    public static List<JObject> GetDynamicConsumers(IEnumerable<ConsumerBase> consumers)
    {
        var result = new List<JObject>();
        var groupedConsumers = consumers.GroupBy(x => x.ConsumerKey);
        foreach (var groupedConsumer in groupedConsumers)
        {
            var resultObject = new JObject
            {
                { "consumerKey", groupedConsumer.First().ConsumerKey }
            };

            foreach (var consumer in groupedConsumer)
            {
                var propertyType = consumer.PropertyType;
                var propertyName = consumer.PropertyName;
                var propertyValue = consumer.PropertyValue;

                switch (propertyType)
                {
                    case ConsumerPropertyType.String:
                        resultObject.Add(propertyName, propertyValue);
                        break;
                    case ConsumerPropertyType.JArray:
                        dynamic? array = JsonConvert.DeserializeObject<object>(propertyValue);
                        resultObject.Add(propertyName, array);
                        break;
                    case ConsumerPropertyType.JObject:
                        dynamic? obj = JsonConvert.DeserializeObject<object>(propertyValue);
                        resultObject.Add(propertyName, obj);
                        break;
                    case ConsumerPropertyType.Int:
                        if (int.TryParse(propertyValue, out var value))
                        {
                            resultObject.Add(propertyName, value);
                        }
                        break;
                    case ConsumerPropertyType.Bool:
                        if (bool.TryParse(propertyValue, out var res))
                        {
                            resultObject.Add(propertyName, res);
                        }
                        break;
                }
            }

            result.Add(resultObject);
        }

        return result;
    }

    /// <summary>
    /// Gets the consumer property value.
    /// </summary>
    /// <param name="propertyType">The property type.</param>
    /// <param name="propertyValue">The property value.</param>
    /// <returns>The <see cref="JToken"/>.</returns>
    [ExcludeFromCodeCoverage(Justification = "Function is currently not used")]
    public static JToken GetConsumerPropertyValue(ConsumerPropertyType propertyType, string propertyValue)
    {
        switch (propertyType)
        {
            case ConsumerPropertyType.String:
                return propertyValue;
            case ConsumerPropertyType.JArray:
            case ConsumerPropertyType.JObject:
            case ConsumerPropertyType.Int:
            case ConsumerPropertyType.Bool:
            default:
                break;
        }
        return string.Empty;
    }
}
