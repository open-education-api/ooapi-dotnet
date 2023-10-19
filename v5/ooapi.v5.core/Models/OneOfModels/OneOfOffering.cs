namespace ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(ProgramOffering), DiscriminatorValue = "programOffering")]
[SwaggerSubType(typeof(CourseOffering), DiscriminatorValue = "courseOffering")]
[SwaggerSubType(typeof(ComponentOffering), DiscriminatorValue = "componentOffering")]
[NotMapped]
public abstract class OneOfOffering { }
public class OneOfOfferingInstance : OneOfOffering
{
    public Guid? Id { get; set; }
    public ProgramOffering? ProgramOffering { get; set; }
    public CourseOffering? CourseOffering { get; set; }
    public ComponentOffering? ComponentOffering { get; set; }

    public OneOfOfferingInstance(Guid? id, ProgramOffering? programOffering)
    {
        Id = id;
        ProgramOffering = programOffering;
    }

    public OneOfOfferingInstance(Guid? id, CourseOffering? courseOffering)
    {
        Id = id;
        CourseOffering = courseOffering;
    }

    public OneOfOfferingInstance(Guid? id, ComponentOffering? componentOffering)
    {
        Id = id;
        ComponentOffering = componentOffering;
    }
}