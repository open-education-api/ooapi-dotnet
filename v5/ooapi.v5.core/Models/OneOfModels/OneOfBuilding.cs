namespace ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("objectType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(Building), DiscriminatorValue = "building")]
[NotMapped]
public abstract class OneOfBuilding { }



public class OneOfBuildingInstance : OneOfBuilding
{
    public Guid? Id { get; set; }
    public Building? Building { get; set; }

    public OneOfBuildingInstance(Guid? id, Building? building)
    {
        Id = id;
        Building = building;
    }
}


