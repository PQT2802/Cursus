using Cursus_Data.Models.DTOs;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.RequestBody?.Content.ContainsKey("multipart/form-data") == true)
        {
            var mediaType = operation.RequestBody.Content["multipart/form-data"];
            var schema = mediaType.Schema;

            // Assuming 'courseContentDTOs' is the key for the collection
            if (schema.Properties.ContainsKey("courseContentDTOs"))
            {
                var courseContentArray = schema.Properties["courseContentDTOs"].Items;

                // Add file property to schema
                if (courseContentArray.Properties != null)
                {
                    courseContentArray.Properties["File"] = new OpenApiSchema
                    {
                        Type = "string",
                        Format = "binary"
                    };
                }
            }
        }
    }
}
