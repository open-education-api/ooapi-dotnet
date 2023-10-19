namespace ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(AcademicSession), DiscriminatorValue = "academicSession")]
[NotMapped]
public abstract class OneOfAcademicSession { }

public class OneOfAcademicSessionInstance : OneOfAcademicSession
{
    public Guid? Id { get; set; }
    public AcademicSession? AcademicSession { get; set; }

    public OneOfAcademicSessionInstance(Guid? id, AcademicSession? academicSession)
    {
        Id = id;
        AcademicSession = academicSession;
    }
}

