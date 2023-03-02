namespace ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(ProgramOffering), DiscriminatorValue = "programOffering")]
[SwaggerSubType(typeof(CourseOffering), DiscriminatorValue = "courseOffering")]
[SwaggerSubType(typeof(ComponentOffering), DiscriminatorValue = "componentOffering")]
[NotMapped]
public abstract class OneOfOfferingNoIdentifier { }


public class OneOfOfferingNoIdentifierInstance : OneOfOfferingNoIdentifier
{
    public Guid? Id { get; set; }
    public Offering? Offering { get; set; }

    public OneOfOfferingNoIdentifierInstance(Guid? id, Offering? offering)
    {
        Id = id;
        Offering = offering;
    }
}


