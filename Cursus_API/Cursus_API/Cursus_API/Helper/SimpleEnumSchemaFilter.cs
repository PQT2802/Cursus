using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cursus_API.Helper
{
    public class SimpleEnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Enum != null && context.Type.IsEnum)
            {
                schema.Enum.Clear();
                foreach (var value in Enum.GetValues(context.Type))
                {
                    schema.Enum.Add(new OpenApiString(value.ToString()));
                }
            }
        }
    }
}
    //    public class SimpleEnumSchemaFilter : ISchemaFilter
    //    {
    //        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    //        {
    //            if (context.Type.IsEnum)
    //            {
    //                schema.Enum.Clear();

//                foreach (var value in Enum.GetValues(context.Type))
//                {
//                    var enumValue = Convert.ToInt32(value);
//                    var enumName = Enum.GetName(context.Type, value);

//                    schema.Enum.Add(new OpenApiString($"{enumName} ({enumValue})"));
//                }
//            }
//        }
//        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
//        {
//            if (schema.Enum != null && context.Type.IsEnum)
//            {
//                schema.Enum.Clear();
//                foreach (var value in Enum.GetValues(context.Type))
//                {
//                    var enumValue = (int)value;
//                    schema.Enum.Add(new OpenApiString(enumValue.ToString()));
//                }

//                schema.Description = GetEnumDescriptions(context.Type);
//            }
//        }

//        private string GetEnumDescriptions(Type enumType)
//        {
//            var descriptions = Enum.GetNames(enumType);
//            return string.Join(", ", descriptions);
//        }
//    }
//    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
//    {
//        if (context.Type.IsEnum)
//        {
//            schema.Enum.Clear();
//            foreach (var value in Enum.GetValues(context.Type))
//            {
//                // Add enum name and integer value to the schema
//                var enumName = Enum.GetName(context.Type, value);
//                schema.Enum.Add(new OpenApiString($"{(int)value} ({enumName})"));
//            }

//            schema.Description = GetEnumDescriptions(context.Type);
//        }
//    }

//    private string GetEnumDescriptions(Type enumType)
//    {
//        var descriptions = Enum.GetNames(enumType);
//        return string.Join(", ", descriptions);
//    }
//    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
//    {
//        if (context.Type.IsEnum)
//        {
//            schema.Enum.Clear();

//            // Iterate through enum values
//            foreach (var value in Enum.GetValues(context.Type))
//            {
//                // Cast enum to its underlying type (int)
//                var enumValue = Convert.ToInt32(value);
//                var enumName = Enum.GetName(context.Type, value);

//                // Add both integer value and name to the schema
//                schema.Enum.Add(new OpenApiString($"{enumValue} "));
//            }

//            // Optionally, set the schema description for clarity
//            schema.Description = GetEnumDescriptions(context.Type);
//        }
//    }

//    private string GetEnumDescriptions(Type enumType)
//    {
//        return string.Join(", ", Enum.GetValues(enumType)
//                                     .Cast<Enum>()
//                                     .Select(e => $"{Convert.ToInt32(e)} ({e.ToString()})"));
//    }

//}
//    }
//}