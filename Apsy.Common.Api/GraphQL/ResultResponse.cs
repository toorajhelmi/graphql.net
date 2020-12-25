using Apsy.Common.Api.Graph;

namespace Apsy.Common.Api
{
    public class ResultResponse<TResult, TStatus>: Response<TStatus>
    {
        [Api]
        public TResult Result { get; set; }
    }
}
