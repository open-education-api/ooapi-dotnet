namespace ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(ProgramOffering), DiscriminatorValue = "programOffering")]
[SwaggerSubType(typeof(CourseOffering), DiscriminatorValue = "courseOffering")]
[SwaggerSubType(typeof(ComponentOffering), DiscriminatorValue = "componentOffering")]
public abstract class OneOfOffering { }


