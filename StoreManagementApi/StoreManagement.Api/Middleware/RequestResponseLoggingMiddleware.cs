using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Api.Middleware;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory
                .CreateLogger<RequestResponseLoggingMiddleware>();
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation(await FormatRequest(context.Request));

        var request = await FormatRequest(context.Request);
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        _logger.LogInformation(await FormatResponse(context.Response));
        var response = await FormatResponse(context.Response);
        await responseBody.CopyToAsync(originalBodyStream);
    }

    private static async Task<string> FormatRequest(HttpRequest request)
    {


        //This line allows us to set the reader for the request back at the beginning of its stream.
        request.EnableBuffering();
        var body = request.Body;

        //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];

        //...Then we copy the entire request stream into the new buffer.
        await request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length)).ConfigureAwait(false);

        //We convert the byte[] into a string using UTF8 encoding...
        var bodyAsText = Encoding.UTF8.GetString(buffer);

        // reset the stream position to 0, which is allowed because of EnableBuffering()
        request.Body.Seek(0, SeekOrigin.Begin);
        request.Body = body;

        return $"{request.Scheme}://{request.Host}{request.Path}" +
                   (!string.IsNullOrEmpty(request.QueryString.ToString()) ? $" - QUERY: {request.QueryString}" : string.Empty) +
                   (!string.IsNullOrEmpty(bodyAsText) ? (bodyAsText.Length < 100 ? $" - BODY: {bodyAsText}" : $" - BODY: {bodyAsText[..98]}..") : string.Empty);
    }

    private static async Task<string> FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);

        string text = await new StreamReader(response.Body).ReadToEndAsync();


        response.Body.Seek(0, SeekOrigin.Begin);

        return $"{response.StatusCode}: {text}";
    }
}
