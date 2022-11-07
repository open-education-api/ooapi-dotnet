namespace IO.Swagger.Models;
using Swashbuckle.AspNetCore.Annotations;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(ProgramOffering), DiscriminatorValue = "programOffering")]
[SwaggerSubType(typeof(CourseOffering), DiscriminatorValue = "courseOffering")]
[SwaggerSubType(typeof(ComponentOffering), DiscriminatorValue = "componentOffering")]
public abstract class OneOfOfferingNoIdentifier { }


