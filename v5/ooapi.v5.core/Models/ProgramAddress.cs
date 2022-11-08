using Newtonsoft.Json;
using ooapi.v5.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    [DataContract]
    public class ProgramAddress
    {

        [JsonIgnore]
        public Guid ProgramId { get; set; }
        [JsonIgnore]
        public Program Program { get; set; }

        [JsonIgnore]
        public Guid AddressId { get; set; }
        [JsonIgnore]
        public Address Address { get; set; }


    }
}
