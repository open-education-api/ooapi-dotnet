namespace ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(AcademicSession), DiscriminatorValue = "acadamicSessiong")]
[NotMapped]
public abstract class OneOfAcadamicSession { }


