using Newtonsoft.Json;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

/// <summary>
/// 
/// </summary>
[DataContract]
public partial class ProgramOffering : OfferingShared
{
    /// <summary>
    /// The Program that is offered in this programoffering. [&#x60;expandable&#x60;](#tag/program_model) By default only the &#x60;programId&#x60; (a string) is returned. If the client requested an expansion of &#x60;program&#x60; the full program object should be returned. 
    /// </summary>
    /// <value>The Program that is offered in this programoffering. [&#x60;expandable&#x60;](#tag/program_model) By default only the &#x60;programId&#x60; (a string) is returned. If the client requested an expansion of &#x60;program&#x60; the full program object should be returned. </value>
    [JsonProperty(PropertyName = "program")]
    [NotMapped]
    [JsonConverter(typeof(OneOfConverter))]
    public OneOfProgram? OneOfProgram
    {
        get
        {
            if (ProgramId == null)
            {
                return null;
            }

            return new OneOfProgramInstance(ProgramId, Program);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public Guid? ProgramId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public Program? Program { get; set; }
}
