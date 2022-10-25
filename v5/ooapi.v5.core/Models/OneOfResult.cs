namespace ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(ProgramResult), DiscriminatorValue = "programResult")]
[SwaggerSubType(typeof(CourseResult), DiscriminatorValue = "courseResult")]
[SwaggerSubType(typeof(ComponentResult), DiscriminatorValue = "componentResult")]
[NotMapped]
public abstract class OneOfResult { }


