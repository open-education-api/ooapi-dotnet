using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ooapi.v5.core.Models.OneOfModels;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(EducationSpecification), DiscriminatorValue = "educationSpecification")]
[NotMapped]
public abstract class OneOfEducationSpecification { }

public class OneOfEducationSpecificationInstance : OneOfEducationSpecification
{
    public Guid? Id { get; set; }
    public EducationSpecification? EducationSpecification { get; set; }

    public OneOfEducationSpecificationInstance(Guid? educationSpecificationId, EducationSpecification? educationSpecificationInstance)
    {
        Id = educationSpecificationId;
        EducationSpecification = educationSpecificationInstance;
    }
}