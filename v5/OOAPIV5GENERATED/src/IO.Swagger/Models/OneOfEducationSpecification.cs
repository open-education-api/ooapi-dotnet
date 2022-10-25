namespace IO.Swagger.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;

[SwaggerDiscriminator("objectType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(EducationSpecification), DiscriminatorValue = "education-specification")]
public abstract class OneOfEducationSpecification { }


