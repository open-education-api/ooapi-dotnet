using System.Text.RegularExpressions;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;

namespace ooapi.v5.Filters;

/// <summary>
/// BasePath Document Filter sets BasePath property of Swagger and removes it from the individual URL paths
/// </summary>
public class BasePathFilter : IDocumentFilter
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="basePath">BasePath to remove from Operations</param>
    public BasePathFilter(string basePath)
    {
        BasePath = basePath;
    }

    /// <summary>
    /// Gets the BasePath of the Swagger Doc
    /// </summary>
    /// <returns>The BasePath of the Swagger Doc</returns>
    public string BasePath { get; }

    /// <summary>
    /// Apply the filter
    /// </summary>
    /// <param name="swaggerDoc">OpenApiDocument</param>
    /// <param name="context">FilterContext</param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Servers.Add(new OpenApiServer() { Url = BasePath });

        var pathsToModify = swaggerDoc.Paths.Where(p => p.Key.StartsWith(BasePath)).ToList();
        foreach (var path in pathsToModify.Where(path => path.Key.StartsWith(BasePath)))
        {
            var newKey = Regex.Replace(path.Key, $"^{BasePath}", string.Empty, RegexOptions.None, TimeSpan.FromSeconds(2));
            swaggerDoc.Paths.Remove(path.Key);
            swaggerDoc.Paths.Add(newKey, path.Value);
        }
    }
}
