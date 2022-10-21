using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace IO.Swagger.Helpers
{
    public class OneOfSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {

            schema.OneOf = new List<OpenApiSchema>
            {

                new OpenApiSchema {Type = "string", Description = "A"},
                new OpenApiSchema {Type = "string", Description = "B"}
            };

        }
    }

}
