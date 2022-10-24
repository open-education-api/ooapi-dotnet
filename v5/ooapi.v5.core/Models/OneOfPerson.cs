namespace ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;

[SwaggerDiscriminator("objectType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(Person), DiscriminatorValue = "person")]
public abstract class OneOfPerson { }


