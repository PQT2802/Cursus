using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cursus_API.Helper
{
    public class SwaggerEnumOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var parameter in operation.Parameters)
            {
                if (parameter.In == ParameterLocation.Query && parameter.Schema.Enum != null)
                {
                    parameter.Description = string.Join(", ", parameter.Schema.Enum.Select(e => e.ToString()));
                }
            }
        }
    }
}
