using Apsy.Common.Api.Graph;

namespace Apsy.Common.Api
{
    public class Response<TStatus>
    {
        [Api]
        public TStatus Status { get; set; }
    }
}
