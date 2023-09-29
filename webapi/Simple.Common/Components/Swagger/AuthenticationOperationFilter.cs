using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Simple.Common.Swagger;

public class AuthenticationOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var actionAttributes = context.MethodInfo.GetCustomAttributes(true);
        var declaringTypeAttributes = context.MethodInfo.DeclaringType?.GetCustomAttributes(true);

        if(declaringTypeAttributes != null)
        {
            actionAttributes = actionAttributes.Concat(declaringTypeAttributes).ToArray();
        }

        var authorizeAttributes = actionAttributes.OfType<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            //operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            //operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
            var bearerScheme = new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            };
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement()
                {
                    [ bearerScheme ] = Array.Empty<string>()
                }
            };
        }
    }
}
