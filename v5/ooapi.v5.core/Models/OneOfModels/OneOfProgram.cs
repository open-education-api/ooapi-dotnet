namespace ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(Program), DiscriminatorValue = "program")]
[NotMapped]
public abstract class OneOfProgram { }

public class OneOfProgramInstance : OneOfProgram
{
    public Guid? Id { get; set; }
    public Program? Program { get; set; }

    public OneOfProgramInstance(Guid? id, Program? program)
    {
        Id = id;
        Program = program;
    }
}


