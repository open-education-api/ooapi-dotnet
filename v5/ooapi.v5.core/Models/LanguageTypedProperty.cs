using Newtonsoft.Json;
using ooapi.v5.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    public class LanguageTypedProperty
    {


        public string PropertyName { get; set; }

        public string Language { get; set; }

        public string Value { get; set; }


    }
}
