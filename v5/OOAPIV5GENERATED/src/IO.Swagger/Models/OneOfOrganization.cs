namespace IO.Swagger.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;

[SwaggerDiscriminator("objectType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(Organization), DiscriminatorValue = "organization")]
public abstract class OneOfOrganization { }


