using System;

namespace FMCW.Common.Results
{
    public class BoolResult : BaseResult<bool, ErrorResult>
    {
        public static BoolResult Ok()
            => new BoolResult
            {
                ResultOk = true,
                ResultOperation = ResultOperation.Ok,
                Success = true
            };

        public static BoolResult Error(Exception ex)
           => new BoolResult
           {
               ResultError = ErrorResult.Build(ex),
               ResultOperation = ResultOperation.Error,
               Success = false
           };

    }
}
