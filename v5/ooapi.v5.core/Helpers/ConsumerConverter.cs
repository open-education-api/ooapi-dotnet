using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.Enums;
using ooapi.v5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ooapi.v5.Helpers
{
    public static  class ConsumerConverter
    {
        public static List<JObject> GetDynamicConsumers(List<Consumer> consumers)
        {

            //JArray result = new JArray();
            List<JObject> result = new List<JObject>();
            var groupedConsumers = consumers.GroupBy(x => x.ConsumerKey);
            foreach (var groupedConsumer in groupedConsumers)
            {
                JObject resultObject = new JObject();
                resultObject.Add("consumerKey", groupedConsumer.FirstOrDefault().ConsumerKey);
                foreach (var consumer in groupedConsumer)
                {
                    ConsumerPropertyTypeEnum propertyType = consumer.PropertyType;
                    string propertyName = consumer.PropertyName;
                    string propertyValue = consumer.PropertyValue; //.Replace("\\","");


                    switch (propertyType)
                    {
                        case ConsumerPropertyTypeEnum.String:
                            resultObject.Add(propertyName, propertyValue);
                            break;
                        case ConsumerPropertyTypeEnum.JArray:
                            dynamic array = JsonConvert.DeserializeObject<object>(propertyValue);
                            //                            JArray jArray = JArray.Parse(test);
                            resultObject.Add(propertyName, array);
                            break;
                        case ConsumerPropertyTypeEnum.JObject:
                            dynamic obj = JsonConvert.DeserializeObject<object>(propertyValue);
                            resultObject.Add(propertyName, obj);
                            break;
                        case ConsumerPropertyTypeEnum.Int:
                            if (int.TryParse(propertyValue, out int value))
                            {
                                resultObject.Add(propertyName, value);
                            }
                            break;
                        case ConsumerPropertyTypeEnum.Bool:
                            if (bool.TryParse(propertyValue, out bool res))
                            {
                                resultObject.Add(propertyName, res);
                            }
                            break;
                        default:
                            break;
                    }
                }
                result.Add(resultObject);
            }

            return result;
        }

        public static JToken GetConsumerPropertyValue(ConsumerPropertyTypeEnum propertyType, string propertyValue)
        {

            switch (propertyType)
            {
                case ConsumerPropertyTypeEnum.String:
                    return propertyValue;
                case ConsumerPropertyTypeEnum.JArray:

                    break;
                case ConsumerPropertyTypeEnum.JObject:
                    break;
                case ConsumerPropertyTypeEnum.Int:
                    break;
                case ConsumerPropertyTypeEnum.Bool:
                    break;
                default:
                    break;
            }
            return "";
        }


    }
}
