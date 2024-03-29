using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ooapi.v5.core.Models.OneOfModels;


[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(CourseOffering), DiscriminatorValue = "courseOffering")]
[NotMapped]
public abstract class OneOfCourseOffering { }

public class OneOfCourseOfferingInstance : OneOfCourseOffering
{

    public Guid? Id { get; set; }


    public CourseOffering? CourseOffering { get; set; }


    /// <param name="id"></param>
    /// <param name="courseOffering"></param>
    public OneOfCourseOfferingInstance(Guid? id, CourseOffering? courseOffering)
    {
        Id = id;
        CourseOffering = courseOffering;
    }
}