namespace IO.Swagger.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;

[SwaggerDiscriminator("objectType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(Course), DiscriminatorValue = "course")]
public abstract class OneOfCourse { }


