using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ooapi.v5.Attributes;

/// <summary>
/// Model state validation attribute
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Source: https://github.com/swagger-api/swagger-codegen/blob/master/samples/server/petstore/aspnetcore/src/IO.Swagger/Attributes/ValidateModelStateAttribute.cs")]
public class ValidateModelStateAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Called before the action method is invoked
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Per https://blog.markvincze.com/how-to-validate-action-parameters-with-dataannotation-attributes/
        if (context.ActionDescriptor is ControllerActionDescriptor descriptor)
        {
            foreach (var parameter in descriptor.MethodInfo.GetParameters())
            {
                object? args = null;
                if (parameter.Name is not null && context.ActionArguments.ContainsKey(parameter.Name))
                {
                    args = context.ActionArguments[parameter.Name];
                }

                ValidateAttributes(parameter, args, context.ModelState);
            }
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }

    private static void ValidateAttributes(ParameterInfo parameter, object? args, ModelStateDictionary modelState)
    {
        foreach (var attributeData in parameter.CustomAttributes)
        {
            var attributeInstance = parameter.GetCustomAttribute(attributeData.AttributeType);

            if (attributeInstance is ValidationAttribute validationAttribute)
            {
                var isValid = validationAttribute.IsValid(args);
                if (!isValid)
                {
                    modelState.AddModelError(parameter.Name ?? "", validationAttribute.FormatErrorMessage(parameter.Name ?? ""));
                }
            }
        }
    }
}
