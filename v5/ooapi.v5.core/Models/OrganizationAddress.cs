using Newtonsoft.Json;
using ooapi.v5.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    [DataContract]
    public class OrganizationAddress
    {

        [JsonIgnore]
        public Guid OrganizationId { get; set; }
        [JsonIgnore]
        public Organization Organization { get; set; }

        [JsonIgnore]
        public Guid AddressId { get; set; }
        [JsonIgnore]
        public Address Address { get; set; }


    }
}
