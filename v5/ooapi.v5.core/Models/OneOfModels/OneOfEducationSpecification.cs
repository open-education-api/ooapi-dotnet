using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ooapi.v5.core.Models.OneOfModels;

/// <summary>
/// 
/// </summary>
[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(EducationSpecification), DiscriminatorValue = "educationSpecification")]
[NotMapped]
public abstract class OneOfEducationSpecification { }

/// <summary>
/// 
/// </summary>
public class OneOfEducationSpecificationInstance : OneOfEducationSpecification
{
    /// <summary>
    /// 
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public EducationSpecification? EducationSpecification { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="educationSpecificationId"></param>
    /// <param name="educationSpecificationInstance"></param>
    public OneOfEducationSpecificationInstance(Guid? educationSpecificationId, EducationSpecification? educationSpecificationInstance)
    {
        Id = educationSpecificationId;
        EducationSpecification = educationSpecificationInstance;
    }
}