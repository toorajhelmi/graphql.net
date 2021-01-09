using Apsy.Common.Api.Core.Graph;

namespace Apsy.Common.Api.Core
{
    public class Response<TStatus>
    {
        [Api]
        public TStatus Status { get; set; }
    }
}
