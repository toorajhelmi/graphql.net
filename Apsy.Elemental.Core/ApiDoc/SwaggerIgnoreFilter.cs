using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

namespace Apsy.Elemental.Core.ApiDoc
{
    public class SwaggerIgnoreFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext schemaFilterContext)
        {
            if (schema.Properties.Count == 0)
                return;

            const BindingFlags bindingFlags = BindingFlags.Public |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Instance;
            var memberList = schemaFilterContext.Type
                                .GetFields(bindingFlags).Cast<MemberInfo>()
                                .Concat(schemaFilterContext.Type
                                .GetProperties(bindingFlags));

            var excludedList = memberList.Where(m => m.GetCustomAttribute<SwaggerIgnoreAttribute>() != null)
                                         .Select(m => m.Name);

            foreach (var excludedName in excludedList)
            {
                var excludedProp = schema.Properties.FirstOrDefault(p => p.Key.ToLower() == excludedName.ToLower());
                
                if (excludedProp.Key != null)
                {
                    schema.Properties.Remove(excludedProp.Key);
                }
            }
        }
    }
}
