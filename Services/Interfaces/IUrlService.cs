using AbleTech_UrlShortener.Models.Dtos.Requests;
using AbleTech_UrlShortener.Models.Dtos.Responses;

namespace AbleTech_UrlShortener.Services.Interfaces;

public interface IUrlService
{
	UrlResponse Create(CreateUrlRequest request);
	UrlResponse RetrieveByCode(string code);
}