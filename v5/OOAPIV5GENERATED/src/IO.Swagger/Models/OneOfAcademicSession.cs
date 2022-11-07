namespace IO.Swagger.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;

[SwaggerDiscriminator("objectType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(AcademicSession), DiscriminatorValue = "academic-session")]
public abstract class OneOfAcademicSession { }


