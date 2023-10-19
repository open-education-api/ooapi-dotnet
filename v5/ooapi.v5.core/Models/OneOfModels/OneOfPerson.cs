namespace ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("objectType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(Person), DiscriminatorValue = "person")]
[NotMapped]
public abstract class OneOfPerson { }

public class OneOfPersonInstance : OneOfPerson
{
    public Guid? Id { get; set; }
    public Person? Person { get; set; }

    public OneOfPersonInstance(Guid? id, Person? person)
    {
        Id = id;
        Person = person;
    }
}