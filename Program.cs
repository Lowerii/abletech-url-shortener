using AbleTech_UrlShortener.Handlers;
using AbleTech_UrlShortener.Repositories.Implementations;
using AbleTech_UrlShortener.Repositories.Interfaces;
using AbleTech_UrlShortener.Services.Implementations;
using AbleTech_UrlShortener.Services.Interfaces;

namespace AbleTech_UrlShortener;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddSingleton<IUrlRepository, UrlRepository>();
		builder.Services.AddScoped<IUrlService, UrlService>();
		
		builder.Services.AddControllers();

		builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
		builder.Services.AddProblemDetails();

		var app = builder.Build();

		app.UseExceptionHandler();

		app.MapControllers();
		
		app.MapGet("{code}", (string code, IUrlService urlService) =>
			Results.Redirect(urlService.RetrieveByCode(code).OriginalUrl)
		);

		app.Run();
	}
}