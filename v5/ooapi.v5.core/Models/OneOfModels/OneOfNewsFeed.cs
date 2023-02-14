namespace ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

[SwaggerDiscriminator("objectType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(NewsFeed), DiscriminatorValue = "newsFeed")]
[NotMapped]
public abstract class OneOfNewsFeed { }


public class OneOfNewsFeedInstance : OneOfNewsFeed
{
    public Guid? Id { get; set; }
    public NewsFeed? NewsFeed { get; set; }

    public OneOfNewsFeedInstance(Guid? id, NewsFeed? newsFeed)
    {
        Id = id;
        NewsFeed = newsFeed;
    }
}


