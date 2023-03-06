using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    public class Attribute : LanguageTypedProperty
    {

        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonIgnore]
        public string ModelTypeName { get; set; }

    }
}