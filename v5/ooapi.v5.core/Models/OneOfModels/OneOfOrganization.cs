namespace ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("resultType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(Organization), DiscriminatorValue = "organization")]
[NotMapped]
public abstract class OneOfOrganization { }


public class OneOfOrganizationInstance : OneOfOrganization
{
    public Guid? Id { get; set; }
    public Organization? Organization { get; set; }

    public OneOfOrganizationInstance(Guid? id, Organization? organization)
    {
        Id = id;
        Organization = organization;
    }
}


