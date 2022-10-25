namespace IO.Swagger.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;

[SwaggerDiscriminator("objectType")]
[SwaggerSubType(typeof(Guid), DiscriminatorValue = "identifier")]
[SwaggerSubType(typeof(NewsFeed), DiscriminatorValue = "news-feed")]
public abstract class OneOfNewsFeed { }


