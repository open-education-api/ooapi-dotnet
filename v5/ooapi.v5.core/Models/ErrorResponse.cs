using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// A system message including the error code and an explanation
    /// </summary>
    [DataContract]
    public class ErrorResponse
    {
        public ErrorResponse(int statusCode, string detail = "")
        {
            Status = statusCode.ToString();
            Detail = detail;
            switch (statusCode)
            {
                case 400:
                    {
                        Title = "Bad request";
                        break;
                    }
                case 401:
                    {
                        Title = "Unauthorized";
                        break;
                    }
                case 403:
                    {
                        Title = "Forbidden";
                        break;
                    }
                case 405:
                    {
                        Title = "Method not allowed";
                        break;
                    }
                case 429:
                    {
                        Title = "Too many requests";
                        break;
                    }
                case 500:
                    {
                        Title = "Internal Server Error";
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// The HTTP status code
        /// </summary>
        /// <value>The HTTP status code</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// A short, human-readable summary of the problem type
        /// </summary>
        /// <value>A short, human-readable summary of the problem type</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// A human-readable explanation specific to this occurrence of the problem
        /// </summary>
        /// <value>A human-readable explanation specific to this occurrence of the problem</value>

        [JsonProperty(PropertyName = "detail")]
        public string Detail { get; set; }
    }

}
