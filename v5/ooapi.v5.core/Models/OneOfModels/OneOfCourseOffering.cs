using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ooapi.v5.core.Models.OneOfModels;

/// <summary>
/// 
/// </summary>
[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(CourseOffering), DiscriminatorValue = "courseOffering")]
[NotMapped]
public abstract class OneOfCourseOffering { }

/// <summary>
/// 
/// </summary>
public class OneOfCourseOfferingInstance : OneOfCourseOffering
{
    /// <summary>
    /// 
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public CourseOffering? CourseOffering { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="courseOffering"></param>
    public OneOfCourseOfferingInstance(Guid? id, CourseOffering? courseOffering)
    {
        Id = id;
        CourseOffering = courseOffering;
    }
}


