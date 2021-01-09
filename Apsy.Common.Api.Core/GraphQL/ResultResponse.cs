using Apsy.Common.Api.Core.Graph;

namespace Apsy.Common.Api.Core
{
    public class ResultResponse<TResult, TStatus>: Response<TStatus>
    {
        [Api]
        public TResult Result { get; set; }
    }
}
