using AbleTech_UrlShortener.Models.Entities;
using AbleTech_UrlShortener.Repositories.Interfaces;

namespace AbleTech_UrlShortener.Repositories.Implementations;

public class UrlRepository : IUrlRepository
{
	private readonly Dictionary<string, UrlEntity> _urls = new(); // mocked database
	
	public void Save(UrlEntity url)
	{
		_urls.Add(url.Code, url);
	}

	public UrlEntity? FindByCode(string code) => _urls.GetValueOrDefault(code);
}