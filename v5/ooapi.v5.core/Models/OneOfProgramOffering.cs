namespace ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(ProgramOffering), DiscriminatorValue = "programOffering")]
[NotMapped]
public abstract class OneOfProgramOffering { }


