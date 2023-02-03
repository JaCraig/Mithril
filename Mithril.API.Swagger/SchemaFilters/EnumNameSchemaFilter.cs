using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mithril.API.Swagger.SchemaFilters
{
    /// <summary>
    /// Enum name schema filter
    /// </summary>
    /// <seealso cref="ISchemaFilter"/>
    public class EnumNameSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Applies the specified schema.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema is null || context?.Type.IsEnum != true)
                return;
            var Names = Enum.GetNames(context.Type);
            var arr = new OpenApiArray();
            arr.AddRange(Names.Select(Name => new OpenApiString(Name)));
            schema.Extensions.Add("EnumNames", arr);
        }
    }
}