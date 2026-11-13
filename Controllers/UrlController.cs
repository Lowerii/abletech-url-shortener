using AbleTech_UrlShortener.Models.Dtos.Requests;
using AbleTech_UrlShortener.Models.Dtos.Responses;
using AbleTech_UrlShortener.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AbleTech_UrlShortener.Controllers;

[ApiController]
[Route("url")]
public class UrlController(
	IUrlService urlService
) : ControllerBase
{
	[HttpGet("{code}")]
	public UrlResponse RetrieveByCode(string code) => urlService.RetrieveByCode(code);
	
	[HttpPost]
	public ActionResult<UrlResponse> Create([FromBody] CreateUrlRequest request) => 
		StatusCode(StatusCodes.Status201Created, urlService.Create(request));
}