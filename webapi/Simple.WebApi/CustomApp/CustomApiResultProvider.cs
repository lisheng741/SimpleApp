using Simple.Common.Result;

namespace Simple.WebApi;

public class CustomApiResultProvider : ApiResultProvider
{
    public override IActionResult ProcessApiResultException(ApiResultException resultException)
    {
        // 都返回 200 状态码
        var apiResult = resultException.ApiResult;
        IActionResult result = new ObjectResult(apiResult) { StatusCode = StatusCodes.Status200OK };

        return result;
    }
}
