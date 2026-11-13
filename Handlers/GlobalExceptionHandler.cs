using System.Net;
using AbleTech_UrlShortener.Models.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace AbleTech_UrlShortener.Handlers;

public class GlobalExceptionHandler(
	ILogger<GlobalExceptionHandler> logger
) : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		var (status, message) = (HttpStatusCode.InternalServerError, "Internal Server Error");

		if (exception is ApiException apiException)
		{
			status = apiException.StatusCode;
			message = apiException.Message;
		}
		
		if((int)status >= 500) // in case of a server error, we log it to make sure we track any potential issue in the codebase
			logger.LogError(exception, "Unhandled exception on {Method} {Path}", httpContext.Request.Method, httpContext.Request.Path);
		
		httpContext.Response.StatusCode = (int)status;
		await httpContext.Response.WriteAsJsonAsync(new { status, message }, cancellationToken);

		return true;
	}
}