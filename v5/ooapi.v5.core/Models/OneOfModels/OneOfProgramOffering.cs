namespace ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(ProgramOffering), DiscriminatorValue = "programOffering")]
[NotMapped]
public abstract class OneOfProgramOffering { }


public class OneOfProgramOfferingInstance : OneOfProgramOffering
{
    public Guid? Id { get; set; }
    public ProgramOffering? ProgramOffering { get; set; }

    public OneOfProgramOfferingInstance(Guid? id, ProgramOffering? programOffering)
    {
        Id = id;
        ProgramOffering = programOffering;
    }
}


