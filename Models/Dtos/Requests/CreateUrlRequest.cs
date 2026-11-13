using System.ComponentModel.DataAnnotations;

namespace AbleTech_UrlShortener.Models.Dtos.Requests;

public record CreateUrlRequest(
	[Required]
	[Url]
	string OriginalUrl
);