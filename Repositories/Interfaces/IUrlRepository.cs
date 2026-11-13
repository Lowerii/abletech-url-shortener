using AbleTech_UrlShortener.Models.Entities;

namespace AbleTech_UrlShortener.Repositories.Interfaces;

public interface IUrlRepository
{
	void Save(UrlEntity url);
	UrlEntity? FindByCode(string code);
}