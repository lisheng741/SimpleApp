namespace Microsoft.AspNetCore.Http;

public static class HttpResponseExtensions
{
    public static Task WriteAsync(this HttpResponse response, AppResult result, CancellationToken cancellationToken = default)
        => WriteAsync(response, result, Encoding.UTF8, cancellationToken);

    public static async Task WriteAsync(this HttpResponse response, AppResult result, Encoding encoding, CancellationToken cancellationToken = default)
    {
        if (response == null) throw new ArgumentNullException(nameof(response));
        if (response.HasStarted) throw new Exception("HTTP响应已经开始，不可更改响应");
        if (result == null) throw new ArgumentNullException(nameof(result));
        if (encoding == null) throw new ArgumentNullException(nameof(encoding));
        
        response.StatusCode = result.Code;
        response.ContentType = encoding == Encoding.UTF8 ? "application/json; charset=utf-8" : "application/json";
        await response.WriteAsync(JsonHelper.Serialize(result), encoding);
    }
}
