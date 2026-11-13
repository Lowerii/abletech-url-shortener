namespace AbleTech_UrlShortener.Models.Entities;

public class UrlEntity
{
	public required string Code { get; set; }
	public required string OriginalUrl { get; set; }
}