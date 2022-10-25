namespace ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("objectType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(Person), DiscriminatorValue = "person")]
[NotMapped]
public abstract class OneOfPerson { }


