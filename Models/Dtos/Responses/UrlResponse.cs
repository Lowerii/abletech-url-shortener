namespace AbleTech_UrlShortener.Models.Dtos.Responses;

public record UrlResponse(
	string Code,
	string ShortenedUrl,
	string OriginalUrl
);