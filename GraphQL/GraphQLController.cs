using GraphQL;
using GraphQL.SystemTextJson;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Apsy.Common.Api.Graph
{
    //public class GraphQLController : Controller
    //{
    //    public class GraphQLRequest
    //    {
    //        public string Query { get; set; }

    //        [JsonConverter(typeof(ObjectDictionaryConverter))]
    //        public Dictionary<string, object> Variables
    //        {
    //            get; set;
    //        }
    //    }

    //    private readonly GraphQL.Types.Schema schema;

    //    public GraphQLController(GraphQL.Types.Schema schema)
    //    {
    //        this.schema = schema;
    //    }

    //    public async Task<IActionResult> Query([FromBody] GraphQLRequest request)
    //    {
    //        var result = await new DocumentExecuter().ExecuteAsync(options =>
    //        {
    //            options.Schema = schema;
    //            options.Query = request.Query;
    //            options.Inputs = request.Variables.ToInputs();
    //        });

    //        if (result.Errors?.Count > 0)
    //        {
    //            return BadRequest();
    //        }

    //        return Ok(result);
    //    }
    //}
}
