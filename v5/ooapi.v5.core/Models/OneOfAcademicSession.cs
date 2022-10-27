namespace ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(AcademicSession), DiscriminatorValue = "academicSession")]
[NotMapped]
public abstract class OneOfAcademicSession { }


