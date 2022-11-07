namespace ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("objectType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(Building), DiscriminatorValue = "building")]
[NotMapped]
public abstract class OneOfBuilding { }



