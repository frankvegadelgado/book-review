using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookReview.Web.Host.Startup
{
    public class AuthorizationDocumentFilter : IDocumentFilter
    {
        private const string _apiVersion2 = "v2";
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (context.DocumentName == _apiVersion2)
            {
                var securityScheme = new OpenApiSecurityScheme()
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                };
                swaggerDoc.Components.SecuritySchemes.Add("bearerAuth", securityScheme);
                swaggerDoc.SecurityRequirements.Add(new OpenApiSecurityRequirement {
                {securityScheme, new string[] { }}
            });
            }
        }
    }
}
