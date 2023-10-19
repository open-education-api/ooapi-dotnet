namespace ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(Component), DiscriminatorValue = "component")]
[NotMapped]
public abstract class OneOfComponent { }

public class OneOfComponentInstance : OneOfComponent
{
    public Guid? Id { get; set; }
    public Component? Component { get; set; }

    public OneOfComponentInstance(Guid? id, Component? component)
    {
        Id = id;
        Component = component;
    }
}