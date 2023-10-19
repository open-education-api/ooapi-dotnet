namespace ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(ProgramResult), DiscriminatorValue = "programResult")]
[SwaggerSubType(typeof(CourseResult), DiscriminatorValue = "courseResult")]
[SwaggerSubType(typeof(ComponentResult), DiscriminatorValue = "componentResult")]
[NotMapped]
public abstract class OneOfResult { }

public class OneOfResultInstance : OneOfResult
{
    public Guid? Id { get; set; }
    public ProgramResult? ProgramResult { get; set; }
    public CourseResult? CourseResult { get; set; }
    public ComponentResult? ComponentResult { get; set; }

    public OneOfResultInstance(Guid? id, ProgramResult? programResult)
    {
        Id = id;
        ProgramResult = programResult;
    }

    public OneOfResultInstance(Guid? id, CourseResult? courseResult)
    {
        Id = id;
        CourseResult = courseResult;
    }

    public OneOfResultInstance(Guid? id, ComponentResult? componentResult)
    {
        Id = id;
        ComponentResult = componentResult;
    }
}