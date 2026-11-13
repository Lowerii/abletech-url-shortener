using System.Net;
using System.Security.Cryptography;
using AbleTech_UrlShortener.Models.Dtos.Requests;
using AbleTech_UrlShortener.Models.Dtos.Responses;
using AbleTech_UrlShortener.Models.Entities;
using AbleTech_UrlShortener.Models.Exceptions;
using AbleTech_UrlShortener.Repositories.Interfaces;
using AbleTech_UrlShortener.Services.Interfaces;

namespace AbleTech_UrlShortener.Services.Implementations;

public class UrlService(
	IUrlRepository urlRepo,
	IConfiguration configuration
) : IUrlService
{
	private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
	
	public UrlResponse Create(CreateUrlRequest request)
	{
		// any validation to the dto that can't be done with annotations should be added here

		var url = new UrlEntity
		{
			Code = GenerateRandomCode(),
			OriginalUrl = request.OriginalUrl
		};
		
		urlRepo.Save(url);

		return new UrlResponse(
			Code: url.Code,
			OriginalUrl: url.OriginalUrl,
			ShortenedUrl: GenerateShortenedUrl(url.Code)
		);
	}

	public UrlResponse RetrieveByCode(string code)
	{
		var url = urlRepo.FindByCode(code) ?? throw new ApiException(HttpStatusCode.NotFound, "Invalid URL code provided");

		return new UrlResponse(
			Code: code,
			OriginalUrl: url.OriginalUrl,
			ShortenedUrl: GenerateShortenedUrl(code)
		);
	}
	
	private string GenerateShortenedUrl(string code) => $"{configuration.GetValue<string>("BaseUrl")}/{code}";

	private string GenerateRandomCode() // generates 7 random characters, in this case we suppose it always will be unique
	{
		return new string(
			Enumerable.Range(0, 7)
			          .Select(_ => Characters[
				          RandomNumberGenerator.GetInt32(Characters.Length)
			          ])
			          .ToArray()
		);
	}
}