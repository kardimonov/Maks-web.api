using System;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GolovinskyAPI.Infrastructure
{
    public class FileUploadOperation : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Clear();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = "access token",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "String",
                    Default = new OpenApiString("Bearer ")
                }
            });

        }
    }
}
