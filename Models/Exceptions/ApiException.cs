using System.Net;

namespace AbleTech_UrlShortener.Models.Exceptions;

public class ApiException(HttpStatusCode statusCode, string message) : Exception(message)
{
	public HttpStatusCode StatusCode { get; } = statusCode;
}
