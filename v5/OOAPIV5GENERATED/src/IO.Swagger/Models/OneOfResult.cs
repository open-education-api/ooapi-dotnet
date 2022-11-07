namespace IO.Swagger.Models;
using Swashbuckle.AspNetCore.Annotations;


[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(ProgramResult), DiscriminatorValue = "programResult")]
[SwaggerSubType(typeof(CourseResult), DiscriminatorValue = "courseResult")]
[SwaggerSubType(typeof(ComponentResult), DiscriminatorValue = "componentResult")]
public abstract class OneOfResult { }


