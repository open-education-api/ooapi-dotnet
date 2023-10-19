namespace ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(Course), DiscriminatorValue = "course")]
[NotMapped]
public abstract class OneOfCourse { }

public class OneOfCourseInstance : OneOfCourse
{
    public Guid? Id { get; set; }
    public Course? Course { get; set; }

    public OneOfCourseInstance(Guid? id, Course? course)
    {
        Id = id;
        Course = course;
    }
}